namespace ServMonAPI.Enums
{
    /// <summary>
    /// Enum containing the three listening states for a NICDevice when transmitting IO.
    /// </summary>
    public enum NICDeviceTrafficType
    {
        /// <summary>
        /// Only monitor sent packets.
        /// </summary>
        Egress,

        /// <summary>
        /// Only monitor received packets.
        /// </summary>
        Ingress,

        /// <summary>
        /// Monitor sent and received packets.
        /// </summary>
        IngressEgress,
    }
}
