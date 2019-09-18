using Aark.Netatmo.SDK.Helpers;
using Aark.Netatmo.SDK.Models;
using Aark.Netatmo.SDK.Weather;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace Aark.Netatmo.SDK
{
    /// <summary>
    /// Netatmo Weather station.
    /// </summary>
    public class WeatherStation
    {
        private readonly APICommands _aPICommands;
        private DateTime _lastRefresh = DateTime.MinValue;

        /// <summary>
        /// List of the devices.
        /// </summary>
        public ObservableCollection<WeatherDevice> Devices { get; private set; } = new ObservableCollection<WeatherDevice>();

        /// <summary>
        /// mail of the user.
        /// </summary>
        public string Mail { get; private set; }
        /// <summary>
        /// Language used by the user.
        /// </summary>
        public string Language { get; private set; }
        /// <summary>
        /// Culture uses by the user.
        /// </summary>
        public CultureInfo CultureInfo { get; private set; }
        /// <summary>
        /// Informations about the country of the user.
        /// </summary>
        public RegionInfo RegionInfo { get; private set; }
        /// <summary>
        /// Unit system used by the user.
        /// </summary>
        public UnitSystem Unit { get; private set; }
        /// <summary>
        /// Wind unit used by the user.
        /// </summary>
        public WindUnit WindUnit { get; private set; }
        /// <summary>
        /// Pressure unit used by the user.
        /// </summary>
        public PressureUnit PressureUnit { get; private set; }
        /// <summary>
        /// Feeling algorithm unit used by the user.
        /// </summary>
        public FeelLikeAlgo FeelLikeTemperature { get; private set; }

        internal WeatherStation(APICommands aPICommands)
        {
            _aPICommands = aPICommands;
        }

        internal async Task<bool> LoadDataAsync()
        {
            if (_lastRefresh.AddMinutes(10).CompareTo(DateTime.Now) > 0)
                return true;
            var stationData = await _aPICommands.GetStationsData();
            if (stationData == null)
                return false;
            _lastRefresh = DateTime.Now;
            Mail = stationData.Body.User.Mail;
            Language = stationData.Body.User.Administrative.Lang;
            CultureInfo = new CultureInfo(stationData.Body.User.Administrative.RegLocale);
            RegionInfo = new RegionInfo(stationData.Body.User.Administrative.Country);
            Unit = stationData.Body.User.Administrative.Unit.ToUnitSystem();
            WindUnit = stationData.Body.User.Administrative.Windunit.ToWindUnit();
            PressureUnit = stationData.Body.User.Administrative.Pressureunit.ToPressureUnit();
            FeelLikeTemperature = stationData.Body.User.Administrative.FeelLikeAlgo.ToFeelLikeAlgo();
            foreach (Device device in stationData.Body.Devices)
            {
                WeatherDevice weatherDevice = new WeatherDevice(_aPICommands)
                {
                    Place = new Location()
                    {
                        Altitude = device.Place.Altitude,
                        City = device.Place.City,
                        Country = device.Place.Country,
                        Latitude = device.Place.Location[0],
                        Longitude = device.Place.Location[1],
                        TimeZone = device.Place.Timezone
                    }
                };
                weatherDevice.Base.Load(device);
                foreach (WeatherModule module in device.Modules)
                {
                    switch (module.Type.ToModuleType())
                    {
                        case ModuleType.Outdoor:
                            weatherDevice.OutdoorModule.Load(module, weatherDevice.Base.Id);
                            break;
                        case ModuleType.Indoor:
                            IndoorModule indoorModule;
                            if (!weatherDevice.IndoorModule1.Available)
                                indoorModule = weatherDevice.IndoorModule1;
                            else
                            if (!weatherDevice.IndoorModule2.Available)
                                indoorModule = weatherDevice.IndoorModule2;
                            else
                                indoorModule = weatherDevice.IndoorModule3;
                            indoorModule.Load(module, weatherDevice.Base.Id);
                            break;
                        case ModuleType.RainGauge:
                            weatherDevice.RainGauge.Load(module, weatherDevice.Base.Id);
                            break;
                        case ModuleType.Anenometer:
                            weatherDevice.Anemometer.Load(module, weatherDevice.Base.Id);
                            break;
                    }
                }
                Devices.Add(weatherDevice);
            }
            return true;
        }
    }
}
