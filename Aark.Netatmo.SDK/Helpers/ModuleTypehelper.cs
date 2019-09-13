namespace Aark.Netatmo.SDK.Helpers
{
    /// <summary>
    /// Netatmo module type.
    /// </summary>
    public enum ModuleType
    {
        /// <summary>
        /// Default base module of the weather station.
        /// </summary>
        Base,
        /// <summary>
        /// Outdoor module.
        /// </summary>
        Outdoor,
        /// <summary>
        /// Indoor module.
        /// </summary>
        Indoor,
        /// <summary>
        /// Anenometer.
        /// </summary>
        Anenometer,
        /// <summary>
        /// Raingauge.
        /// </summary>
        RainGauge,
        /// <summary>
        /// Thermostat.
        /// </summary>
        Thermostat,
        /// <summary>
        /// Connected valve.
        /// </summary>
        Valve,
        /// <summary>
        /// Relay for connected valves.
        /// </summary>
        Relay,
        /// <summary>
        /// Indoor Welcome Camera.
        /// </summary>
        WelcomeCamera,
        /// <summary>
        /// Outdoor Presence Camera.
        /// </summary>
        PresenceCamera,
        /// <summary>
        /// Smoke detector.
        /// </summary>
        SmokeDetector
    }

    /// <summary>
    /// Tools to manage ModuleType.
    /// </summary>
    public static class ModuleTypeHelper
    {
        /// <summary>
        /// Convert a <paramref name="value"/> to a <see cref="ToModuleType"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The <see cref="ToModuleType"/> corresponding to the input <paramref name="value"/></returns>
        public static ModuleType ToModuleType(this string value)
        {
            switch (value)
            {
                case "NAMain":
                    return ModuleType.Base;
                case "NAModule1":
                    return ModuleType.Outdoor;
                case "NAModule2":
                    return ModuleType.Anenometer;
                case "NAModule3":
                    return ModuleType.RainGauge;
                case "NAModule4":
                    return ModuleType.Indoor;
                case "NATherm1":
                    return ModuleType.Thermostat;
                case "NRV":
                    return ModuleType.Valve;
                case "NAPlug":
                    return ModuleType.Relay;
                case "NACamera":
                    return ModuleType.WelcomeCamera;
                case "NOC":
                    return ModuleType.PresenceCamera;
                case "NSD":
                    return ModuleType.SmokeDetector;
                default:
                    return ModuleType.Base;
            }
        }
    }
}
