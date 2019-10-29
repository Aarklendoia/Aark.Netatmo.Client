using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aark.Netatmo.SDK.Models.Energy
{
    internal partial class RoomMeasures
    {
        [JsonProperty("body")]
        internal List<Body> Body { get; set; }

        [JsonProperty("status")]
        internal string Status { get; set; }

        [JsonProperty("time_exec")]
        internal double TimeExec { get; set; }

        [JsonProperty("time_server")]
        internal long TimeServer { get; set; }
    }

    internal partial class Body
    {
        [JsonProperty("beg_time")]
        internal long BegTime { get; set; }

        [JsonProperty("step_time")]
        internal long StepTime { get; set; }

        [JsonProperty("value")]
        internal List<List<double>> Value { get; set; }
    }

    internal partial class RoomMeasures
    {
        internal static RoomMeasures FromJson(string json) => JsonConvert.DeserializeObject<RoomMeasures>(json, RoomMeasuresConverter.Settings);
    }

    internal static class RoomMeasuresSerialize
    {
        internal static string ToJson(this RoomMeasures self) => JsonConvert.SerializeObject(self, RoomMeasuresConverter.Settings);
    }

    internal static class RoomMeasuresConverter
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