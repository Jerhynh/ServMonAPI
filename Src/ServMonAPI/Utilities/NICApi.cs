using ServMonAPI.Enums;
using ServMonAPI.Models;
using System.Management;

namespace ServMonAPI.Utilities
{
    public class NICApi
    {
        public static List<NICDevice> QueryNICDevices()
        {
            if (!OperatingSystem.IsWindows())
            {
                throw new NotSupportedException("This method is not supported on the current Operating System!");
            }

            List<NICDevice> networkDevices = new();
            using (var searcher = new ManagementObjectSearcher("select * from Win32_NetworkAdapter"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    Dictionary<string, string> NICProperties = new();
                    foreach (var property in obj.Properties)
                    {
                        NICProperties.Add(property.Name, Convert.ToString(obj[property.Name.ToString()])!);
                    }
                    var device = new NICDevice(NICProperties);
                    networkDevices.Add(device);
                }
            }
            return networkDevices;
        }

        public static UInt64 MonitorNICIO(ManagementObject networkInterface, NICDeviceIOState iOState)
        {
            if (!OperatingSystem.IsWindows())
            {
                throw new NotSupportedException("This method is not supported on the current Operating System!");
            }
            if (iOState == NICDeviceIOState.Receive)
            {
                networkInterface.Get();
                return Convert.ToUInt64(networkInterface.GetPropertyValue("BytesReceivedPerSec"));
            }
            else if (iOState == NICDeviceIOState.Send)
            {
                networkInterface.Get();
                return Convert.ToUInt64(networkInterface.GetPropertyValue("BytesSentPersec"));
            }
            else if (iOState == NICDeviceIOState.SendReceive)
            {
                networkInterface.Get();
                return Convert.ToUInt64(networkInterface.GetPropertyValue("BytesTotalPersec"));
            }
            else
            {
                return 0;
            }
        }

        public static void MonitorNetworkIO()
        {
            if (!OperatingSystem.IsWindows())
                throw new NotSupportedException("This method is not supported on the current Operating System!");
            ManagementObjectSearcher searcher = new("SELECT * FROM Win32_PerfFormattedData_Tcpip_NetworkInterface");
            foreach (ManagementObject networkInterface in searcher.Get())
            {
                UInt64 bytesReceivedTotal = 0;
                Console.WriteLine($"Starting bandwidth monitoring for inteface: {networkInterface["Name"]}");
                for (; ; )
                {
                    //Thread.Sleep(1000);
                    networkInterface.Get();
                    //bytesReceivedTotal += Convert.ToUInt64(networkInterface.GetPropertyValue("BytesReceivedPerSec"));
                    bytesReceivedTotal += MonitorNICIO(networkInterface, NICDeviceIOState.SendReceive);
                    Console.WriteLine(NICDevice.FormatMemoryBytes(bytesReceivedTotal));
                }
            }
        }

    }
}
