using Aark.Netatmo.SDK.Helpers;
using Aark.Netatmo.SDK.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Aark.Netatmo.SDK.Weather
{
    /// <summary>
    /// Define the list of devices of the weather station. 
    /// </summary>
    public class WeatherDevice
    {
        /// <summary>
        /// Base module of the weather station.
        /// </summary>
        public Base Base { get; private set; }
        /// <summary>
        /// Outdoor module of the weather station.
        /// </summary>
        public OutdoorModule OutdoorModule { get; private set; }
        /// <summary>
        /// First indoor module of the weather station.
        /// </summary>
        public IndoorModule IndoorModule1 { get; private set; }
        /// <summary>
        /// Second indoor module of the weather station.
        /// </summary>
        public IndoorModule IndoorModule2 { get; private set; }
        /// <summary>
        /// Third indoor module of the weather station.
        /// </summary>
        public IndoorModule IndoorModule3 { get; private set; }
        /// <summary>
        /// Anenometer module of the weather station.
        /// </summary>
        public Anemometer Anemometer { get; private set; }
        /// <summary>
        /// Raingauge module of the weather station.
        /// </summary>
        public RainGauge RainGauge { get; private set; }
        /// <summary>
        /// Location informations of the weather station.
        /// </summary>
        public Location Place { get; internal set; }

        internal WeatherDevice(APICommands aPICommands)
        {
            Base = new Base(aPICommands);
            OutdoorModule = new OutdoorModule(aPICommands);
            IndoorModule1 = new IndoorModule(aPICommands);
            IndoorModule2 = new IndoorModule(aPICommands);
            IndoorModule3 = new IndoorModule(aPICommands);
            Anemometer = new Anemometer(aPICommands);
            RainGauge = new RainGauge(aPICommands);
        }
    }
}
