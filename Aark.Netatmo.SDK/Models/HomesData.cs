﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aark.Netatmo.SDK.Models
{
    internal partial class HomesData
    {
        [JsonProperty("body")]
        internal HomesDataBody Body { get; set; }

        [JsonProperty("status")]
        internal string Status { get; set; }

        [JsonProperty("time_exec")]
        internal double TimeExec { get; set; }

        [JsonProperty("time_server")]
        internal long TimeServer { get; set; }
    }

    internal partial class HomesDataBody
    {
        [JsonProperty("homes")]
        internal List<HomeData> Homes { get; set; }

        [JsonProperty("user")]
        internal User User { get; set; }
    }

    internal partial class HomeData
    {
        [JsonProperty("id")]
        internal string Id { get; set; }

        [JsonProperty("name")]
        internal string Name { get; set; }

        [JsonProperty("altitude")]
        internal long Altitude { get; set; }

        [JsonProperty("coordinates")]
        internal List<double> Coordinates { get; set; }

        [JsonProperty("country")]
        internal string Country { get; set; }

        [JsonProperty("timezone")]
        internal string Timezone { get; set; }

        [JsonProperty("therm_setpoint_default_duration")]
        internal long ThermSetpointDefaultDuration { get; set; }

        [JsonProperty("therm_mode")]
        internal string ThermMode { get; set; }

        [JsonProperty("rooms", NullValueHandling = NullValueHandling.Ignore)]
        internal List<HomeRoom> Rooms { get; set; }

        [JsonProperty("modules", NullValueHandling = NullValueHandling.Ignore)]
        internal List<HomesModule> Modules { get; set; }

        [JsonProperty("therm_schedules", NullValueHandling = NullValueHandling.Ignore)]
        internal List<Schedule> ThermSchedules { get; set; }

        [JsonProperty("schedules", NullValueHandling = NullValueHandling.Ignore)]
        internal List<Schedule> Schedules { get; set; }
    }

    internal partial class HomesModule
    {
        [JsonProperty("id")]
        internal string Id { get; set; }

        [JsonProperty("type")]
        internal string Type { get; set; }

        [JsonProperty("name")]
        internal string Name { get; set; }

        [JsonProperty("setup_date")]
        internal long SetupDate { get; set; }

        [JsonProperty("modules_bridged", NullValueHandling = NullValueHandling.Ignore)]
        internal List<string> ModulesBridged { get; set; }

        [JsonProperty("room_id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        internal long? RoomId { get; set; }

        [JsonProperty("bridge", NullValueHandling = NullValueHandling.Ignore)]
        internal string Bridge { get; set; }
    }

    internal partial class HomeRoom
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        internal long Id { get; set; }

        [JsonProperty("name")]
        internal string Name { get; set; }

        [JsonProperty("type")]
        internal string Type { get; set; }

        [JsonProperty("module_ids")]
        internal List<string> ModuleIds { get; set; }
    }

    internal partial class Schedule
    {
        [JsonProperty("timetable")]
        internal List<Timetable> Timetable { get; set; }

        [JsonProperty("zones")]
        internal List<Zone> Zones { get; set; }

        [JsonProperty("name")]
        internal string Name { get; set; }

        [JsonProperty("default")]
        internal bool Default { get; set; }

        [JsonProperty("away_temp")]
        internal long AwayTemp { get; set; }

        [JsonProperty("hg_temp")]
        internal long HgTemp { get; set; }

        [JsonProperty("id")]
        internal string Id { get; set; }

        [JsonProperty("selected", NullValueHandling = NullValueHandling.Ignore)]
        internal bool? Selected { get; set; }

        [JsonProperty("type")]
        internal string Type { get; set; }
    }

    internal partial class Timetable
    {
        [JsonProperty("zone_id")]
        internal long ZoneId { get; set; }

        [JsonProperty("m_offset")]
        internal long MOffset { get; set; }
    }

    internal partial class Zone
    {
        [JsonProperty("name")]
        internal string Name { get; set; }

        [JsonProperty("id")]
        internal long Id { get; set; }

        [JsonProperty("type")]
        internal long Type { get; set; }

        [JsonProperty("rooms_temp")]
        internal List<RoomsTemp> RoomsTemp { get; set; }

        [JsonProperty("rooms", NullValueHandling = NullValueHandling.Ignore)]
        internal List<ZoneRoom> Rooms { get; set; }
    }

    internal partial class ZoneRoom
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        internal long Id { get; set; }

        [JsonProperty("therm_setpoint_temperature")]
        internal long ThermSetpointTemperature { get; set; }
    }

    internal partial class RoomsTemp
    {
        [JsonProperty("room_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        internal long RoomId { get; set; }

        [JsonProperty("temp")]
        internal long Temp { get; set; }
    }

    internal partial class User
    {
        [JsonProperty("email")]
        internal string Email { get; set; }

        [JsonProperty("language")]
        internal string Language { get; set; }

        [JsonProperty("locale")]
        internal string Locale { get; set; }

        [JsonProperty("feel_like_algorithm")]
        internal long FeelLikeAlgorithm { get; set; }

        [JsonProperty("unit_pressure")]
        internal long UnitPressure { get; set; }

        [JsonProperty("unit_system")]
        internal long UnitSystem { get; set; }

        [JsonProperty("unit_wind")]
        internal long UnitWind { get; set; }

        [JsonProperty("id")]
        internal string Id { get; set; }
    }

    internal partial class HomesData
    {
        internal static HomesData FromJson(string json) => JsonConvert.DeserializeObject<HomesData>(json, HomesDataConverter.Settings);
    }

    internal static class HomesDataSerialize
    {
        internal static string ToJson(this HomesData self) => JsonConvert.SerializeObject(self, HomesDataConverter.Settings);
    }

    internal static class HomesDataConverter
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