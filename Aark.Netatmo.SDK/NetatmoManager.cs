using Aark.Netatmo.SDK.Models;
using Aark.Netatmo.SDK.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                Scope = "read_station read_thermostat write_thermostat read_camera write_camera access_camera read_presence access_presence read_homecoach read_smokedetector"
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
        /// Loads latest events for the person provided.
        /// </summary>
        /// <param name="homeId">Identifier of the home.</param>
        /// <param name="personId">Identifier of the person.</param>
        /// <returns></returns>
        public async Task<ObservableCollection<SecurityEvent>> GetLastSecurityEventsFor(string homeId, string personId)
        {
            return await SecurityStation.GetLastEventsFor(homeId, personId).ConfigureAwait(false);
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

        /// <summary>
        /// Indicates that the people on the list provided are at home.
        /// </summary>
        /// <param name="homeId">Identifier of the home.</param>
        /// <param name="personIds">list of identifiers of the persons.</param>
        /// <returns></returns>
        public async Task<bool> SetPersonsAtHome(string homeId, List<string> personIds)
        {
            return await SecurityStation.SetPersonsHome(homeId, personIds).ConfigureAwait(false);
        }

        /// <summary>
        /// Indicates that the people provided is away from home.
        /// </summary>
        /// <param name="homeId">Identifier of the home.</param>
        /// <param name="personId">Iidentifiers of the person.</param>
        /// <returns></returns>
        public async Task<bool> SetPersonAwayFromHome(string homeId, string personId)
        {
            return await SecurityStation.SetPersonAway(homeId, personId).ConfigureAwait(false);
        }

        /// <summary>
        /// Indicates that all the people are away and the home is empty.
        /// </summary>
        /// <param name="homeId">Identifier of the home.</param>
        /// <returns></returns>
        public async Task<bool> SetHomeEmpty(string homeId)
        {
            return await SecurityStation.SetHomeEmpty(homeId).ConfigureAwait(false);
        }

        /// <summary>
        /// Links a callback url to a user.
        /// </summary>
        /// <param name="url">Your webhook callback url . Only http (80) and https (443) port can be used.</param>
        /// <returns></returns>
        public async Task<bool> RegisterWebHook(Uri url)
        {
            return await SecurityStation.RegisterWebHook(url).ConfigureAwait(false);
        }

        /// <summary>
        /// Dissociates a webhook from a user.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UnregisterWebHook()
        {
            return await SecurityStation.UnregisterWebHook().ConfigureAwait(false);
        }
    }
}