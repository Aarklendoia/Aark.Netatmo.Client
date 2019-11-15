using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Globalization;

namespace Aark.Netatmo.SDK.Models.Security
{
    internal class PersonsAtHome
    {
        [JsonProperty("home_id")]
        internal string HomeId { get; set; }

        [JsonProperty("person_ids")]
        internal List<string> PersonIds { get; set; }
    }

    internal static class Serialize
    {
        public static string ToJson(this PersonsAtHome self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
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
