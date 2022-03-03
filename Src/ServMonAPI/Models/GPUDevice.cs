namespace ServMonAPI.Models
{
    /// <summary>
    /// Model class that represents a WMI GPUObject.
    /// </summary>
    public class GPUDevice
    {
        /// <summary>
        /// Dictionary containing all GPU device properties supplied via WMI.
        /// </summary>
        public Dictionary<string, string> GPUDeviceProperties { get; private set; }

        /// <summary>
        /// Initial constructor for the GPUDevice class.
        /// </summary>
        public GPUDevice(Dictionary<string, string> GraphicsDeviceProperties)
        {
            GPUDeviceProperties = GraphicsDeviceProperties;
        }
    }
}
