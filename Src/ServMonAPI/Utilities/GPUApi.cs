using ServMonAPI.Models;
using System.Management;

namespace ServMonAPI.Utilities
{
    public class GPUApi
    {
        public static List<GPUDevice> QueryGPUDevices()
        {
            if (OperatingSystem.IsWindows())
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
            throw new NotSupportedException("This method is not supported on the current Operating System!");
        }

        public static void PrintGPUInfo()
        {
            if (OperatingSystem.IsWindows())
            {
                using (var searcher = new ManagementObjectSearcher("select * from Win32_VideoController"))
                {
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
            throw new NotSupportedException("This method is not supported on the current Operating System!");
        }
    }
}
