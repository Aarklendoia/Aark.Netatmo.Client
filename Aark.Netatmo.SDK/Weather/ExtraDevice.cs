using Aark.Netatmo.SDK.Helpers;
using Aark.Netatmo.SDK.Models;
using System;

namespace Aark.Netatmo.SDK.Weather
{
    /// <summary>
    /// Define the common properties and functions for an extra device.
    /// </summary>
    public abstract class ExtraDevice : CommonWeatherDevice
    {
        internal string BaseId;
        /// <summary>
        /// Indicates whether the device is available.
        /// </summary>
        public bool Available { get; internal set; } = false;
        /// <summary>
        /// Percentage of battery remaining.
        /// </summary>
        public long BatteryPercent { get; internal set; }
        /// <summary>
        /// Date of the last message received.
        /// </summary>
        public DateTime LastMessage { get; internal set; }
        /// <summary>
        /// Date of the last time the device was seen.
        /// </summary>
        public DateTime LastSeen { get; internal set; }
        /// <summary>
        /// Status of the radio frequecy connection.
        /// </summary>
        public SignalStatus RadioFrequenceStatus { get; internal set; }
        /// <summary>
        /// Status of the battery.
        /// </summary>
        public BatteryStatus BatteryStatus { get; internal set; }

        internal ExtraDevice(APICommands aPICommands) : base(aPICommands)
        {

        }

        internal abstract void Load(WeatherModule weatherModule, string baseId);
    }
}