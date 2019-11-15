using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aark.Netatmo.SDK.Models.Common
{
    internal partial class ErrorMessageData
    {
        internal struct DataError
        {
            [JsonProperty("code")]
            internal long Code { get; set; }

            [JsonProperty("message")]
            internal string Message { get; set; }
        }


        [JsonProperty("error")]
        internal DataError Error { get; set; }
    
        private readonly JsonSerializerSettings Settings;

        public ErrorMessageData()
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

        public ErrorMessageData FromJson(string json) => JsonConvert.DeserializeObject<ErrorMessageData>(json, Settings);
    }
}
