using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Runtime.InteropServices;

namespace SerialPlotAndLog
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        [STAThread]
        static void Main(string[] args)
        {
            // https://www.csharp411.com/console-output-from-winforms-application/
            AttachConsole(ATTACH_PARENT_PROCESS);

            string port = args.Length > 0 ? args[0] : null;
            string sBaud = args.Length > 1 ? args[1] : null;
            int baud = 0;

            // works fine with cmd.exe
            // a bit wonky with PowerShell.exe (displays but cursor location does not update)
            // doesn't work at all with PowerShell_ISE.exe
            // -- but this is really just for convenience, so let's not work any harder
            if (port != null)
            {
                var ports = SerialPortInfo.GetSerialPorts().ToArray();
                if (!ports.Any(p => p.PortName.Equals(port, StringComparison.Ordinal)))
                {
                    if (ports.Length == 0)
                    {
                        Console.Error.WriteLine("No COM ports found.");
                    }
                    else
                    {
                        Console.Error.WriteLine($"No COM port found matching {port}; available ports:");
                        foreach (var p in ports)
                            Console.Error.WriteLine($"   {p}");
                    }
                }
            }
            if (sBaud != null && !int.TryParse(args[1], out baud))
            {
                Console.Error.WriteLine("Expected an integer as the second argument (for baud rate).");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain(new SerialPortConfig(port, baud)));
        }
    }
}
