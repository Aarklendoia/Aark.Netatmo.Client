using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aark.Netatmo.SDK.Models.Security
{
    internal class HomeData
    {
        internal struct HomeDataBody
        {
            [JsonProperty("homes")]
            internal Home[] Homes { get; set; }

            [JsonProperty("user")]
            internal User User { get; set; }

            [JsonProperty("global_info")]
            internal GlobalInfo GlobalInfo { get; set; }
        }

        internal struct GlobalInfo
        {
            [JsonProperty("show_tags")]
            internal bool ShowTags { get; set; }
        }

        internal struct SmokeDetector
        {
            [JsonProperty("id")]
            internal string Id { get; set; }

            [JsonProperty("type")]
            internal string Type { get; set; }

            [JsonProperty("last_setup")]
            internal long LastSetup { get; set; }

            [JsonProperty("name")]
            internal string Name { get; set; }
        }

        internal struct Module
        {
            [JsonProperty("id")]
            internal string Id { get; set; }


            [JsonProperty("type")]
            internal string Type { get; set; }

            [JsonProperty("name")]
            internal string Name { get; set; }

            [JsonProperty("battery_percent")]
            internal long BatteryPercent { get; set; }

            [JsonProperty("status")]
            internal string Status { get; set; }

            [JsonProperty("rf")]
            internal long RadioFrequecy { get; set; }

            [JsonProperty("last_activity")]
            internal long LastActivity { get; set; }
        }

        internal struct Home
        {
            [JsonProperty("id")]
            internal string Id { get; set; }

            [JsonProperty("name")]
            internal string Name { get; set; }

            [JsonProperty("persons", NullValueHandling = NullValueHandling.Ignore)]
            internal List<Person> Persons { get; set; }

            [JsonProperty("place")]
            internal Place Place { get; set; }

            [JsonProperty("cameras", NullValueHandling = NullValueHandling.Ignore)]
            internal List<Camera> Cameras { get; set; }

            [JsonProperty("smokedetectors", NullValueHandling = NullValueHandling.Ignore)]
            internal List<SmokeDetector> SmokeDetectors { get; set; }

            [JsonProperty("events", NullValueHandling = NullValueHandling.Ignore)]
            internal List<Event> Events { get; set; }

            [JsonProperty("modules", NullValueHandling = NullValueHandling.Ignore)]
            internal List<Module> Modules { get; set; }
        }

        internal struct Camera
        {
            [JsonProperty("id")]
            internal string Id { get; set; }

            [JsonProperty("type")]
            internal string Type { get; set; }

            [JsonProperty("status")]
            internal string Status { get; set; }

            [JsonProperty("vpn_url")]
            internal Uri VpnUrl { get; set; }

            [JsonProperty("is_local")]
            internal bool IsLocal { get; set; }

            [JsonProperty("sd_status")]
            internal string SdStatus { get; set; }

            [JsonProperty("alim_status")]
            internal string AlimStatus { get; set; }

            [JsonProperty("name")]
            internal string Name { get; set; }

            [JsonProperty("use_pin_code")]
            internal bool UsePinCode { get; set; }

            [JsonProperty("last_setup")]
            internal long LastSetup { get; set; }
        }

        internal struct Event
        {
            [JsonProperty("id")]
            internal string Id { get; set; }

            [JsonProperty("type")]
            internal string Type { get; set; }

            [JsonProperty("time")]
            internal long Time { get; set; }

            [JsonProperty("camera_id")]
            internal string CameraId { get; set; }

            [JsonProperty("device_id")]
            internal string DeviceId { get; set; }

            [JsonProperty("person_id", NullValueHandling = NullValueHandling.Ignore)]
            internal string PersonId { get; set; }

            [JsonProperty("video_status")]
            internal string VideoStatus { get; set; }

            [JsonProperty("is_arrival", NullValueHandling = NullValueHandling.Ignore)]
            internal bool? IsArrival { get; set; }

            [JsonProperty("message")]
            internal string Message { get; set; }

            [JsonProperty("sub_type")]
            internal string SubType { get; set; }

            [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
            internal string Category { get; set; }

            [JsonProperty("snapshot", NullValueHandling = NullValueHandling.Ignore)]
            internal Snapshot Snapshot { get; set; }

            [JsonProperty("vignette", NullValueHandling = NullValueHandling.Ignore)]
            internal Snapshot Vignette { get; set; }

            [JsonProperty("video_id", NullValueHandling = NullValueHandling.Ignore)]
            internal string VideoId { get; set; }

            [JsonProperty("event_list", NullValueHandling = NullValueHandling.Ignore)]
            internal List<string> EventList { get; set; }
        }

        internal struct Snapshot
        {
            [JsonProperty("id")]
            internal string Id { get; set; }

            [JsonProperty("version")]
            internal long Version { get; set; }

            [JsonProperty("key")]
            internal string Key { get; set; }

            [JsonProperty("url")]
            internal Uri Url { get; set; }
        }

        internal struct Person
        {
            [JsonProperty("id")]
            internal string Id { get; set; }

            [JsonProperty("last_seen")]
            internal long LastSeen { get; set; }

            [JsonProperty("out_of_sight")]
            internal bool OutOfSight { get; set; }

            [JsonProperty("face")]
            internal Snapshot Face { get; set; }

            [JsonProperty("pseudo", NullValueHandling = NullValueHandling.Ignore)]
            internal string Pseudo { get; set; }
        }

        internal struct Place
        {
            [JsonProperty("city")]
            internal string City { get; set; }

            [JsonProperty("country")]
            internal string Country { get; set; }

            [JsonProperty("timezone")]
            internal string Timezone { get; set; }
        }

        internal struct User
        {
            [JsonProperty("reg_locale")]
            internal string RegLocale { get; set; }

            [JsonProperty("lang")]
            internal string Lang { get; set; }

            [JsonProperty("country")]
            internal string Country { get; set; }

            [JsonProperty("mail")]
            internal string Mail { get; set; }
        }

        [JsonProperty("body")]
        internal HomeDataBody Body { get; set; }

        [JsonProperty("status")]
        internal string Status { get; set; }

        [JsonProperty("time_exec")]
        internal double TimeExec { get; set; }

        [JsonProperty("time_server")]
        internal long TimeServer { get; set; }

        private readonly JsonSerializerSettings Settings;

        public HomeData()
        {
            Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
                {
                    new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
                },
            };
        }

        internal HomeData FromJson(string json) => JsonConvert.DeserializeObject<HomeData>(json, Settings);
    }
}