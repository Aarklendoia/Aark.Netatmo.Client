using Aark.Netatmo.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;

namespace Aark.Netatmo.SDK.Energy
{
    /// <summary>
    /// A Home contains Room, Modules and room-by-room heating schedules.
    /// </summary>
    public abstract class Home
    {
        /// <summary>
        /// Id of the home.
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// Name of the home.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Altitude of the home.
        /// </summary>
        public long Altitude { get; private set; }
        /// <summary>
        /// Latitude of the home.
        /// </summary>
        public long Latitude { get; private set; }
        /// <summary>
        /// Longitude of th home.
        /// </summary>
        public long Longitude { get; private set; }
        /// <summary>
        /// Localisation informations for the home.
        /// </summary>
        public RegionInfo RegionInfo { get; private set; }
        /// <summary>
        /// Timezone of th home.
        /// </summary>
        public string TimeZone { get; internal set; } // TODO : convert to timezone
        /// <summary>
        /// Default duration of a manual setpoint.
        /// </summary>
        public long DefaultDurationSetpoint { get; private set; }
        /// <summary>
        /// Thermostat operating mode.
        /// </summary>
        public ThermostatMode ThermostatMode { get; private set; }
        /// <summary>
        /// List of heating schedules.
        /// </summary>
        public ObservableCollection<Schedule> Schedules { get; private set; }
        /// <summary>
        /// Time of stopping the selected thermostat operating mode.
        /// </summary>
        public DateTime ThermostatModeEndTime { get; private set; }
        /// <summary>
        /// Rooms list.
        /// </summary>
        public ObservableCollection<Room> Rooms { get; private set; }

        // TODO : add modules
    }
}
