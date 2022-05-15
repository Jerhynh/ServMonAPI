namespace ServMonAPI.Utilities
{
    /// <summary>
    /// Class containing useful methods for formatting data values.
    /// </summary>
    public class DataFormatting
    {
        /// <summary>
        /// Determines the storage capacity suffix for the specified number of bytes supplied.
        /// </summary>
        /// <param name="bytes">Number of bytes in context.</param>
        /// <returns>A string value representing the storage capacity and suffix.</returns>
        public static string FormatMemoryBytes(ulong bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }
            return string.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }

        /// <summary>
        /// Determines the storage capacity suffix for the specified number of bytes supplied.
        /// </summary>
        /// <param name="bytes">Number of bytes in context.</param>
        /// <returns>A string value representing the storage capacity and suffix.</returns>
        public static string FormatMemoryBytes(long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }
            return string.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }
    }
}
