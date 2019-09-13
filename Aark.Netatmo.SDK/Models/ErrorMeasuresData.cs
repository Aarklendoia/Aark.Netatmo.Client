using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aark.Netatmo.SDK.Models
{
    internal partial class ErrorMeasuresData
    {
        [JsonProperty("error")]
        internal Error Error { get; set; }
    }

    internal partial class Error
    {
        [JsonProperty("code")]
        internal long Code { get; set; }

        [JsonProperty("message")]
        internal string Message { get; set; }
    }

    internal partial class ErrorMeasuresData
    {
        public static ErrorMeasuresData FromJson(string json) => JsonConvert.DeserializeObject<ErrorMeasuresData>(json, ErrorMeasuresDataConverter.Settings);
    }

    internal static class ErrorMeasuresDataSerialize
    {
        public static string ToJson(this ErrorMeasuresData self) => JsonConvert.SerializeObject(self, ErrorMeasuresDataConverter.Settings);
    }

    internal static class ErrorMeasuresDataConverter
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
