using Aark.Netatmo.SDK.Helpers;
using System;

namespace Aark.Netatmo.SDK.Energy
{
    /// <summary>
    /// Connected valve of Netatmo.
    /// </summary>
    public class Valve : CommonEnergyRadioDevice
    {
        /// <summary>
        /// Indicates whether the device is reachable.
        /// </summary>
        public bool Reachable { get; set; }
        /// <summary>
        /// Temprature measured by the valve.
        /// </summary>
        public long MeasuredTemperature { get; set; }
        /// <summary>
        /// Indicates whether the valve is being heated.
        /// </summary>
        public long HeatingPowerRequest { get; set; }
        /// <summary>
        /// Setpoint temperature.
        /// </summary>
        public long SetpointTemperature { get; set; }
        /// <summary>
        /// Setpoint mode.
        /// </summary>
        public SetpointMode SetpointMode { get; set; }
        /// <summary>
        /// Start date for the endpoint.
        /// </summary>
        public DateTime SetpointStartTime { get; set; }
        /// <summary>
        /// End date for the endpoint.
        /// </summary>
        public DateTime SetpointEndTime { get; set; }
        /// <summary>
        /// Indicates whether the valve is in the heating anticipation phase.
        /// </summary>
        public bool Anticipating { get; set; }
        /// <summary>
        /// Indicates whether the valve has detected an open window.
        /// </summary>
        public bool OpenWindow { get; set; }
        /// <summary>
        /// Valve battery level.
        /// </summary>
        public BatteryStatus BatteryLevel { get; set; }
        /// <summary>
        /// Number displayed during the pairing with the relay.
        /// </summary>
        public long RadioId { get; set; }
        /// <summary>
        /// Id of the room in which the valve is located.
        /// </summary>
        public long? RoomId { get; set; }
    }
}
