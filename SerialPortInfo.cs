using System.Collections.Generic;
using System.Management;

namespace SerialPlotAndLog
{
    public class SerialPortInfo
    {
        public string PortName { get; set; }
        public string Description { get; set; }

        public override int GetHashCode()
        {
            return PortName.GetHashCode() ^ Description.GetHashCode();
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
            return !string.IsNullOrEmpty(PortName)
                ? $"{PortName} ({Description})"
                : $"({Description})";
        }

        public static IEnumerable<SerialPortInfo> GetSerialPorts()
        {
            using (var mc = new ManagementClass("Win32_SerialPort"))
            {
                foreach (ManagementObject mo in mc.GetInstances())
                {
                    yield return new SerialPortInfo
                    {
                        PortName = (string)mo.GetPropertyValue("DeviceID"),
                        Description = (string)mo.GetPropertyValue("Description"),
                    };
                }
            }
            // SerialPort.GetPortNames() doesn't update while the application is open if
            // a device is unplugged; the same is true of the following registry keys:
            //     using (var key = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DEVICEMAP\SERIALCOMM"))
            //         Console.WriteLine("Registry: " + string.Join(", ", key.GetValueNames().Select(name => key.GetValue(name).ToString()).ToArray()));
            // WMI doesn't seem to be "sticky" in this way.
        }
    }
}
