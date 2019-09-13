namespace Aark.Netatmo.SDK.Helpers
{
    /// <summary>
    /// Status of the transmission signal.
    /// </summary>
    public enum SignalStatus
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown,
        /// <summary>
        /// Very bad signal.
        /// </summary>
        VeryBad,
        /// <summary>
        /// bad signal.
        /// </summary>
        Bad,
        /// <summary>
        /// Average signal.
        /// </summary>
        Average,
        /// <summary>
        /// Good signal.
        /// </summary>
        Good,
        /// <summary>
        /// Very good signal.
        /// </summary>
        VeryGood
    }
    /// <summary>
    /// Status of the battery of a device.
    /// </summary>
    public enum BatteryStatus
    {
        /// <summary>
        /// Full.
        /// </summary>
        Full,
        /// <summary>
        /// High.
        /// </summary>
        High,
        /// <summary>
        /// Medium.
        /// </summary>
        Medium,
        /// <summary>
        /// Low.
        /// </summary>
        Low,
        /// <summary>
        /// Empty.
        /// </summary>
        Empty
    }
    /// <summary>
    /// Status of the radio frequency signal.
    /// </summary>
    public enum RadioFrequencyStatus
    {
        /// <summary>
        /// Low.
        /// </summary>
        Low,
        /// <summary>
        /// Medium.
        /// </summary>
        Medium,
        /// <summary>
        /// High.
        /// </summary>
        High,
        /// <summary>
        /// Full.
        /// </summary>
        Full
    }
    /// <summary>
    /// Status of the wifi signal.
    /// </summary>
    public enum WifiStatus
    {
        /// <summary>
        /// Good.
        /// </summary>
        Good,
        /// <summary>
        /// Medium.
        /// </summary>
        Medium,
        /// <summary>
        /// Poor.
        /// </summary>
        Poor
    }

    /// <summary>
    /// Tools to manage status.
    /// </summary>
    public static class StatusHelper
    {
        internal static SignalStatus ToSignalStatus(this long value)
        {
            switch (value)
            {
                case long result when (result > 80):
                    return SignalStatus.VeryBad;
                case long result when (result > 60 && result <= 80):
                    return SignalStatus.Bad;
                case long result when (result > 40 && result <= 60):
                    return SignalStatus.Average;
                case long result when (result > 20 && result <= 40):
                    return SignalStatus.Good;
                case long result when (result <= 20):
                    return SignalStatus.VeryGood;
                default:
                    return SignalStatus.Unknown;
            }
        }

        internal static BatteryStatus ToOutdoorBatteryStatus(this long value)
        {
            switch (value)
            {
                case long result when (result > 5500):
                    return BatteryStatus.Full;
                case long result when (result > 5000 && result <= 5500):
                    return BatteryStatus.High;
                case long result when (result > 4500 && result <= 5000):
                    return BatteryStatus.Medium;
                case long result when (result > 4000 && result <= 4500):
                    return BatteryStatus.Low;
                default:
                    return BatteryStatus.Empty;
            }
        }

        internal static BatteryStatus ToIndoorBatteryStatus(this long value)
        {
            switch (value)
            {
                case long result when (result > 5640):
                    return BatteryStatus.Full;
                case long result when (result > 5280 && result <= 5640):
                    return BatteryStatus.High;
                case long result when (result > 4920 && result <= 5280):
                    return BatteryStatus.Medium;
                case long result when (result > 4560 && result <= 4920):
                    return BatteryStatus.Low;
                default:
                    return BatteryStatus.Empty;
            }
        }

        internal static BatteryStatus ToRainGaugeBatteryStatus(this long value)
        {
            switch (value)
            {
                case long result when (result > 5500):
                    return BatteryStatus.Full;
                case long result when (result > 5000 && result <= 5500):
                    return BatteryStatus.High;
                case long result when (result > 4500 && result <= 5000):
                    return BatteryStatus.Medium;
                case long result when (result > 4000 && result <= 4500):
                    return BatteryStatus.Low;
                default:
                    return BatteryStatus.Empty;
            }
        }

        internal static BatteryStatus ToAnenometerBatteryStatus(this long value)
        {
            switch (value)
            {
                case long result when (result > 5590):
                    return BatteryStatus.Full;
                case long result when (result > 5180 && result <= 5590):
                    return BatteryStatus.High;
                case long result when (result > 4770 && result <= 5180):
                    return BatteryStatus.Medium;
                case long result when (result > 4360 && result <= 4770):
                    return BatteryStatus.Low;
                default:
                    return BatteryStatus.Empty;
            }
        }

        internal static BatteryStatus ToThermostatBatteryStatus(this long value)
        {
            switch (value)
            {
                case long result when (result > 4100):
                    return BatteryStatus.Full;
                case long result when (result > 3600 && result <= 4100):
                    return BatteryStatus.High;
                case long result when (result > 3300 && result <= 3600):
                    return BatteryStatus.Medium;
                case long result when (result > 3000 && result <= 3300):
                    return BatteryStatus.Low;
                default:
                    return BatteryStatus.Empty;
            }
        }

        internal static BatteryStatus ToValveBatteryStatus(this long value)
        {
            switch (value)
            {
                case long result when (result > 3200):
                    return BatteryStatus.Full;
                case long result when (result > 2700 && result <= 3200):
                    return BatteryStatus.High;
                case long result when (result > 2400 && result <= 2700):
                    return BatteryStatus.Medium;
                case long result when (result > 2200 && result <= 2400):
                    return BatteryStatus.Low;
                default:
                    return BatteryStatus.Empty;
            }
        }

        internal static RadioFrequencyStatus ToRadioFrequencyStatus(this long value)
        {
            switch (value)
            {
                case long result when (result <= 60):
                    return RadioFrequencyStatus.Full;
                case long result when (result > 60 && result <= 70):
                    return RadioFrequencyStatus.High;
                case long result when (result > 70 && result <= 80):
                    return RadioFrequencyStatus.Medium;
                case long result when (result > 80 && result <= 90):
                    return RadioFrequencyStatus.Low;
                default:
                    return RadioFrequencyStatus.Low;
            }
        }

        // TODO : harmoniser les états de réception
        internal static WifiStatus ToWifiStatus(this long value)
        {
            switch (value)
            {
                case long result when (result < 56):
                    return WifiStatus.Good;
                case long result when (result > 86):
                    return WifiStatus.Poor;
                default:
                    return WifiStatus.Medium;
            }
        }
    }
}
