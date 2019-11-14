using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace Aark.Netatmo.SDK.Models.Security
{
    internal class LocalUrl
    {
        [JsonProperty("local_url")]
        internal string Url { get; set; }

        [JsonProperty("product_name")]
        internal long ProductName { get; set; }

        private readonly JsonSerializerSettings Settings;

        public LocalUrl()
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

        internal LocalUrl FromJson(string json) => JsonConvert.DeserializeObject<LocalUrl>(json, Settings);
    }
}
