using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Aark.Netatmo.SDK.Energy
{
    /// <summary>
    /// Thermostat relay of Netatmo.
    /// </summary>
    public class Relay : ICommonEnergyDevice
    {
        /// <summary>
        /// Id of the device.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Name of the device.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Date on which the device was setted. 
        /// </summary>
        public DateTime SetupDate { get; set; }
        /// <summary>
        /// List of the connected valves.
        /// </summary>
        public ObservableCollection<Valve> Valves { get; private set; }
        /// <summary>
        /// List of the thermostats.
        /// </summary>
        public ObservableCollection<Thermostat> Thermostats { get; private set; }
    }
}
