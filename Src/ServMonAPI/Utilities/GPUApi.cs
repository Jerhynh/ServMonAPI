using ServMonAPI.Models;
using System.Management;

namespace ServMonAPI.Utilities
{
    /// <summary>
    /// A class that contains useful methods for obtaining information about attached GPU devices.
    /// </summary>
    public class GPUApi
    {
        /// <summary>
        /// Queries WMI to obtain a list of all attacted GPU devices.
        /// </summary>
        /// <returns>Returns a list of all attached GPUDevices represented by GPUDevice objects.</returns>
        public static List<GPUDevice> QueryGPUDevices()
        {
            List<GPUDevice> graphicsDevices = new();
            using (var searcher = new ManagementObjectSearcher("select * from Win32_VideoController"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    Dictionary<string, string> GPUProperties = new();
                    foreach (var property in obj.Properties)
                    {
                        GPUProperties.Add(property.Name, Convert.ToString(obj[property.Name.ToString()])!);
                    }
                    var device = new GPUDevice(GPUProperties);
                    graphicsDevices.Add(device);
                }
            }
            return graphicsDevices;
        }

        /// <summary>
        /// Prints to the console (if exists) information about each attached GPU device.
        /// </summary>
        [Obsolete("This method is planned to be removed in the future from the base library. It is reccomended to get all future values from QueryGPUDevices().")]
        public static void PrintGPUInfo() // Note this class is planned to be removed in the future from the base dll.
        {
            using var searcher = new ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (ManagementObject obj in searcher.Get())
            {

                Console.WriteLine("Name  -  " + obj["Name"]!);
                Console.WriteLine("DeviceID  -  " + obj["DeviceID"]!);
                Console.WriteLine("AdapterRAM  -  " + obj["AdapterRAM"]!);
                Console.WriteLine("AdapterDACType  -  " + obj["AdapterDACType"]!);
                Console.WriteLine("Monochrome  -  " + obj["Monochrome"]!);
                Console.WriteLine("InstalledDisplayDrivers  -  " + obj["InstalledDisplayDrivers"]!);
                Console.WriteLine("DriverVersion  -  " + obj["DriverVersion"]!);
                Console.WriteLine("VideoProcessor  -  " + obj["VideoProcessor"]!);
                Console.WriteLine("VideoArchitecture  -  " + obj["VideoArchitecture"]!);
                Console.WriteLine("VideoMemoryType  -  " + obj["VideoMemoryType"]!);
            }
        }
    }
}
