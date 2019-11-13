using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aark.Netatmo.SDK.Models.Energy
{
    internal class RoomMeasures
    {
        internal struct BodyRoomMeasures
        {
            [JsonProperty("beg_time")]
            internal long BegTime { get; set; }

            [JsonProperty("step_time")]
            internal long StepTime { get; set; }

            [JsonProperty("value")]
            internal List<List<double>> Value { get; set; }
        }

        [JsonProperty("body")]
        internal List<BodyRoomMeasures> Body { get; set; }

        [JsonProperty("status")]
        internal string Status { get; set; }

        [JsonProperty("time_exec")]
        internal double TimeExec { get; set; }

        [JsonProperty("time_server")]
        internal long TimeServer { get; set; }

        private readonly JsonSerializerSettings Settings;

        public RoomMeasures()
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

        internal RoomMeasures FromJson(string json) => JsonConvert.DeserializeObject<RoomMeasures>(json, Settings);
    }
}