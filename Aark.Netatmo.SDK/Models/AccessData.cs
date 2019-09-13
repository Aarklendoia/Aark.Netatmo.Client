using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aark.Netatmo.SDK.Models
{
    internal partial class AccessData
    {
        [JsonProperty("access_token")]
        internal string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        internal long ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        internal string RefreshToken { get; set; }
    }

    internal partial class AccessData
    {
        internal static AccessData FromJson(string json) => JsonConvert.DeserializeObject<AccessData>(json, AccessDataConverter.Settings);
    }

    internal static class AccessDataSerialize
    {
        internal static string ToJson(this AccessData self) => JsonConvert.SerializeObject(self, AccessDataConverter.Settings);
    }

    internal static class AccessDataConverter
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
