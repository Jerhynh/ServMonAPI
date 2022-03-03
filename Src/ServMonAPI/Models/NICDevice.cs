namespace ServMonAPI.Models
{
    /// <summary>
    /// Model class that represents a WMI NICObject.
    /// </summary>
    public class NICDevice
    {
        /// <summary>
        /// Dictionary containing all NIC device properties supplied via WMI.
        /// </summary>
        public Dictionary<string, string> NICDeviceProperties { get; private set; }

        /// <summary>
        /// Initial constructor for the NICDevice class.
        /// </summary>
        public NICDevice(Dictionary<string, string> NICDeviceProperties)
        {
            this.NICDeviceProperties = NICDeviceProperties;
        }
    }
}
