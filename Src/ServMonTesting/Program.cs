using System;
using ServMonAPI;
using ServMonAPI.Utilities;
using ServMonAPI.Enums;
using ServMonAPI.Models;
using System.Management;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            Console.Title = "ServMon Test Utility";

            ManagementObject nic = QueryNIC(); // Get nic monitoring preference from the user

            UInt64 IOBytesTotal = 0; // Init a counter for keeping track of reported IO bytes
            Console.WriteLine($"Starting bandwidth monitoring for inteface: {nic["Name"]}");
            for (; ;)
            {
                Task.Delay(1000).Wait();
                var resBytes = NICApi.MonitorNICIO(nic, NICDeviceIOState.SendReceive);
                IOBytesTotal += resBytes;
                Console.WriteLine($"Total Usage: {NICDevice.FormatMemoryBytes(IOBytesTotal)} Added: {NICDevice.FormatMemoryBytes(resBytes)}");
                Console.Title = $"ServMon Test Utility | Status: Monitoring {nic["Name"]} | Current Session Bill: {CalculateBill(IOBytesTotal, 75):c}"; // format for currency
            }
        }

        private static ManagementObject QueryNIC()
        {   
            Dictionary<int, ManagementObject> NICDict = new();
            var firstLoop = true;
            Console.Clear();
            Console.WriteLine("Network Interfaces:");
            while (true)
            {
                var indice = 0;
                foreach (var nic in NICApi.GetMonitorableNICs())
                {
                    nic.Get();
                    Console.WriteLine($"Interface {indice}: {nic["Name"]}");
                    if (firstLoop)
                    {
                        NICDict.Add(indice, nic);   
                    }
                    indice++;
                }
                firstLoop = false;
                Console.Write("Select the interface to monitor: ");
                var NICIface = Console.ReadLine();
                if (NICIface != null && int.TryParse(NICIface, out int NicIndice))
                {
                    try
                    {
                        return NICDict[NicIndice];
                    }
                    catch (KeyNotFoundException)
                    {
                        Console.WriteLine("Please enter a valid NIC number! (Press any key to continue...)");
                        Console.ReadLine();
                    }
                }
                // Nic not found query again for nic selection.
            }
        }

        /// <summary>
        /// Calculates a dollar amount based on the used data passed in by the NIC monitor.
        /// </summary>
        /// <param name="bytes">IO Bytes used by network transactions.</param>
        /// <param name="gbDataRate">Amount of GB that makes up $1.00.</param>
        /// <param name="flatRate">Rate to be charged no matter what when data reaches a specific amount.</param>
        /// <returns>Amount of moners due</returns>
        public static double CalculateBill(UInt64 bytes, int gbDataRate, double flatRate = 10.00)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            if (Suffix[i] == "MB")
            {
                return (dblSByte / 1024) / gbDataRate;
            }
            else if (Suffix[i] == "GB")
            {
                return dblSByte / gbDataRate;
            }
            else if (Suffix[i] == "TB")
            {
                return flatRate;
            }
            return 0.00;
        }
    }
}
