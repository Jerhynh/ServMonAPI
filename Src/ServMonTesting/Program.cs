using Microsoft.Data.Sqlite;
using ServMonAPI.Enums;
using ServMonAPI.Utilities;
using System.Management;
using System.Net.NetworkInformation;


namespace Program
{
    public class Program
    {
        public static void Main()
        {
            Console.Title = "Ingress & Egress Traffic Mon Util | Starting up...";

            NetworkInterface nic = QueryNIC(); // Get nic monitoring preference from the user
            long totalIngress = 0; // Init a counter for keeping track of reported IO bytes
            long totalEgress = 0; // Init a counter for keeping track of reported IO bytes
            Console.WriteLine($"Starting bandwidth monitoring for inteface: {nic.Name}");
            for (; ; )
            {
                Task.Delay(1000).Wait();
                //var ingressBytes = NICApi.MonitorNICIO(nic, NICDeviceTrafficType.Ingress); // Optionally run both in a separate threads.
                //var egressBytes = NICApi.MonitorNICIO(nic, NICDeviceTrafficType.Egress);
                var ingressBytes = 0; // pull network sent, wait some time and calculate the change. <<<<<*************************************************
                var egressBytes = 0; 
                totalIngress += ingressBytes;
                totalEgress += egressBytes;
                Console.WriteLine($"Ingress Increase:{DataFormatting.FormatMemoryBytes(ingressBytes)} <---> Egress Increase:{DataFormatting.FormatMemoryBytes(egressBytes)}");
                Console.Title = $"Ingress & Egress Traffic Mon Util | Status: Monitoring {nic.Name} | Total Usage: {DataFormatting.FormatMemoryBytes(totalIngress + totalEgress)} - Ingress:{DataFormatting.FormatMemoryBytes(totalIngress)} Egress:{DataFormatting.FormatMemoryBytes(totalEgress)}"; // format for currency
                //Console.Title = $"ServMon Test Utility | Status: Monitoring {nic["Name"]} | Current Session Bill: {CalculateBill(IOBytesTotal, 75):c}"; // format for currency
            }
        }

        private static NetworkInterface QueryNIC()
        {
            Dictionary<int, NetworkInterface> NICDict = new();
            var firstLoop = true;
            Console.Clear();
            Console.WriteLine("Network Interfaces:");
            IEnumerable<NetworkInterface> networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            while (true)
            {
                var indice = 0;
                foreach (var nic in networkInterfaces) // NICApi.GetMonitorableNICs()
                {
                    Console.WriteLine($"Interface {indice}: {nic.Name}");
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

        //static void Main(string[] args)
        //{
        //    File.Delete("Application.db");
        //    SqliteConnection sqlite_conn;
        //    sqlite_conn = CreateConnection();
        //    CreateTable(sqlite_conn);
        //    InsertData(sqlite_conn);
        //    ReadData(sqlite_conn);
        //}

        static SqliteConnection CreateConnection()
        {

            SqliteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SqliteConnection("Data Source=Application.db;Cache=Shared");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return sqlite_conn;
        }

        static void CreateTable(SqliteConnection conn)
        {

            SqliteCommand sqlite_cmd;
            string Createsql = "CREATE TABLE DataUsage(txID varchar(40) primary key, DateTime varchar(50),Ingress varchar(25), Egress varchar(25));";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
        }

        static void InsertData(SqliteConnection conn)
        {
            SqliteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO DataUsage(txID, DateTime, Ingress, Egress) VALUES(@txID, @DateTime, @Ingress, @Egress);";
            sqlite_cmd.Parameters.AddWithValue("@txID", Guid.NewGuid().ToString());
            sqlite_cmd.Parameters.AddWithValue("@DateTime", DateTime.Today.Date.ToString());
            sqlite_cmd.Parameters.AddWithValue("@Ingress", 1000);
            sqlite_cmd.Parameters.AddWithValue("@Egress", 2000);
            sqlite_cmd.ExecuteNonQuery();
        }

        static void ReadData(SqliteConnection conn)
        {
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM DataUsage";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                int fieldCount = sqlite_datareader.FieldCount;
                for (int i = 0; i < fieldCount; i++)
                {
                    string myreader = sqlite_datareader.GetString(i);
                    Console.WriteLine(myreader);
                }
            }
            conn.Close();
        }
    }
}
