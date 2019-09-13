using Aark.Netatmo.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aark.Netatmo.SDK.Energy
{
    /// <summary>
    /// Common properties for an Energy device.
    /// </summary>
    public abstract class CommonEnergyDevice
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
    }
}
