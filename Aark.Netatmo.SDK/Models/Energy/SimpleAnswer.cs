using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aark.Netatmo.SDK.Models.Energy
{
    internal class SimpleAnswer
    {
        [JsonProperty("status")]
        internal string Status { get; set; }

        [JsonProperty("time_exec")]
        internal double TimeExec { get; set; }

        [JsonProperty("time_server")]
        internal long TimeServer { get; set; }

        private readonly JsonSerializerSettings Settings;

        public SimpleAnswer()
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

        internal SimpleAnswer FromJson(string json) => JsonConvert.DeserializeObject<SimpleAnswer>(json, Settings);
    }
}
