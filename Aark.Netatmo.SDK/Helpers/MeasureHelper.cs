using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Aark.Netatmo.SDK.Helpers
{
    internal enum MeasureScale { Unknown, Max, ThirtyMinutes, OneHour, ThreeHours, OneDay, OneWeek, OneMonth }
    /// <summary>
    /// Type of measurements available for the weather station.
    /// </summary>
    [Flags]
    public enum MeasuresFilters
    {
        #region Filters available for Max measure scale
        /// <summary>
        /// Temperature.
        /// </summary>
        Temperature = 1,
        /// <summary>
        /// Co2.
        /// </summary>
        Co2 = 2,
        /// <summary>
        /// Humidity.
        /// </summary>
        Humidity = 4,
        /// <summary>
        /// Pressure.
        /// </summary>
        Pressure = 8,
        /// <summary>
        /// Noise.
        /// </summary>
        Noise = 16,
        /// <summary>
        /// Rain.
        /// </summary>
        Rain = 32,
        /// <summary>
        /// Wind strength.
        /// </summary>
        WindStrength = 64,
        /// <summary>
        /// Wind angle.
        /// </summary>
        WindAngle = 128,
        /// <summary>
        /// Gust strength.
        /// </summary>
        GustStrength = 256,
        /// <summary>
        /// Gust angle.
        /// </summary>
        GustAngle = 512,
        #endregion
        #region Filters available for Max, 30min, 1hour, 3hours measure scales
        /// <summary>
        /// Minimal temperature.
        /// </summary>
        MinTemperature = 1024,
        /// <summary>
        /// Maximal temperature.
        /// </summary>
        MaxTemperature = 2048,
        /// <summary>
        /// Minimal humidity.
        /// </summary>
        MinHumidity = 4096,
        /// <summary>
        /// Maximal humidity.
        /// </summary>
        MaxHumidity = 8192,
        /// <summary>
        /// Minimal pressure.
        /// </summary>
        MinPressure = 16384,
        /// <summary>
        /// Maximal pressure.
        /// </summary>
        MaxPressure = 32768,
        /// <summary>
        /// Minimal noise.
        /// </summary>
        MinNoise = 65536,
        /// <summary>
        /// Maximal noise.
        /// </summary>
        MaxNoise = 131072,
        /// <summary>
        /// Cumulative rainfall.
        /// </summary>
        SumRain = 262144,
        #endregion
        #region Filters avaliable for all measures scales
        /// <summary>
        /// Date maximal humidity.
        /// </summary>
        DateMaxHumidity = 524288,
        /// <summary>
        /// Date minimal humidity.
        /// </summary>
        DateMinPressure = 1048576,
        /// <summary>
        /// Date maximal pressure.
        /// </summary>
        DateMaxPressure = 2097152,
        /// <summary>
        /// Date minimal moise.
        /// </summary>
        DateMinNoise = 4194304,
        /// <summary>
        /// Date maximal moise.
        /// </summary>
        DateMaxNoise = 8388608,
        /// <summary>
        /// Date minimal Co2.
        /// </summary>
        DateMinCo2 = 16777216,
        /// <summary>
        /// Date maximal Co2.
        /// </summary>
        DateMaxCo2 = 33554432,
        /// <summary>
        /// Date maximal gust.
        /// </summary>
        DateMaxGust = 67108864
        #endregion
    }
    /// <summary>
    /// Trend.
    /// </summary>
    public enum Trend
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown,
        /// <summary>
        /// Upward trend.
        /// </summary>
        Up,
        /// <summary>
        /// Downward trend.
        /// </summary>
        Down,
        /// <summary>
        /// Stable trend.
        /// </summary>
        Stable
    }

    internal static class MeasureHelper
    {
        private static readonly ResourceManager stringManager = new ResourceManager("en-US", Assembly.GetExecutingAssembly());

        internal const MeasuresFilters MaxFilters =
            MeasuresFilters.Temperature |
            MeasuresFilters.Co2 |
            MeasuresFilters.Humidity |
            MeasuresFilters.Pressure |
            MeasuresFilters.Noise |
            MeasuresFilters.Rain |
            MeasuresFilters.WindStrength |
            MeasuresFilters.WindAngle |
            MeasuresFilters.GustStrength |
            MeasuresFilters.GustAngle;

        internal const MeasuresFilters ShortScaleFilters = MaxFilters |
             MeasuresFilters.MinTemperature |
            MeasuresFilters.MaxTemperature |
            MeasuresFilters.MinHumidity |
            MeasuresFilters.MaxHumidity |
            MeasuresFilters.MinPressure |
            MeasuresFilters.MaxPressure |
            MeasuresFilters.MinNoise |
            MeasuresFilters.MaxNoise |
            MeasuresFilters.SumRain;

        internal const MeasuresFilters LargeScaleFilters = ShortScaleFilters |
            MeasuresFilters.DateMaxHumidity |
            MeasuresFilters.DateMinPressure |
            MeasuresFilters.DateMaxPressure |
            MeasuresFilters.DateMinNoise |
            MeasuresFilters.DateMaxNoise |
            MeasuresFilters.DateMinCo2 |
            MeasuresFilters.DateMaxCo2 |
            MeasuresFilters.DateMaxGust;

        internal static string ToJsonString(this MeasureScale value)
        {
            switch (value)
            {
                case MeasureScale.Max:
                    return "max";
                case MeasureScale.OneDay:
                    return "1day";
                case MeasureScale.OneHour:
                    return "1hour";
                case MeasureScale.OneMonth:
                    return "1month";
                case MeasureScale.OneWeek:
                    return "1week";
                case MeasureScale.ThirtyMinutes:
                    return "30min";
                case MeasureScale.ThreeHours:
                    return "3hours";
                default:
                    throw new ArgumentException(stringManager.GetString("invalideValueForMeasureScale", CultureInfo.CurrentUICulture), nameof(value));
            }
        }

        internal static string ToJsonString(this MeasuresFilters value)
        {
            List<string> filters = new List<string>();
            if (value.HasFlag(MeasuresFilters.Temperature))
                filters.Add("Temperature");
            if (value.HasFlag(MeasuresFilters.Co2))
                filters.Add("CO2");
            if (value.HasFlag(MeasuresFilters.Humidity))
                filters.Add("Humidity");
            if (value.HasFlag(MeasuresFilters.Pressure))
                filters.Add("Pressure");
            if (value.HasFlag(MeasuresFilters.Noise))
                filters.Add("Noise");
            if (value.HasFlag(MeasuresFilters.Rain))
                filters.Add("Rain");
            if (value.HasFlag(MeasuresFilters.WindStrength))
                filters.Add("WindStrength");
            if (value.HasFlag(MeasuresFilters.WindAngle))
                filters.Add("WindAngle");
            if (value.HasFlag(MeasuresFilters.GustStrength))
                filters.Add("GustStrength");
            if (value.HasFlag(MeasuresFilters.GustAngle))
                filters.Add("GustAngle");
            if (value.HasFlag(MeasuresFilters.MinTemperature))
                filters.Add("min_temp");
            if (value.HasFlag(MeasuresFilters.MaxTemperature))
                filters.Add("max_temp");
            if (value.HasFlag(MeasuresFilters.MinHumidity))
                filters.Add("min_hum");
            if (value.HasFlag(MeasuresFilters.MaxHumidity))
                filters.Add("max_hum");
            if (value.HasFlag(MeasuresFilters.MinPressure))
                filters.Add("min_pressure");
            if (value.HasFlag(MeasuresFilters.MaxPressure))
                filters.Add("max_pressure");
            if (value.HasFlag(MeasuresFilters.MinNoise))
                filters.Add("min_noise");
            if (value.HasFlag(MeasuresFilters.MaxNoise))
                filters.Add("max_noise");
            if (value.HasFlag(MeasuresFilters.SumRain))
                filters.Add("sum_rain");
            if (value.HasFlag(MeasuresFilters.DateMaxHumidity))
                filters.Add("date_max_hum");
            if (value.HasFlag(MeasuresFilters.DateMinPressure))
                filters.Add("date_min_pressure");
            if (value.HasFlag(MeasuresFilters.DateMaxPressure))
                filters.Add("date_max_pressure");
            if (value.HasFlag(MeasuresFilters.DateMaxGust))
                filters.Add("date_max_gust");
            if (value.HasFlag(MeasuresFilters.DateMinNoise))
                filters.Add("date_min_noise");
            if (value.HasFlag(MeasuresFilters.DateMaxNoise))
                filters.Add("date_max_noise");
            if (value.HasFlag(MeasuresFilters.DateMinCo2))
                filters.Add("date_min_co2");
            if (value.HasFlag(MeasuresFilters.DateMaxCo2))
                filters.Add("date_max_co2");
            return String.Join(",", filters.ToArray());
        }

        internal static Trend ToTrend(this string value)
        {
            switch (value)
            {
                case "up":
                    return Trend.Up;
                case "down":
                    return Trend.Down;
                case "stable":
                    return Trend.Stable;
                default:
                    return Trend.Unknown;
            }

        }
    }
}
