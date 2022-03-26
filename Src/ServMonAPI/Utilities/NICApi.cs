using ServMonAPI.Enums;
using ServMonAPI.Models;
using System.Management;

namespace ServMonAPI.Utilities
{
    /// <summary>
    /// A class that contains useful methods for obtaining information about attached NIC devices.
    /// </summary>
    public class NICApi
    {
        /// <summary>
        /// Queries WMI to obtain a list of all attacted NIC devices.
        /// </summary>
        /// <returns>Returns a list of all attached NICDevices represented by NICDevice objects.</returns>
        public static List<NICDevice> QueryNICDevices()
        {
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

        /// <summary>
        /// Must supply a monitorable NIC device, returns the IO status listed in WMI.
        /// </summary>
        /// <param name="networkInterface"></param>
        /// <param name="iOState"></param>
        /// <returns>Returns a UInt64 representing the IO state of the specified NIC.</returns>
        public static ulong MonitorNICIO(ManagementObject networkInterface, NICDeviceTrafficType iOState)
        {   
            networkInterface.Get();
            if (iOState == NICDeviceTrafficType.Ingress)
            {
                return Convert.ToUInt64(networkInterface.GetPropertyValue("BytesReceivedPerSec"));
            }
            else if (iOState == NICDeviceTrafficType.Egress)
            {
                return Convert.ToUInt64(networkInterface.GetPropertyValue("BytesSentPersec"));
            }
            else if (iOState == NICDeviceTrafficType.IngressEgress)
            {
                return Convert.ToUInt64(networkInterface.GetPropertyValue("BytesTotalPersec"));
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Returns all NIC devices made avaliable under the Win32_PerfFormattedData_Tcpip_NetworkInterface query with corresponding objects in a List.
        /// </summary>
        /// <returns>Returns ManagementObject list containing corresponding object for each detected NIC.</returns>
        public static List<ManagementObject> GetMonitorableNICs()
        {
            ManagementObjectSearcher searcher = new("SELECT * FROM Win32_PerfFormattedData_Tcpip_NetworkInterface");
            List<ManagementObject> NicArray = new();
            foreach (ManagementObject networkInterface in searcher.Get())
            {
                NicArray.Add(networkInterface);
                //Console.WriteLine($"Network interface {NICIndice}: {networkInterface["Name"]}"); // Name is the mental identifier for the nic.
            }
            return NicArray;
        }

    }
}
