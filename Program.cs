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

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
		static void Main(string[] args)
		{
            // https://www.csharp411.com/console-output-from-winforms-application/
            AttachConsole(ATTACH_PARENT_PROCESS);

            string port = args.Length > 0 ? args[0] : null;
            string baud = args.Length > 1 ? args[1] : null;

            if (false)//(port == null)
            {
                var ports = SerialPort.GetPortNames();

                if (ports.Length == 1)
                {
                    port = ports[0];
                    Console.WriteLine("Automatically using the only port detected: {0}", port);
                }
                else if (ports.Length == 0)
                {
                    Console.Error.WriteLine("No serial ports found.");
                    return;
                }
                else
                {
                    string name = System.IO.Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    Console.Error.WriteLine("Multiple ports found:");
                    foreach (var p in ports)
                        Console.Error.WriteLine("    {0}", p);
                    Console.Error.WriteLine("Please specify one by typing:");
                    Console.Error.WriteLine("    {0} COMX", name);
                    Console.Error.WriteLine("or:");
                    Console.Error.WriteLine("    {0} COMX BAUD", name);
                    return;
                }
            }

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new frmMain(port, baud));
		}
	}
}
