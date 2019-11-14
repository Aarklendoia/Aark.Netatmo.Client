using Aark.Netatmo.SDK.Models;
using System;
using System.Threading.Tasks;

namespace Aark.Netatmo.SDK
{
    /// <summary>
    /// Gives access to all Netatmo devices of a user. 
    /// </summary>
    public class NetatmoManager
    {
        private readonly APICommands _APICommands;

        /// <summary>
        /// Weather station.
        /// </summary>
        public WeatherStation WeatherStation { get; private set; }
        /// <summary>
        /// Energy data.
        /// </summary>
        public EnergyStation EnergyStation { get; private set; }
        /// <summary>
        /// Security data.
        /// </summary>
        public SecurityStation SecurityStation { get; private set; }

        /// <summary>
        /// Create a new manager for a user.
        /// </summary>
        /// <param name="applicationId"> Your application ID (more information at <see href="https://dev.netatmo.com/myaccount/createanapp"/>).</param>
        /// <param name="applicationSecret">Your application Secret (more information at <see href="https://dev.netatmo.com/myaccount/createanapp"/>).</param>
        /// <param name="username">Netatmo account username</param>
        /// <param name="password">Netatmo account password</param>
        public NetatmoManager(string applicationId, string applicationSecret, string username, string password)
        {
            _APICommands = new APICommands
            {
                ApplicationId = applicationId,
                ApplicationSecret = applicationSecret,
                Username = username,
                Password = password,
                Scope = "read_station read_thermostat write_thermostat read_camera write_camera access_camera read_presence access_presence read_homecoach"
            };
            WeatherStation = new WeatherStation(_APICommands);
            EnergyStation = new EnergyStation(_APICommands);
            SecurityStation = new SecurityStation(_APICommands);
        }

        /// <summary>
        /// Return the last error message received from Netatmo.
        /// </summary>
        /// <returns></returns>
        public string GetLastError()
        {
            return _APICommands.GetLastError();
        }

        /// <summary>
        /// Loads all data related to the weather station.
        /// </summary>
        /// <returns>Return <see cref="bool">true</see> if the data has been correctly loaded.</returns>
        public async Task<bool> LoadWeatherDataAsync()
        {
            return await WeatherStation.LoadDataAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Loads all data related to the energy station.
        /// </summary>
        /// <returns>Return <see cref="bool">true</see> if the data has been correctly loaded.</returns>
        public async Task<bool> LoadEnergyDataAsync()
        {
            return await EnergyStation.LoadDataAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Loads all data related to the security station.
        /// </summary>
        /// <returns>Return <see cref="bool">true</see> if the data has been correctly loaded.</returns>
        public async Task<bool> LoadSecurityDataAsync()
        {
            return await SecurityStation.LoadDataAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Loads next events from the event provided.
        /// </summary>
        /// <param name="homeId">Identifier of the home.</param>
        /// <param name="eventId">Identifier of the event.</param>
        /// <returns></returns>
        public async Task<bool> GetNextSecurityEvents(string homeId, string eventId)
        {
            return await SecurityStation.GetNextEvents(homeId, eventId).ConfigureAwait(false);
        }

        /// <summary>
        /// Loads events until the event provided.
        /// </summary>
        /// <param name="homeId">Identifier of the home.</param>
        /// <param name="eventId">Identifier of the event.</param>
        /// <returns></returns>
        public async Task<bool> GetSecurityEventsUntil(string homeId, string eventId)
        {
            return await SecurityStation.GetEventsUntil(homeId, eventId).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the URL of the thumbnail of a person or event.
        /// </summary>
        /// <param name="imageId">Identifier of the image available in the face of a person or in the snapshot of an event.</param>
        /// <param name="securityKey">Security key available in the face of a person or in the snapshot of an event.</param>
        /// <returns></returns>
        public static Uri GetCameraPicture(string imageId, string securityKey)
        {
            return SecurityStation.GetCameraPicture(imageId, securityKey);
        }

        /// <summary>
        /// Gets the live video stream from a camera.
        /// </summary>
        /// <param name="cameraId">Identifier of the camera.</param>
        /// <returns></returns>
        public async Task<Uri> GetLiveStream(string cameraId)
        {
            return await SecurityStation.GetLiveStream(cameraId).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the video on demand stream from a camera.
        /// </summary>
        /// <param name="cameraId">Identifier of the camera.</param>
        /// <param name="videoId">Identifier of the video.</param>
        /// <returns></returns>
        public async Task<Uri> GetVodStream(string cameraId, string videoId)
        {
            return await SecurityStation.GetVodStream(cameraId, videoId).ConfigureAwait(false);
        }
    }
}