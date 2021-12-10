using ServMonAPI.Models;
using System.Management;

namespace ServMonAPI.Utilities
{
    public class CPUApi
    {
        public static List<CPUDevice> QueryCPUDevices()
        {
            if (OperatingSystem.IsWindows())
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
            throw new NotSupportedException("This method is not supported on the current Operating System!");
        }
    }
}
