using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aark.Netatmo.SDK.Models
{
    internal partial class MeasuresData
    {
        [JsonProperty("body")]
        internal List<StationDataBody> Body { get; set; }

        [JsonProperty("status")]
        internal string Status { get; set; }

        [JsonProperty("time_exec")]
        internal double TimeExec { get; set; }

        [JsonProperty("time_server")]
        internal long TimeServer { get; set; }
    }

    internal partial class StationDataBody
    {
        [JsonProperty("beg_time")]
        internal long BegTime { get; set; }

        [JsonProperty("step_time")]
        internal long StepTime { get; set; }

        [JsonProperty("value")]
        internal List<List<double?>> Value { get; set; }
    }

    internal partial class MeasuresData
    {
        internal static MeasuresData FromJson(string json) => JsonConvert.DeserializeObject<MeasuresData>(json, MeasuresDataConverter.Settings);
    }

    internal static class MeasuresDataSerialize
    {
        internal static string ToJson(this MeasuresData self) => JsonConvert.SerializeObject(self, MeasuresDataConverter.Settings);
    }

    internal static class MeasuresDataConverter
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

