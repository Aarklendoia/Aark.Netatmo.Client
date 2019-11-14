using Aark.Netatmo.SDK.Helpers;
using System;

namespace Aark.Netatmo.SDK.Security
{
    /// <summary>
    /// Define a home module.
    /// </summary>
    public class SecurityModule
    {
        /// <summary>
        /// Mac address of the module.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Type of the module.
        /// </summary>
        public SecurityModuleType Type { get; set; }
        /// <summary>
        /// Name of the module (given by the user).
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Remaining battery percentage.
        /// </summary>
        public long BatteryPercent { get; set; }
        /// <summary>
        /// Status of the module.
        /// </summary>
        public StatusSecurityModule Status { get; set; }
        /// <summary>
        /// Radio status.
        /// </summary>
        public RadioFrequencyStatus RadioStatus { get; set; }
        /// <summary>
        /// Timestamp of last move detected by the module.
        /// </summary>
        public DateTime LastActivity { get; set; }
    }
}