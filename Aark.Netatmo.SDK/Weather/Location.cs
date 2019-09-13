using System;
using System.Collections.Generic;
using System.Text;

namespace Aark.Netatmo.SDK.Weather
{
    /// <summary>
    /// Define the location of the weather station.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Altitude of the weather station.
        /// </summary>
        public long Altitude { get; internal set; }
        /// <summary>
        /// City of the weather station.
        /// </summary>
        public string City { get; internal set; }
        /// <summary>
        /// Country of the weather station.
        /// </summary>
        public string Country { get; internal set; }
        /// <summary>
        /// Timezone of the weather station.
        /// </summary>
        public string TimeZone { get; internal set; }
        /// <summary>
        /// Latitude of the weather station.
        /// </summary>
        public double Latitude { get; internal set; }
        /// <summary>
        /// Longitude of the weather station.
        /// </summary>
        public double Longitude { get; internal set; }
    }
}
