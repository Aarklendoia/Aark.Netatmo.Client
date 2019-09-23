using System;
using System.Collections.Generic;
using System.Text;

namespace Aark.Netatmo.SDK.Security
{
    /// <summary>
    /// Common properties for a Security device.
    /// </summary>
    public interface ICommonSecurityDevice
    {
        /// <summary>
        /// Id of the device.
        /// </summary>
        string Id { get; set; }
        /// <summary>
        /// Name of the device.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Date on which the device was setted. 
        /// </summary>
        DateTime SetupDate { get; set; }
    }
}
