using Microsoft.Win32;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SerialPlotAndLog
{
    public struct SerialPortConfig
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public SerialPortConfig(string portName, int baudRate)
        {
            this.PortName = portName;
            this.BaudRate = baudRate;
        }
    }

    public partial class frmMain : Form
    {
        private SerialPortConfig _args;
        private SerialPortConfig _current;
        private static bool _abortReading = false;
        private static IAsyncResult _asyncResult = null;
        private SerialPort _ser = null;
        private FileStream _fs = null;
        private ConcurrentQueue<string> _queue = new ConcurrentQueue<string>();
        private System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
        private SerialPortInfo SelectedPort
        {
            get => (SerialPortInfo)port.SelectedItem;
            set => port.SelectedItem = value;
        }
        private int SelectedBaud
        {
            get => (int)baud.SelectedItem;
            set => baud.SelectedItem = value;
        }

        public frmMain(SerialPortConfig args)
        {
            _args = args;

            InitializeComponent();
            this.Load += frmMain_Load;
            this.FormClosing += (s, e) => CloseHandles();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            error.Text = "";
            RepopulatePortSelect();
            PopulateBaudRates(_args.BaudRate);
            _current = InitializeFromConfig(_args);

            SetupChart(_current);

            void eh(object s2, EventArgs e2) => SetupChart(new SerialPortConfig(SelectedPort.PortName, SelectedBaud));
            bool ignoreChanges = false;
            port.DropDown += (s2, e2) =>
            {
                ignoreChanges = true;
                RepopulatePortSelect();
                ignoreChanges = false;
            };
            port.SelectedIndexChanged += (s2, e2) => { if (!ignoreChanges) eh(s2, e2); };
            baud.SelectedIndexChanged += eh;
            resetChart.Click += (object s2, EventArgs e2) => ResetChartOnly();
            resetArduino.Click += (s2, e2) =>
            {
                _ser.DtrEnable = true;
                Thread.Sleep(10);
                _ser.DtrEnable = false;
                if (alsoResetChart.Checked)
                    eh(null, new EventArgs());
            };
            new ToolTip().SetToolTip(error, "Double click to copy error text to the clipboard.");
            error.DoubleClick += (s2, e2) => Clipboard.SetText(error.Text);
            _timer.Tick += _timer_Tick;
            _timer.Interval = 50;
            _timer.Enabled = true;
        }

        private void CloseHandles()
        {
            if (_ser != null && _ser.IsOpen)
            {
                _abortReading = true;
                // strictly speaking we could just let the async operation in SetupDataReceive error out;
                // it will anyway if the COM device is removed in the middle of a read
                if (_asyncResult != null && !_asyncResult.IsCompleted)
                    _asyncResult.AsyncWaitHandle.WaitOne(500);
                _ser.Close();
                _ser.Dispose();
                _abortReading = false;
            }
            if (_fs != null)
            {
                _fs.Close();
                _fs.Dispose();
            }
        }

        // HACK: our data rate is 8 per second right now
        const float SamplesPerSecond = 8;
        // HACK: this should be configured in a file or via the UI
        const int MaxSamples = 2400;

        class AugmentedSeries : Series
        {
            public Label CtlLabel;
            public TextBox CtlTextBox;
            public CheckBox CtlCheckBox;

            public AugmentedSeries(Form form, int seriesNumber)
            {
                // there wasn't an easy way to get the legend dimensions
                CtlLabel = new Label { Text = $"series {seriesNumber}", TextAlign = ContentAlignment.MiddleRight, Left = form.Width - 240, Top = 300 + seriesNumber * 30 + 6, BackColor = Color.White, Anchor = AnchorStyles.Right };
                CtlTextBox = new TextBox { Left = form.Width - 140, Top = 300 + seriesNumber * 30, Width = 70, Anchor = AnchorStyles.Right };
                CtlCheckBox = new CheckBox { Text = "", Left = form.Width - 50, Top = 300 + seriesNumber * 30 + 6, Anchor = AnchorStyles.Right };

                CtlTextBox.Font = new Font(CtlTextBox.Font.FontFamily, 12);

                form.Controls.Add(CtlLabel);
                form.Controls.Add(CtlTextBox);
                form.Controls.Add(CtlCheckBox);

                CtlLabel.BringToFront();
                CtlTextBox.BringToFront();
                CtlCheckBox.BringToFront();

                CtlCheckBox.CheckedChanged += CtlCheckBox_CheckedChanged;
            }

            private void CtlCheckBox_CheckedChanged(object sender, EventArgs e)
            {
                this.YAxisType = CtlCheckBox.Checked ? AxisType.Secondary : AxisType.Primary;
            }

            public void ChangeLabel(string text)
            {
                this.LegendText = text;
                CtlLabel.Text = text;
            }

            public void AddPoint(int index, string s, double f)
            {
                this.Points.Add(new DataPoint { XValue = index / SamplesPerSecond, YValues = new[] { f } });
                CtlTextBox.Text = s;
            }
            public void AddEmptyPoint(int index)
            {
                this.Points.Add(new DataPoint { XValue = index / SamplesPerSecond, IsEmpty = true });
                CtlTextBox.Text = "";
            }

            public void RemoveControls()
            {
                foreach (var ctl in new Control[] { CtlLabel, CtlTextBox, CtlCheckBox })
                {
                    if (ctl.Parent != null)
                        ctl.Parent.Controls.Remove(ctl);
                    ctl.Dispose();
                }

                CtlLabel = null;
                CtlTextBox = null;
                CtlCheckBox = null;
            }
        }

        DefaultDictionary<int, AugmentedSeries> _series;
        ChartArea _area;
        bool _minimumYValueSet = false;
        int _dataIndex = 0;
        int _visibleStartIndex = 0;
        private void _timer_Tick(object sender, EventArgs e)
        {
            if (_queue.IsEmpty)
                return;
            if (!Monitor.TryEnter(_timer, 1000))
                return;
            _timer.Enabled = false;

            chart.Invoke((Action)(() =>
            {
                while (_queue.TryDequeue(out string line))
                {
                    lastData.Text = line;
                    if (line.StartsWith("#"))
                        continue;
                    _dataIndex++;
                    var x = Regex.Split(line, " *, *");
                    for (int j = 0; j < x.Length; j++)
                    {
                        if (float.TryParse(x[j], out float f))
                        {
                            _series[j].AddPoint(_dataIndex, x[j], f);
                            if (yAxisNonZero.Checked)
                            {
                                if (!_minimumYValueSet)
                                {
                                    _area.AxisY.Minimum = Math.Floor(f);
                                    _minimumYValueSet = true;
                                }
                                else
                                {
                                    _area.AxisY.Minimum = Math.Min(_area.AxisY.Minimum, Math.Floor(f));
                                }
                            }
                        }
                        else
                        {
                            _series[j].AddEmptyPoint(_dataIndex);
                            if (x[j].Length != 0)
                                _series[j].ChangeLabel(x[j]);
                        }
                    }
                    //_area.AxisX.Maximum++;
                }
                if (chart.Series[0].Points.Count > MaxSamples)
                {
                    /* // The below is the non-zoom option
                    int toRemove = chart.Series[0].Points.Count - MaxSamples;
                    //toRemove = (int)(Math.Ceiling(toRemove / SamplesPerSecond) * SamplesPerSecond);

                    _visibleStartIndex += toRemove;
                    foreach (var ser in chart.Series)
                        for (int i = 0; i < toRemove; i++)
                            ser.Points.RemoveAt(0);
                    //chart.ChartAreas[0].AxisX.Minimum = Math.Floor(_visibleStartIndex / SamplesPerSecond);
                    //chart.ChartAreas[0].AxisX.Maximum = Math.Floor(_visibleStartIndex / SamplesPerSecond) + MaxSamples / SamplesPerSecond;
                    chart.ChartAreas[0].AxisX.Minimum = _visibleStartIndex / SamplesPerSecond;
                    chart.ChartAreas[0].AxisX.Maximum = _visibleStartIndex / SamplesPerSecond + + MaxSamples / SamplesPerSecond;
                    //chart.ChartAreas[0].AxisX.MajorTickMark.IntervalOffset = (_visibleStartIndex % SamplesPerSecond) / SamplesPerSecond;
                    chart.ChartAreas[0].AxisX.IntervalOffset = -(_visibleStartIndex % SamplesPerSecond) / SamplesPerSecond;
                    */

                    var start = (_dataIndex - MaxSamples) / SamplesPerSecond;
                    var sv = chart.ChartAreas[0].AxisX.ScaleView;

                    if (!sv.IsZoomed)
                        sv.Size = MaxSamples / SamplesPerSecond;

                    if (start - sv.Position < 2)
                        sv.Scroll(start);
                }
                chart.Invalidate();
            }));

            _timer.Enabled = true;
            Monitor.Exit(_timer);
        }

        private void SetupChart(SerialPortConfig config)
        {
            CloseHandles();
            // the only way to clear in .NET 4.8
            while (_queue.TryDequeue(out string ignored))
                ;

            if (string.IsNullOrEmpty(config.PortName) || config.BaudRate <= 0)
                return;

            if (_series != null)
                foreach (var ser in _series.Values)
                    ser.RemoveControls();

            _dataIndex = 0;
            error.Text = null;
            lastData.Text = null;

            chart.ChartAreas.Clear();
            chart.Series.Clear();

            _area = new ChartArea();
            _minimumYValueSet = false;
            chart.ChartAreas.Add(_area);
            _series = new DefaultDictionary<int, AugmentedSeries>(n =>
                {
                    var newS = new AugmentedSeries(this, n) { ChartType = SeriesChartType.Line, BorderWidth = 2 };
                    chart.Series.Add(newS);
                    return newS;
                });

            try
            {
                _ser = new SerialPort(config.PortName, config.BaudRate);
                _ser.Open();
            }
            catch (Exception ex)
            {
                error.Text = ex.Message;
                return;
            }

            var area = chart.ChartAreas[0];
            area.CursorX.AutoScroll = true;
            area.AxisX.ScaleView.Zoomable = true;
            area.AxisX.ScaleView.SizeType = DateTimeIntervalType.Number;
            area.AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            //area.AxisX.ScaleView.SmallScrollSize = MaxSamples / SamplesPerSecond;

            string residual = "";

            try
            {
                string filename = string.Format("{0:yyyy-MM-dd_HH-mm-ss}_SerialPlotAndLog.txt", DateTime.Now);
                this.Text = string.Format("SerialPlotAndLog - {0}", Path.Combine(Environment.CurrentDirectory, filename));
                _fs = new FileStream(filename, FileMode.Create);
                // consider https://www.sparxeng.com/blog/software/reading-lines-serial-port
                SetupDataReceive(_ser, by =>
                {
                    // this should only fire when the chart is reset, and that will get altered in a soon-to-be-made change
                    if (!_fs.CanWrite)
                    {
                        error.Invoke((Action)(() => error.Text = "Cannot write to log file."));
                        return;
                    }
                    _fs.Write(by, 0, by.Length);
                    _fs.Flush();
                    string s = residual + Encoding.ASCII.GetString(by);
                    string[] lines = Regex.Split(s, @"\n\r?");
                    residual = lines[lines.Length - 1];
                    for (int i = 0; i <= lines.Length - 2; i++)
                        _queue.Enqueue(lines[i]);
                }, ex => error.Invoke((Action)(() => error.Text = ex.Message)));
            }
            catch
            {
                _fs.Close();
                _fs.Dispose();
                _fs = null;
                throw;
            }
        }

        private void ResetChartOnly()
        {
            if (!Monitor.TryEnter(_timer, 3000))
            {
                error.Text = "Monitor.TryEnter returned false";
                return;
            }

            foreach (var ser in chart.Series)
                ser.Points.Clear();

            // not going to do this for now, to match position in file
            //_dataIndex = 0;
            _minimumYValueSet = false;
            error.Text = null;
            lastData.Text = null;
            _visibleStartIndex = _dataIndex;
            var area = chart.ChartAreas.First();
            area.AxisX.Minimum = Math.Floor(_visibleStartIndex / SamplesPerSecond);
            // https://stackoverflow.com/a/10593832/2328341 -- ChartArea.RecalculateAxesScale() did not work
            // not sure how many of the below are required for the zoom option, rather than the "delete points" option
            area.AxisX.Maximum = Double.NaN;
            area.AxisY.Minimum = Double.NaN;
            area.AxisY.Maximum = Double.NaN;
            area.AxisY2.Minimum = Double.NaN;
            area.AxisY2.Maximum = Double.NaN;
            var start = (_dataIndex - MaxSamples) / SamplesPerSecond;
            var sv = chart.ChartAreas[0].AxisX.ScaleView;
            sv.ZoomReset();

            Monitor.Exit(_timer);
        }

        System.Threading.Timer _testTimer;
        StreamReader _testSr;
        private void SetupDataReceiveTEST(SerialPort _, Action<byte[]> receive, Action<Exception> error)
        {

            const string TestFileFullName = @"C:\Users\labreuer\Downloads\20220603_4650.txt";
            if (_testSr == null)
                _testSr = new StreamReader(TestFileFullName);
            if (_testTimer != null)
                _testTimer.Dispose();
            _testTimer = new System.Threading.Timer(o =>
            {
                var by = Encoding.ASCII.GetBytes(_testSr.ReadLine() + "\r\n");
                receive(by);
            }, null, 0, 20);
        }

        private static void SetupDataReceive(SerialPort port, Action<byte[]> receive, Action<Exception> error)
        {
            byte[] buffer = new byte[10];
            Action kickoffRead = null;
            kickoffRead = delegate {
                if (!port.IsOpen)
                    return;

                _asyncResult = port.BaseStream.BeginRead(buffer, 0, buffer.Length, delegate (IAsyncResult ar)
                {
                    try
                    {
                        // this can get called after the port is closed
                        if (_abortReading)
                            return;
                        int actualLength = port.BaseStream.EndRead(ar);
                        byte[] received = new byte[actualLength];
                        Buffer.BlockCopy(buffer, 0, received, 0, actualLength);
                        receive(received);
                    }
                    catch (IOException ex)
                    {
                        if (ex.Message == "The I/O operation has been aborted because of either a thread exit or an application request.\r\n")
                        {
                            error(new Exception("Please check if the COM port is still available."));
                            port.Close();
                            port.Dispose();
                            return;
                        }

                        error(ex);
                    }
                    catch (InvalidOperationException ex)
                    {
                        // this happens if the port is closed due to e.g. changing the baud rate or port;
                        // even if we avoid that via using _asyncResult, someone could physically remove the device
                        if (ex.Message == "The BaseStream is only available when the port is open.")
                            return;
                        else
                            error(ex);
                    }
                    kickoffRead();
                }, null);
            };
            kickoffRead();
        }

        private SerialPortConfig InitializeFromConfig(SerialPortConfig args)
        {
            string error = null;

            if (args.PortName != null)
            {
                var spi = port.Items.Cast<SerialPortInfo>()
                    .SingleOrDefault(e => e.PortName.Equals(args.PortName, StringComparison.OrdinalIgnoreCase));

                if (spi != null)
                    SelectedPort = spi;
                else
                    error = $"No port found with name {args.PortName}.";
            }
            else if (port.Items.Count > 1)
            {
                port.SelectedIndex = 1;
                args.PortName = SelectedPort.PortName;
            }

            if (args.BaudRate != 0)
            {
                if (baud.Items.Contains(args.BaudRate))
                    SelectedBaud = args.BaudRate;
                else
                    error = $"${args.BaudRate} is not an acceptable baud rate.";
            }
            else
            {
                args.BaudRate = SelectedBaud;
            }

            if (error != null)
            {
                this.error.Text = error;
                return new SerialPortConfig("", int.Parse(baud.Text));
            }
            else
            {
                return args;
            }
        }

        private SerialPortInfo[] RepopulatePortSelect()
        {
            var dummy = new SerialPortInfo("", "disconnect");

            var ports = new[] { dummy }.Concat(SerialPortInfo.GetSerialPorts()).ToArray();
            var h = new HashSet<SerialPortInfo>(ports);
            var lastSelected = port.SelectedIndex;

            // lastSelected == -1 in its default state
            if (port.Items.Count > 0 && !h.Contains(SelectedPort) || lastSelected == -1)
                lastSelected = 0;

            port.Items.Clear();
            port.Items.AddRange(ports);
            port.SelectedIndex = lastSelected;

            return ports;
        }

        private void PopulateBaudRates(int additionalBaudRate = 0)
        {
            const int DefaultBaud = 115200;
            int[] bauds = new[]
            {
                230400,
                115200,
                57600,
                38400,
                19200,
                9600,
            };
            if (additionalBaudRate == 0)
                additionalBaudRate = DefaultBaud;
            var allBauds = bauds.Concat(new[] { additionalBaudRate })
                .Distinct()
                .OrderByDescending(x => x);
            baud.Items.AddRange(allBauds.Select(b => (object)b).ToArray());
            baud.SelectedItem = additionalBaudRate;
        }
    }
}
