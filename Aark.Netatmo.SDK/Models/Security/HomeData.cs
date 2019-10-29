using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aark.Netatmo.SDK.Models.Security
{
    internal partial class HomeData
    {
        [JsonProperty("body")]
        internal Body Body { get; set; }

        [JsonProperty("status")]
        internal string Status { get; set; }

        [JsonProperty("time_exec")]
        internal double TimeExec { get; set; }

        [JsonProperty("time_server")]
        internal long TimeServer { get; set; }
    }

    internal partial class Body
    {
        [JsonProperty("homes")]
        internal Home[] Homes { get; set; }

        [JsonProperty("user")]
        internal User User { get; set; }

        [JsonProperty("global_info")]
        internal GlobalInfo GlobalInfo { get; set; }
    }

    internal partial class GlobalInfo
    {
        [JsonProperty("show_tags")]
        internal bool ShowTags { get; set; }
    }

    internal partial class Home
    {
        [JsonProperty("id")]
        internal string Id { get; set; }

        [JsonProperty("name")]
        internal string Name { get; set; }

        [JsonProperty("persons")]
        internal Person[] Persons { get; set; }

        [JsonProperty("place")]
        internal Place Place { get; set; }

        [JsonProperty("cameras")]
        internal Camera[] Cameras { get; set; }

        [JsonProperty("smokedetectors")]
        internal object[] Smokedetectors { get; set; }

        [JsonProperty("events", NullValueHandling = NullValueHandling.Ignore)]
        internal Event[] Events { get; set; }
    }

    internal partial class Camera
    {
        [JsonProperty("id")]
        internal Id Id { get; set; }

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

    internal partial class Event
    {
        [JsonProperty("id")]
        internal string Id { get; set; }

        [JsonProperty("type")]
        internal TypeEnum Type { get; set; }

        [JsonProperty("time")]
        internal long Time { get; set; }

        [JsonProperty("camera_id")]
        internal Id CameraId { get; set; }

        [JsonProperty("device_id")]
        internal Id DeviceId { get; set; }

        [JsonProperty("person_id", NullValueHandling = NullValueHandling.Ignore)]
        internal Guid? PersonId { get; set; }

        [JsonProperty("video_status")]
        internal VideoStatus VideoStatus { get; set; }

        [JsonProperty("is_arrival", NullValueHandling = NullValueHandling.Ignore)]
        internal bool? IsArrival { get; set; }

        [JsonProperty("message")]
        internal Message Message { get; set; }

        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        internal string Category { get; set; }

        [JsonProperty("snapshot", NullValueHandling = NullValueHandling.Ignore)]
        internal Snapshot Snapshot { get; set; }

        [JsonProperty("vignette", NullValueHandling = NullValueHandling.Ignore)]
        internal Snapshot Vignette { get; set; }

        [JsonProperty("video_id", NullValueHandling = NullValueHandling.Ignore)]
        internal Guid? VideoId { get; set; }
    }

    internal partial class Snapshot
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

    internal partial class Person
    {
        [JsonProperty("id")]
        internal Guid Id { get; set; }

        [JsonProperty("last_seen")]
        internal long LastSeen { get; set; }

        [JsonProperty("out_of_sight")]
        internal bool OutOfSight { get; set; }

        [JsonProperty("face")]
        internal Snapshot Face { get; set; }

        [JsonProperty("pseudo", NullValueHandling = NullValueHandling.Ignore)]
        internal string Pseudo { get; set; }
    }

    internal partial class Place
    {
        [JsonProperty("city")]
        internal string City { get; set; }

        [JsonProperty("country")]
        internal string Country { get; set; }

        [JsonProperty("timezone")]
        internal string Timezone { get; set; }
    }

    internal partial class User
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

    internal enum Id { The70Ee5021F9Ee };

    internal enum Message { BDelphineBAÉtéVuE, BEdouardBAÉtéVuE, BMouvementBDétecté };

    internal enum TypeEnum { Movement, Person };

    internal enum VideoStatus { Available, Deleted };

    internal partial class HomeData
    {
        internal static HomeData FromJson(string json) => JsonConvert.DeserializeObject<HomeData>(json, HomeDataConverter.Settings);
    }

    internal static class HomeDataSerialize
    {
        internal static string ToJson(this HomeData self) => JsonConvert.SerializeObject(self, HomeDataConverter.Settings);
    }

    internal static class HomeDataConverter
    {
        internal static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                TypeEnumConverter.Singleton,
                VideoStatusConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        readonly ResourceManager stringManager;

        public TypeEnumConverter()
        {
            stringManager = new ResourceManager("en-US", Assembly.GetExecutingAssembly());
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "movement":
                    return TypeEnum.Movement;
                case "person":
                    return TypeEnum.Person;
            }
            throw new Exception(stringManager.GetString("cannotUnmarshalTypeTypeEnum", CultureInfo.CurrentUICulture));
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            switch (value)
            {
                case TypeEnum.Movement:
                    serializer.Serialize(writer, "movement");
                    return;
                case TypeEnum.Person:
                    serializer.Serialize(writer, "person");
                    return;
            }
            throw new Exception(stringManager.GetString("cannotMarshalTypeTypeEnum", CultureInfo.CurrentUICulture));
        }

        internal static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }

    internal class VideoStatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(VideoStatus) || t == typeof(VideoStatus?);

        readonly ResourceManager stringManager;

        public VideoStatusConverter()
        {
            stringManager = new ResourceManager("en-US", Assembly.GetExecutingAssembly());
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "available":
                    return VideoStatus.Available;
                case "deleted":
                    return VideoStatus.Deleted;
            }
            throw new Exception(stringManager.GetString("cannotUnmarshalTypeVideoStatus", CultureInfo.CurrentUICulture));
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (VideoStatus)untypedValue;
            switch (value)
            {
                case VideoStatus.Available:
                    serializer.Serialize(writer, "available");
                    return;
                case VideoStatus.Deleted:
                    serializer.Serialize(writer, "deleted");
                    return;
            }
            throw new Exception(stringManager.GetString("cannotMarshalTypeVideoStatus", CultureInfo.CurrentUICulture));
        }

        internal static readonly VideoStatusConverter Singleton = new VideoStatusConverter();
    }
}