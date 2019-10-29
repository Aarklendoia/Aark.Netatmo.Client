using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aark.Netatmo.SDK.Models.Common
{
    internal class AccessData
    {
        [JsonProperty("access_token")]
        internal string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        internal long ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        internal string RefreshToken { get; set; }

        readonly JsonSerializerSettings Settings;

        public AccessData()
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

        internal AccessData FromJson(string json)
        {
            return JsonConvert.DeserializeObject<AccessData>(json, Settings);
        }
    }
}
