using ServMonAPI.Models;
using System.Management;

namespace ServMonAPI.Utilities
{
    /// <summary>
    /// A class that contains useful methods for obtaining information about attached CPU devices.
    /// </summary>
    public class CPUApi
    {
        /// <summary>
        /// Queries WMI to obtain a list of all attacted CPU devices.
        /// </summary>
        /// <returns>Returns a list of all attached CPUDevices represented by CPUDevice objects.</returns>
        public static List<CPUDevice> QueryCPUDevices()
        {                
            List<CPUDevice> Processors = new();
            using (var searcher = new ManagementObjectSearcher("select * from Win32_Processor"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    Dictionary<string, string> CPUProperties = new();
                    foreach (var property in obj.Properties)
                    {
                        CPUProperties.Add(property.Name, Convert.ToString(obj[property.Name.ToString()])!);
                    }
                    var device = new CPUDevice(CPUProperties);
                    Processors.Add(device);
                }
            }
            return Processors;
        }

    }
}
