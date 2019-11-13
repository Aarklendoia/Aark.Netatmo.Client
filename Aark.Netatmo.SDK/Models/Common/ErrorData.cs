using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aark.Netatmo.SDK.Models.Common
{
    internal class ErrorData
    {
        [JsonProperty("error")]
        internal string Error { get; set; }

        private readonly JsonSerializerSettings Settings;

        public ErrorData()
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

        internal ErrorData FromJson(string json) => JsonConvert.DeserializeObject<ErrorData>(json, Settings);
    }
}