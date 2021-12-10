namespace ServMonAPI.Models
{
    public class NICDevice
    {
        public Dictionary<string, string> NICDeviceProperties { get; private set; }

        public NICDevice(Dictionary<string, string> NICDeviceProperties)
        {
            this.NICDeviceProperties = NICDeviceProperties;
        }

        public static string FormatMemoryBytes(UInt64 bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }
    }
}
