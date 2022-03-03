namespace ServMonAPI.Enums
{
    /// <summary>
    /// Enum containing the three listening states for a NICDevice when transmitting IO.
    /// </summary>
    public enum NICDeviceIOState
    {
        /// <summary>
        /// Only monitor sent packets.
        /// </summary>
        Send,

        /// <summary>
        /// Only monitor received packets.
        /// </summary>
        Receive,

        /// <summary>
        /// Monitor sent and received packets.
        /// </summary>
        SendReceive,
    }
}
