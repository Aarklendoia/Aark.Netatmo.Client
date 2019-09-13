using System;
using System.Collections.Generic;
using System.Text;

namespace Aark.Netatmo.SDK.Energy
{
    /// <summary>
    /// Thermostat of Netatmo.
    /// </summary>
    public class Thermostat : CommonEnergyDevice
    {
        /// <summary>
        /// Id of the room in which the thermostat is located.
        /// </summary>
        public long? RoomId { get; set; }
    }
}
