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
                Scope = "read_station read_thermostat write_thermostat"
            };
            WeatherStation = new WeatherStation(_APICommands);
            EnergyStation = new EnergyStation(_APICommands);
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
            return await WeatherStation.LoadDataAsync();
        }

        /// <summary>
        /// Loads all data related to the energy station.
        /// </summary>
        /// <returns>Return <see cref="bool">true</see> if the data has been correctly loaded.</returns>
        public async Task<bool> LoadEnergyDataAsync()
        {
            return await EnergyStation.LoadDataAsync();
        }
    }
}