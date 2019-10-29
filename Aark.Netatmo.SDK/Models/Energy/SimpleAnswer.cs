using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aark.Netatmo.SDK.Models.Energy
{
    internal partial class SimpleAnswer
    {
        [JsonProperty("status")]
        internal string Status { get; set; }

        [JsonProperty("time_exec")]
        internal double TimeExec { get; set; }

        [JsonProperty("time_server")]
        internal long TimeServer { get; set; }
    }

    internal partial class SimpleAnswer
    {
        internal static SimpleAnswer FromJson(string json) => JsonConvert.DeserializeObject<SimpleAnswer>(json, SimpleAnswerConverter.Settings);
    }

    internal static class SimpleAnswerSerialize
    {
        internal static string ToJson(this SimpleAnswer self) => JsonConvert.SerializeObject(self, SimpleAnswerConverter.Settings);
    }

    internal static class SimpleAnswerConverter
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
