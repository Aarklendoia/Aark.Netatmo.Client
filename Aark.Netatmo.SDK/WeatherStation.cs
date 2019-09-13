using Aark.Netatmo.SDK.Helpers;
using Aark.Netatmo.SDK.Models;
using Aark.Netatmo.SDK.Weather;
using Aiolos.Models.Netatmo.Models;
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
                weatherDevice.Base.Id = device.Id;
                weatherDevice.Base.DateSetup = device.DateSetup.ToLocalDateTime();
                weatherDevice.Base.LastSetup = device.LastSetup.ToLocalDateTime();
                weatherDevice.Base.Name = device.ModuleName;
                weatherDevice.Base.Firmware = device.Firmware;
                weatherDevice.Base.LastUpgrade = device.LastSetup.ToLocalDateTime();
                weatherDevice.Base.WifiStatus = device.WifiStatus.ToSignalStatus();
                weatherDevice.Base.Reachable = device.Reachable;
                weatherDevice.Base.CO2Calibrating = device.Co2Calibrating;
                weatherDevice.Base.StationName = device.StationName;
                if (weatherDevice.Base.Reachable)
                {
                    weatherDevice.Base.Time = device.DashboardData.TimeUtc.ToLocalDateTime();
                    weatherDevice.Base.Temperature = device.DashboardData.Temperature;
                    weatherDevice.Base.TemperatureMax = device.DashboardData.MaxTemp;
                    weatherDevice.Base.TemperatureMaxDate = device.DashboardData.DateMaxTemp.ToLocalDateTime();
                    weatherDevice.Base.TemperatureMin = device.DashboardData.MinTemp;
                    weatherDevice.Base.TemperatureMinDate = device.DashboardData.DateMinTemp.ToLocalDateTime();
                    weatherDevice.Base.TemperatureTrend = device.DashboardData.TempTrend.ToTrend();
                    weatherDevice.Base.Co2 = device.DashboardData.Co2;
                    weatherDevice.Base.Humidity = device.DashboardData.Humidity;
                    weatherDevice.Base.Noise = device.DashboardData.Noise;
                    weatherDevice.Base.Pressure = device.DashboardData.Pressure;
                    weatherDevice.Base.AbsolutePressure = device.DashboardData.AbsolutePressure;
                    weatherDevice.Base.PresureTrend = device.DashboardData.PressureTrend.ToTrend();
                }
                foreach (WeatherModule module in device.Modules)
                {
                    switch (module.Type.ToModuleType())
                    {
                        case ModuleType.Outdoor:
                            weatherDevice.OutdoorModule.Available = true;
                            weatherDevice.OutdoorModule.BaseId = weatherDevice.Base.Id;
                            weatherDevice.OutdoorModule.Id = module.Id;
                            weatherDevice.OutdoorModule.Name = module.ModuleName;
                            weatherDevice.OutdoorModule.LastSetup = module.LastSetup.ToLocalDateTime();
                            weatherDevice.OutdoorModule.BatteryPercent = module.BatteryPercent;
                            weatherDevice.OutdoorModule.Reachable = module.Reachable;
                            weatherDevice.OutdoorModule.Firmware = module.Firmware;
                            weatherDevice.OutdoorModule.LastMessage = module.LastMessage.ToLocalDateTime();
                            weatherDevice.OutdoorModule.LastSeen = module.LastSeen.ToLocalDateTime();
                            weatherDevice.OutdoorModule.RadioFrequenceStatus = module.RfStatus.ToSignalStatus();
                            weatherDevice.OutdoorModule.BatteryStatus = module.BatteryVp.ToOutdoorBatteryStatus();
                            if (weatherDevice.OutdoorModule.Reachable)
                            {
                                weatherDevice.OutdoorModule.Time = module.DashboardData.TimeUtc.ToLocalDateTime();
                                weatherDevice.OutdoorModule.Temperature = module.DashboardData.Temperature;
                                weatherDevice.OutdoorModule.TemperatureMin = module.DashboardData.MinTemp;
                                weatherDevice.OutdoorModule.TemperatureMinDate = module.DashboardData.DateMinTemp.ToLocalDateTime();
                                weatherDevice.OutdoorModule.TemperatureMax = module.DashboardData.MaxTemp;
                                weatherDevice.OutdoorModule.TemperatureMaxDate = module.DashboardData.DateMaxTemp.ToLocalDateTime();
                                weatherDevice.OutdoorModule.TemperatureTrend = module.DashboardData.TempTrend;
                                weatherDevice.OutdoorModule.Humidity = module.DashboardData.Humidity;
                            }
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
                            indoorModule.Available = true;
                            indoorModule.BaseId = weatherDevice.Base.Id;
                            indoorModule.Id = module.Id;
                            indoorModule.Name = module.ModuleName;
                            indoorModule.LastSetup = module.LastSetup.ToLocalDateTime();
                            indoorModule.BatteryPercent = module.BatteryPercent;
                            indoorModule.Reachable = module.Reachable;
                            indoorModule.Firmware = module.Firmware;
                            indoorModule.LastMessage = module.LastMessage.ToLocalDateTime();
                            indoorModule.LastSeen = module.LastSeen.ToLocalDateTime();
                            indoorModule.RadioFrequenceStatus = module.RfStatus.ToSignalStatus();
                            indoorModule.BatteryStatus = module.BatteryVp.ToIndoorBatteryStatus();
                            if (indoorModule.Reachable)
                            {
                                indoorModule.Time = module.DashboardData.TimeUtc.ToLocalDateTime();
                                indoorModule.Temperature = module.DashboardData.Temperature;
                                indoorModule.TemperatureMin = module.DashboardData.MinTemp;
                                indoorModule.TemperatureMinDate = module.DashboardData.DateMinTemp.ToLocalDateTime();
                                indoorModule.TemperatureMax = module.DashboardData.MaxTemp;
                                indoorModule.TemperatureMaxDate = module.DashboardData.DateMaxTemp.ToLocalDateTime();
                                indoorModule.TemperatureTrend = module.DashboardData.TempTrend;
                                indoorModule.Humidity = module.DashboardData.Humidity;
                            }
                            break;
                        case ModuleType.RainGauge:
                            weatherDevice.RainGauge.Available = true;
                            weatherDevice.RainGauge.BaseId = weatherDevice.Base.Id;
                            weatherDevice.RainGauge.Id = module.Id;
                            weatherDevice.RainGauge.Name = module.ModuleName;
                            weatherDevice.RainGauge.LastSetup = module.LastSetup.ToLocalDateTime();
                            weatherDevice.RainGauge.BatteryPercent = module.BatteryPercent;
                            weatherDevice.RainGauge.Reachable = module.Reachable;
                            weatherDevice.RainGauge.Firmware = module.Firmware;
                            weatherDevice.RainGauge.LastMessage = module.LastMessage.ToLocalDateTime();
                            weatherDevice.RainGauge.LastSeen = module.LastSeen.ToLocalDateTime();
                            weatherDevice.RainGauge.RadioFrequenceStatus = module.RfStatus.ToSignalStatus();
                            weatherDevice.RainGauge.BatteryStatus = module.BatteryVp.ToRainGaugeBatteryStatus();
                            if (weatherDevice.RainGauge.Reachable)
                            {
                                weatherDevice.RainGauge.Time = module.DashboardData.TimeUtc.ToLocalDateTime();
                                weatherDevice.RainGauge.Rain = module.DashboardData.Rain;
                                weatherDevice.RainGauge.SumRainLastHour = module.DashboardData.SumRain1;
                                weatherDevice.RainGauge.SumRainLast24h = module.DashboardData.SumRain24;
                            }
                            break;
                        case ModuleType.Anenometer:
                            weatherDevice.Anemometer.Available = true;
                            weatherDevice.Anemometer.BaseId = weatherDevice.Base.Id;
                            weatherDevice.Anemometer.Id = module.Id;
                            weatherDevice.Anemometer.Name = module.ModuleName;
                            weatherDevice.Anemometer.LastSetup = module.LastSetup.ToLocalDateTime();
                            weatherDevice.Anemometer.BatteryPercent = module.BatteryPercent;
                            weatherDevice.Anemometer.Reachable = module.Reachable;
                            weatherDevice.Anemometer.Firmware = module.Firmware;
                            weatherDevice.Anemometer.LastMessage = module.LastMessage.ToLocalDateTime();
                            weatherDevice.Anemometer.LastSeen = module.LastSeen.ToLocalDateTime();
                            weatherDevice.Anemometer.RadioFrequenceStatus = module.RfStatus.ToSignalStatus();
                            weatherDevice.Anemometer.BatteryStatus = module.BatteryVp.ToAnenometerBatteryStatus();
                            if (weatherDevice.Anemometer.Reachable)
                            {
                                weatherDevice.Anemometer.Time = module.DashboardData.TimeUtc.ToLocalDateTime();
                                weatherDevice.Anemometer.WindStrength = module.DashboardData.WindStrength;
                                weatherDevice.Anemometer.WindAngle = module.DashboardData.WindAngle;
                                weatherDevice.Anemometer.MaxWindStrength = module.DashboardData.MaxWindStr;
                                weatherDevice.Anemometer.MaxWindAngle = module.DashboardData.MaxWindAngle;
                                weatherDevice.Anemometer.DateMaxWindStrength = module.DashboardData.DateMaxWindStr.ToLocalDateTime();
                                weatherDevice.Anemometer.GustStrength = module.DashboardData.GustStrength;
                                weatherDevice.Anemometer.GustAngle = module.DashboardData.GustAngle;
                            }
                            break;
                    }
                }
                Devices.Add(weatherDevice);
            }
            return true;
        }
    }
}
