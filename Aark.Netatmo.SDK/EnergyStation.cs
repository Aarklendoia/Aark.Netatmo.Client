using Aark.Netatmo.SDK.Energy;
using Aark.Netatmo.SDK.Helpers;
using Aark.Netatmo.SDK.Models;
using Aark.Netatmo.SDK.Models.Energy;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace Aark.Netatmo.SDK
{
    /// <summary>
    /// Netatmo Energy station.
    /// </summary>
    public class EnergyStation
    {
        private readonly APICommands _aPICommands;

        /// <summary>
        /// List of the devices.
        /// </summary>
        public ObservableCollection<EnergyDevice> Devices { get; private set; } = new ObservableCollection<EnergyDevice>();

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

        /// <summary>
        /// List of the connected valves.
        /// </summary>
        public ObservableCollection<Valve> Valves { get; private set; }

        internal EnergyStation(APICommands aPICommands)
        {
            _aPICommands = aPICommands;
        }

        internal async Task<bool> LoadDataAsync()
        {
            var homesData = await _aPICommands.GetHomesData().ConfigureAwait(false);
            if (homesData == null)
                return false;
            Mail = homesData.Body.User.Email;
            Language = homesData.Body.User.Language;
            CultureInfo = new CultureInfo(homesData.Body.User.Locale);
            Unit = homesData.Body.User.UnitSystem.ToUnitSystem();
            WindUnit = homesData.Body.User.UnitWind.ToWindUnit();
            PressureUnit = homesData.Body.User.UnitPressure.ToPressureUnit();
            FeelLikeTemperature = homesData.Body.User.FeelLikeAlgorithm.ToFeelLikeAlgo();
            foreach (HomesData.HomeData home in homesData.Body.Homes)
            {
                EnergyDevice energyDevice = new EnergyDevice(_aPICommands);

                // Search for relays.
                foreach (HomesData.HomesModule module in home.Modules)
                {
                    if (module.Type.ToModuleType() == ModuleType.Relay)
                    {
                        energyDevice.Relay.Id = module.Id;
                        energyDevice.Relay.Name = module.Name;
                        energyDevice.Relay.SetupDate = module.SetupDate.ToLocalDateTime();
                        break;
                    }
                }
                // Search for thermostats and valves.
                foreach (HomesData.HomesModule module in home.Modules)
                {
                    switch (module.Type.ToModuleType())
                    {
                        case ModuleType.Valve:
                            Valve valve = new Valve
                            {
                                Id = module.Id,
                                Name = module.Name,
                                RoomId = module.RoomId,
                                SetupDate = module.SetupDate.ToLocalDateTime()
                            };
                            energyDevice.Relay.Valves.Add(valve);
                            break;
                        case ModuleType.Thermostat:
                            Thermostat thermostat = new Thermostat()
                            {
                                Id = module.Id,
                                Name = module.Name,
                                RoomId = module.RoomId,
                                SetupDate = module.SetupDate.ToLocalDateTime()
                            };
                            energyDevice.Relay.Thermostats.Add(thermostat);
                            break;
                    }
                }
                Devices.Add(energyDevice);
            }
            return true;
        }
    }
}
