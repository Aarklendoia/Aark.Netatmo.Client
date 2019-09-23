using Aark.Netatmo.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aark.Netatmo.SDK.Energy
{
    /// <summary>
    /// Common properties for a radio connected Energy device.
    /// </summary>
    public abstract class CommonEnergyRadioDevice: ICommonEnergyDevice
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
        /// Firmware version.
        /// </summary>
        public long Firmware { get; private set; }
        /// <summary>
        /// Strength of the radio signal.
        /// </summary>
        public RadioFrequencyStatus RadioFrequenceStrength { get; internal set; }
    }
}
