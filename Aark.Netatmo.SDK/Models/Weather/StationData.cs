using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aark.Netatmo.SDK.Models.Weather
{
    internal partial class StationData
    {
        [JsonProperty("body")]
        internal StationDataBody Body { get; set; }

        [JsonProperty("status")]
        internal string Status { get; set; }

        [JsonProperty("time_exec")]
        internal double TimeExec { get; set; }

        [JsonProperty("time_server")]
        [JsonConverter(typeof(ParseStringConverter))]
        internal long TimeServer { get; set; }
    }

    internal partial class StationDataBody
    {
        [JsonProperty("devices")]
        internal List<Device> Devices { get; set; }

        [JsonProperty("user")]
        internal User User { get; set; }
    }

    internal partial class Device
    {
        [JsonProperty("_id")]
        internal string Id { get; set; }

        [JsonProperty("cipher_id")]
        internal string CipherId { get; set; }

        [JsonProperty("station_name")]
        internal string StationName { get; set; }

        [JsonProperty("type")]
        internal string Type { get; set; }

        [JsonProperty("wifi_status")]
        internal long WifiStatus { get; set; }

        [JsonProperty("reachable")]
        internal bool Reachable { get; set; }

        [JsonProperty("WifiStrength")]
        internal long WifiStrength { get; set; }

        [JsonProperty("module_name")]
        internal string ModuleName { get; set; }

        [JsonProperty("co2_calibrating")]
        internal bool Co2Calibrating { get; set; }

        [JsonProperty("firmware")]
        internal long Firmware { get; set; }

        [JsonProperty("last_upgrade")]
        [JsonConverter(typeof(ParseStringConverter))]
        internal long LastUpgrade { get; set; }

        [JsonProperty("date_setup")]
        [JsonConverter(typeof(ParseStringConverter))]
        internal long DateSetup { get; set; }

        [JsonProperty("last_setup")]
        [JsonConverter(typeof(ParseStringConverter))]
        internal long LastSetup { get; set; }

        [JsonProperty("last_status_store")]
        [JsonConverter(typeof(ParseStringConverter))]
        internal long LastStatusStore { get; set; }

        [JsonProperty("dashboard_data")]
        internal DeviceDashboardData DashboardData { get; set; }

        [JsonProperty("data_type")]
        internal List<string> DataType { get; set; }

        [JsonProperty("place")]
        internal Place Place { get; set; }

        [JsonProperty("modules")]
        internal List<WeatherModule> Modules { get; set; }
    }

    internal partial class DeviceDashboardData
    {
        [JsonProperty("CO2")]
        internal long Co2 { get; set; }

        [JsonProperty("Humidity")]
        internal long Humidity { get; set; }

        [JsonProperty("Noise")]
        internal long Noise { get; set; }

        [JsonProperty("AbsolutePressure")]
        internal double AbsolutePressure { get; set; }

        [JsonProperty("Pressure")]
        internal double Pressure { get; set; }

        [JsonProperty("pressure_trend")]
        internal string PressureTrend { get; set; }

        [JsonProperty("Temperature")]
        internal double Temperature { get; set; }

        [JsonProperty("temp_trend")]
        internal string TempTrend { get; set; }

        [JsonProperty("min_temp")]
        internal double MinTemp { get; set; }

        [JsonProperty("max_temp")]
        internal double MaxTemp { get; set; }

        [JsonProperty("date_min_temp")]
        [JsonConverter(typeof(ParseStringConverter))]
        internal long DateMinTemp { get; set; }

        [JsonProperty("date_max_temp")]
        [JsonConverter(typeof(ParseStringConverter))]
        internal long DateMaxTemp { get; set; }

        [JsonProperty("time_utc")]
        [JsonConverter(typeof(ParseStringConverter))]
        internal long TimeUtc { get; set; }
    }

    internal partial class WeatherModule
    {
        [JsonProperty("_id")]
        internal string Id { get; set; }

        [JsonProperty("type")]
        internal string Type { get; set; }

        [JsonProperty("module_name")]
        internal string ModuleName { get; set; }

        [JsonProperty("rf_status")]
        internal long RfStatus { get; set; }

        [JsonProperty("RfStrength")]
        internal long RfStrength { get; set; }

        [JsonProperty("battery_percent")]
        internal long BatteryPercent { get; set; }

        [JsonProperty("reachable")]
        internal bool Reachable { get; set; }

        [JsonProperty("battery_vp")]
        internal long BatteryVp { get; set; }

        [JsonProperty("BatteryStatus")]
        internal long BatteryStatus { get; set; }

        [JsonProperty("firmware")]
        internal long Firmware { get; set; }

        [JsonProperty("last_message")]
        [JsonConverter(typeof(ParseStringConverter))]
        internal long LastMessage { get; set; }

        [JsonProperty("last_seen")]
        [JsonConverter(typeof(ParseStringConverter))]
        internal long LastSeen { get; set; }

        [JsonProperty("last_setup")]
        [JsonConverter(typeof(ParseStringConverter))]
        internal long LastSetup { get; set; }

        [JsonProperty("data_type")]
        internal List<string> DataType { get; set; }

        [JsonProperty("dashboard_data", NullValueHandling = NullValueHandling.Ignore)]
        internal ModuleDashboardData DashboardData { get; set; }
    }

    internal partial class ModuleDashboardData
    {
        [JsonProperty("time_utc")]
        internal long TimeUtc { get; set; }

        [JsonProperty("Temperature", NullValueHandling = NullValueHandling.Ignore)]
        internal double? Temperature { get; set; }

        [JsonProperty("Humidity", NullValueHandling = NullValueHandling.Ignore)]
        internal long? Humidity { get; set; }

        [JsonProperty("min_temp", NullValueHandling = NullValueHandling.Ignore)]
        internal double? MinTemp { get; set; }

        [JsonProperty("max_temp", NullValueHandling = NullValueHandling.Ignore)]
        internal double? MaxTemp { get; set; }

        [JsonProperty("date_min_temp", NullValueHandling = NullValueHandling.Ignore)]
        internal long? DateMinTemp { get; set; }

        [JsonProperty("date_max_temp", NullValueHandling = NullValueHandling.Ignore)]
        internal long? DateMaxTemp { get; set; }

        [JsonProperty("temp_trend", NullValueHandling = NullValueHandling.Ignore)]
        internal string TempTrend { get; set; }

        [JsonProperty("CO2", NullValueHandling = NullValueHandling.Ignore)]
        internal long? Co2 { get; set; }

        [JsonProperty("WindStrength", NullValueHandling = NullValueHandling.Ignore)]
        internal long? WindStrength { get; set; }

        [JsonProperty("WindAngle", NullValueHandling = NullValueHandling.Ignore)]
        internal long? WindAngle { get; set; }

        [JsonProperty("GustStrength", NullValueHandling = NullValueHandling.Ignore)]
        internal long? GustStrength { get; set; }

        [JsonProperty("GustAngle", NullValueHandling = NullValueHandling.Ignore)]
        internal long? GustAngle { get; set; }

        [JsonProperty("max_wind_str", NullValueHandling = NullValueHandling.Ignore)]
        internal long? MaxWindStr { get; set; }

        [JsonProperty("max_wind_angle", NullValueHandling = NullValueHandling.Ignore)]
        internal long? MaxWindAngle { get; set; }

        [JsonProperty("date_max_wind_str", NullValueHandling = NullValueHandling.Ignore)]
        internal long? DateMaxWindStr { get; set; }

        [JsonProperty("Rain", NullValueHandling = NullValueHandling.Ignore)]
        internal double? Rain { get; set; }

        [JsonProperty("sum_rain_1", NullValueHandling = NullValueHandling.Ignore)]
        internal double? SumRain1 { get; set; }

        [JsonProperty("sum_rain_24", NullValueHandling = NullValueHandling.Ignore)]
        internal double? SumRain24 { get; set; }
    }

    internal partial class Place
    {
        [JsonProperty("altitude")]
        internal long Altitude { get; set; }

        [JsonProperty("city")]
        internal string City { get; set; }

        [JsonProperty("country")]
        internal string Country { get; set; }

        [JsonProperty("timezone")]
        internal string Timezone { get; set; }

        [JsonProperty("location")]
        internal List<double> Location { get; set; }
    }

    internal partial class User
    {
        [JsonProperty("administrative")]
        internal Administrative Administrative { get; set; }

        [JsonProperty("mail")]
        internal string Mail { get; set; }
    }

    internal partial class Administrative
    {
        [JsonProperty("country")]
        internal string Country { get; set; }

        [JsonProperty("feel_like_algo")]
        internal long FeelLikeAlgo { get; set; }

        [JsonProperty("lang")]
        internal string Lang { get; set; }

        [JsonProperty("pressureunit")]
        internal long Pressureunit { get; set; }

        [JsonProperty("reg_locale")]
        internal string RegLocale { get; set; }

        [JsonProperty("unit")]
        internal long Unit { get; set; }

        [JsonProperty("windunit")]
        internal long Windunit { get; set; }
    }

    internal partial class StationData
    {
        internal static StationData FromJson(string json) => JsonConvert.DeserializeObject<StationData>(json, StationDataConverter.Settings);
    }

    internal static class StationDataSerialize
    {
        internal static string ToJson(this StationData self) => JsonConvert.SerializeObject(self, StationDataConverter.Settings);
    }

    internal static class StationDataConverter
    {
        internal static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
