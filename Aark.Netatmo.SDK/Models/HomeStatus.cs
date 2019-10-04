using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aark.Netatmo.SDK.Models
{ 
    internal partial class HomeStatus
    {
        [JsonProperty("status")]
        internal string Status { get; set; }

        [JsonProperty("time_server")]
        internal long TimeServer { get; set; }

        [JsonProperty("body")]
        internal Body Body { get; set; }
    }

    internal partial class Body
    {
        [JsonProperty("home")]
        internal Home Home { get; set; }
    }

    internal partial class Home
    {
        [JsonProperty("id")]
        internal string Id { get; set; }

        [JsonProperty("modules")]
        internal List<Module> Modules { get; set; }

        [JsonProperty("rooms")]
        internal List<Room> Rooms { get; set; }
    }

    internal partial class Module
    {
        [JsonProperty("id")]
        internal string Id { get; set; }

        [JsonProperty("type")]
        internal string Type { get; set; }

        [JsonProperty("firmware_revision")]
        internal long FirmwareRevision { get; set; }

        [JsonProperty("rf_strength")]
        internal long RfStrength { get; set; }

        [JsonProperty("wifi_strength", NullValueHandling = NullValueHandling.Ignore)]
        internal long? WifiStrength { get; set; }

        [JsonProperty("reachable", NullValueHandling = NullValueHandling.Ignore)]
        internal bool? Reachable { get; set; }

        [JsonProperty("battery_level", NullValueHandling = NullValueHandling.Ignore)]
        internal long? BatteryLevel { get; set; }

        [JsonProperty("bridge", NullValueHandling = NullValueHandling.Ignore)]
        internal string Bridge { get; set; }

        [JsonProperty("battery_state", NullValueHandling = NullValueHandling.Ignore)]
        internal string BatteryState { get; set; }
    }

    internal partial class Room
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        internal long Id { get; set; }

        [JsonProperty("reachable")]
        internal bool Reachable { get; set; }

        [JsonProperty("therm_measured_temperature")]
        internal double ThermMeasuredTemperature { get; set; }

        [JsonProperty("heating_power_request")]
        internal long HeatingPowerRequest { get; set; }

        [JsonProperty("therm_setpoint_temperature")]
        internal long ThermSetpointTemperature { get; set; }

        [JsonProperty("therm_setpoint_mode")]
        internal string ThermSetpointMode { get; set; }

        [JsonProperty("therm_setpoint_start_time")]
        internal long ThermSetpointStartTime { get; set; }

        [JsonProperty("therm_setpoint_end_time")]
        internal long ThermSetpointEndTime { get; set; }

        [JsonProperty("anticipating")]
        internal bool Anticipating { get; set; }

        [JsonProperty("open_window")]
        internal bool OpenWindow { get; set; }
    }

    internal partial class HomeStatus
    {
        internal static HomeStatus FromJson(string json) => JsonConvert.DeserializeObject<HomeStatus>(json, HomeStatusConverter.Settings);
    }

    internal static class HomeStatusSerialize
    {
        internal static string ToJson(this HomeStatus self) => JsonConvert.SerializeObject(self, HomeStatusConverter.Settings);
    }

    internal static class HomeStatusConverter
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