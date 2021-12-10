using ServMonAPI.Utilities;

namespace ServMonAPI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //foreach (var processor in CPUApi.QueryCPUDevices())
            //{
            //    Console.ForegroundColor = ConsoleColor.DarkGreen;
            //    Console.WriteLine(processor.CPUDeviceProperties["Name"]);
            //    Console.ForegroundColor = ConsoleColor.White;
            //    foreach (var property in processor.CPUDeviceProperties)
            //    {
            //        Console.WriteLine($"{property.Key} : {property.Value}");
            //    }
            //}

            //foreach (var nic in NICApi.QueryNICDevices())
            //{
            //    Console.ForegroundColor = ConsoleColor.DarkGreen;
            //    Console.WriteLine(nic.NICDeviceProperties["Name"]);
            //    Console.ForegroundColor = ConsoleColor.White;
            //    foreach (var property in nic.NICDeviceProperties)
            //    {
            //        Console.WriteLine($"{property.Key} : {property.Value}");
            //    }
            //}

            //var devs = GPUApi.QueryGPUDevices();
            //foreach (var dev in devs)
            //{
            //    Console.ForegroundColor = ConsoleColor.DarkGreen;
            //    Console.WriteLine(dev.GPUDeviceProperties["Name"]);
            //    Console.ForegroundColor = ConsoleColor.White;

            //    foreach (var item in dev.GPUDeviceProperties)
            //    {
            //        Console.WriteLine($"{item.Key} : {item.Value}");
            //    }
            //}


            //foreach (var dev in NICApi.QueryNICDevices())
            //{
            //    Console.WriteLine(dev.NICDeviceProperties["Name"]);
            //    foreach (var prop in dev.NICDeviceProperties)
            //    {
            //        Console.WriteLine($"{prop.Key}: " + dev.NICDeviceProperties[prop.Key]);
            //        // Query for NetConnectionID and look for adapter that has the name: NIC2
            //    }
            //}
            NICApi.MonitorNetworkIO();

        }

    }
}