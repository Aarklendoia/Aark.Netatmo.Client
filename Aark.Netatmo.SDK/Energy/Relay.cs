using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Aark.Netatmo.SDK.Energy
{
    /// <summary>
    /// Thermostat relay of Netatmo.
    /// </summary>
    public class Relay : CommonEnergyDevice
    {
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
