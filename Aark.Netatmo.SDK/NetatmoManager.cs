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
        /// Loads next event from the event provided.
        /// </summary>
        /// <param name="homeId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public async Task<bool> GetNextSecurityEvents(string homeId, string eventId)
        {
            return await SecurityStation.GetNextEvents(homeId, eventId).ConfigureAwait(false);
        }      

        /// <summary>
        /// Allows you to obtain the live video stream from a camera.
        /// </summary>
        /// <param name="cameraId"></param>
        /// <returns></returns>
        public async Task<Uri> GetLiveStream(string cameraId)
        {
            return await SecurityStation.GetLiveStream(cameraId).ConfigureAwait(false);
        }

        /// <summary>
        /// Allows you to obtain the video on demand stream from a camera.
        /// </summary>
        /// <param name="cameraId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<Uri> GetVodStream(string cameraId, string videoId)
        {
            return await SecurityStation.GetVodStream(cameraId, videoId).ConfigureAwait(false);
        }
    }
}