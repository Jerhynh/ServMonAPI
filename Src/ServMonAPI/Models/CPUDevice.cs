using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServMonAPI.Models
{
    /// <summary>
    /// Model class that represents a WMI CPUObject.
    /// </summary>
    public class CPUDevice
    {
        /// <summary>
        /// Dictionary containing all CPU device properties supplied via WMI.
        /// </summary>
        public Dictionary<string,string> ProcessorDeviceProperties { get; private set; }

        /// <summary>
        /// Initial constructor for the CPUDevice class.
        /// </summary>
        /// <param name="ProcessorDeviceProperties">Dictionary containing all CPU propertys retrieved from WMI.</param>
        public CPUDevice(Dictionary<string, string> ProcessorDeviceProperties)
        {
            this.ProcessorDeviceProperties = ProcessorDeviceProperties;
        }
    }
}
