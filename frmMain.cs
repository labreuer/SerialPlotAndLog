using System;
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
	public partial class frmMain : Form
	{
        private string _serialPortName;
        private string _baudRate;
        private static bool _abortReading = false;
        private static IAsyncResult _asyncResult = null;

		public frmMain(string serialPortName, string baud)
		{
            _serialPortName = serialPortName;
            _baudRate = baud;

            InitializeComponent();
			this.Load += frmMain_Load;
			this.FormClosing += frmMain_FormClosing;
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
            CloseHandles();
		}

        private SerialPort _ser;
        private FileStream _fs = null;

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

        private void frmMain_Load(object sender, EventArgs e)
        {
            error.Text = "";
            var portName = LoadPorts(_serialPortName);
            var baudRate = LoadBaudRates(_baudRate);

            SetupChart(portName, baudRate);

            var eh = (EventHandler)((s2, e2) => SetupChart((string)port.SelectedItem, (int)baud.SelectedItem));

            this.port.SelectedIndexChanged += eh;
            this.baud.SelectedIndexChanged += eh;
            this.resetChart.Click += eh;
            this.resetArduino.Click += (s2, e2) =>
            {
                _ser.DtrEnable = false;
                System.Threading.Thread.Sleep(10);
                _ser.DtrEnable = true;
                if (this.alsoResetChart.Checked)
                    eh(null, new EventArgs());
            };
        }

        private void SetupChart(string portName, int baudRate)
        {
            if (portName == null || baudRate == -1)
                return;
            CloseHandles();
            error.Text = null;
            lastData.Text = null;


            chart.ChartAreas.Clear();
            //chart.Legends.Clear();
            chart.Series.Clear();

            var area = new ChartArea();
            bool minimumYValueSet = false;
            chart.ChartAreas.Add(area);
            var series = new DefaultDictionary<int, Series>(n =>
                {
                    var newS = new Series() { ChartType = SeriesChartType.Line };
                    chart.Series.Add(newS);
                    return newS;
                });

            try
            {
                _ser = new SerialPort(portName, baudRate);
                _ser.Open();
            }
            catch (Exception ex)
            {
                error.Text = ex.Message;
                return;
            }

            string residual = "";

            try
            {
                string filename = string.Format("{0:yyyy-MM-dd_HH-mm-ss}_SerialPlotAndLog.txt", DateTime.Now);
                this.Text = string.Format("SerialPlotAndLog - {0}", Path.Combine(Environment.CurrentDirectory, filename));
                _fs = new FileStream(filename, FileMode.Create);
                SetupDataReceive(_ser, by =>
                {
                    _fs.Write(by, 0, by.Length);
                    _fs.Flush();
                    //Console.WriteLine(Encoding.ASCII.GetString(by));
                    string s = residual + Encoding.ASCII.GetString(by);
                    string[] lines = Regex.Split(s, @"\n\r?");
                    residual = lines[lines.Length - 1];
                    chart.Invoke((Action)(() =>
                    {
                        for (int i = 0; i <= lines.Length - 2; i++)
                        {
                            lastData.Text = lines[i];
                            if (lines[i].StartsWith("#"))
                                continue;
                            var x = Regex.Split(lines[i], " *, *");
                            for (int j = 0; j < x.Length; j++)
                            {
                                if (float.TryParse(x[j], out float f))
                                {
                                    series[j].Points.Add(f);
                                    if (yAxisNonZero.Checked)
                                    {
                                        if (!minimumYValueSet)
                                        {
                                            area.AxisY.Minimum = Math.Floor(f);
                                            minimumYValueSet = true;
                                        }
                                        else
                                        {
                                            area.AxisY.Minimum = Math.Min(area.AxisY.Minimum, Math.Floor(f));
                                        }
                                    }
                                }
                                else
                                {
                                    series[j].Points.Add(new DataPoint { IsEmpty = true });
                                    if (x[j].Length != 0)
                                        series[j].LegendText = x[j];
                                }
                            }
                            area.AxisX.Maximum++;
                        }
                        chart.Invalidate();
                    }));
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

        private string LoadPorts(string selectedPort)
        {
            string ret = null;
            var ports = SerialPort.GetPortNames();
            foreach (string p in ports)
                this.port.Items.Add(p);

            if (selectedPort != null)
            {
                if (SetPortValue(ports, selectedPort, " specified from the command line"))
                    ret = (string)port.SelectedItem;
            }
            else if (ports.Length > 0)
            {
                this.port.SelectedIndex = 0;
                ret = (string)port.SelectedItem;
            }
            else
            {
                error.Text = "No COM ports found.";
            }

            return ret;
        }

        private void Port_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private bool SetPortValue(string[] ports, string portName, string extraError = "")
        {
            if (portName == null)
            {
                error.Text = "Port name cannot be null.";
                return false;
            }

            var idx = Array.FindIndex(ports, p => p.Equals(portName, StringComparison.OrdinalIgnoreCase));

            if (idx == -1)
            {
                error.Text = string.Format("Did not find COM port '{0}'{1}.", portName, extraError);
                return false;
            }

            this.port.SelectedIndex = idx;
            return true;
        }

        private int LoadBaudRates(string selectedBaud)
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
            foreach (var b in bauds)
                this.baud.Items.Add(b);

            return SetBaudRate(bauds, selectedBaud ?? DefaultBaud.ToString(), " specified from the command line");
        }

        private int SetBaudRate(int[] bauds, string baudRate, string extraError = "")
        {
            if (baudRate == null)
                throw new ArgumentNullException(nameof(baudRate));

            int baud;
            if (!int.TryParse(baudRate, out baud))
            {
                error.Text = string.Format("'{0}'{1} is not an integer.", baudRate, extraError);
                return -1;
            }

            var idx = Array.IndexOf(bauds, baud);

            if (idx != -1)
                this.baud.SelectedIndex = idx;
            else
                this.baud.SelectedItem = baud;

            return baud;
        }
    }
}
