using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aark.Netatmo.SDK.Models
{
    internal partial class ErrorData
    {
        [JsonProperty("error")]
        internal string Error { get; set; }
    }

    internal partial class ErrorData
    {
        internal static ErrorData FromJson(string json) => JsonConvert.DeserializeObject<ErrorData>(json, ErrorDataConverter.Settings);
    }

    internal static class ErrorDataSerialize
    {
        internal static string ToJson(this ErrorData self) => JsonConvert.SerializeObject(self, ErrorDataConverter.Settings);
    }

    internal static class ErrorDataConverter
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