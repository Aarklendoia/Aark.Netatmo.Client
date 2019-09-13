using Aark.Netatmo.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aark.Netatmo.SDK.Energy
{
    /// <summary>
    /// Common properties for a radio connected Energy device.
    /// </summary>
    public abstract class CommonEnergyRadioDevice: CommonEnergyDevice
    {
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
