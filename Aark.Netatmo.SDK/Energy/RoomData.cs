namespace Aark.Netatmo.SDK.Energy
{
    /// <summary>
    /// Measured and planned temperature data of a room.
    /// </summary>
    public class RoomData
    {
        /// <summary>
        /// Id of the corresponding room.
        /// </summary>
        public string RoomId { get; private set; }
        /// <summary>
        /// Measured temperature.
        /// </summary>
        public double Temperature { get; private set; }
        /// <summary>
        /// Setpoint temperature.
        /// </summary>
        public double SetpointTemperature { get; private set; }
    }
}