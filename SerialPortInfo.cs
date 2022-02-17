using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;

namespace SerialPlotAndLog
{
    public class SerialPortInfo
    {
        public string PortName { get; set; }
        public string Description { get; set; }

        public SerialPortInfo(string portName, string description)
        {
            PortName = portName;
            Description = description;
        }

        public override int GetHashCode()
        {
            return (PortName ?? "").GetHashCode() ^ (Description ?? "").GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as SerialPortInfo;
            return
                other != null &&
                other.PortName == PortName &&
                other.Description == Description;
        }

        public static bool operator ==(SerialPortInfo a, SerialPortInfo b)
        {
            return a is null && b is null || !(a is null) && a.Equals(b);
        }
        public static bool operator !=(SerialPortInfo a, SerialPortInfo b) => !(a == b);

        public override string ToString()
        {
            var name = !string.IsNullOrEmpty(PortName) ? $"{PortName} " : "";
            var description = !string.IsNullOrEmpty(Description) ? $"({Description})" : "";

            return $"{name}{description}";
        }

        public static IEnumerable<SerialPortInfo> GetSerialPorts()
        {
            var ports = new List<SerialPortInfo>();

            using (var mc = new ManagementClass("Win32_SerialPort"))
            {
                foreach (ManagementObject mo in mc.GetInstances())
                {
                    ports.Add(new SerialPortInfo(
                        (string)mo.GetPropertyValue("DeviceID"),
                        (string)mo.GetPropertyValue("Description")));
                }
            }

            // HACK: the above wasn't working on a different machine
            foreach (var name in SerialPort.GetPortNames())
                if (!ports.Any(p => name.Equals(p.PortName, System.StringComparison.OrdinalIgnoreCase)))
                    ports.Add(new SerialPortInfo(name, null));

            return ports;

            // SerialPort.GetPortNames() doesn't update while the application is open if
            // a device is unplugged; the same is true of the following registry keys:
            //     using (var key = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DEVICEMAP\SERIALCOMM"))
            //         Console.WriteLine("Registry: " + string.Join(", ", key.GetValueNames().Select(name => key.GetValue(name).ToString()).ToArray()));
            // WMI doesn't seem to be "sticky" in this way.
        }
    }
}
