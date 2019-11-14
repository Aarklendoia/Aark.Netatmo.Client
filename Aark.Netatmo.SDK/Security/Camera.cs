using Aark.Netatmo.SDK.Helpers;
using System;

namespace Aark.Netatmo.SDK.Security
{
    /// <summary>
    /// Camera.
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// Identifier of the camera.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Type of the camera.
        /// </summary>
        public CameraType Type { get; set; }
        /// <summary>
        /// Status of the camera.
        /// </summary>
        public CameraStatus Status { get; set; } // TODO Missing netatmo documentation
        /// <summary>
        /// Url of the VPN.
        /// </summary>
        public Uri VpnUrl { get; set; }
        /// <summary>
        /// Check you can access the camera locally.
        /// </summary>
        public bool IsLocal { get; set; }
        /// <summary>
        /// Status of the alimentation.
        /// </summary>
        public SDCardStatus SDCardStatus { get; set; } // TODO Missing netatmo documentation
        /// <summary>
        /// Status of the alimentation.
        /// </summary>
        public AlimStatus AlimStatus { get; set; } // TODO Missing netatmo documentation
        /// <summary>
        /// Name of the camera.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Enabling or disabling the camera requires the PIN code.
        /// </summary>
        public bool UsePinCode { get; set; }
        /// <summary>
        /// Timestamp of the last setup.
        /// </summary>
        public DateTime LastSetup { get; set; }

    }
}