using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aark.Netatmo.SDK.Models.Weather
{
    internal partial class ErrorMeasuresData
    {
        internal struct MeasuresDataError
        {
            [JsonProperty("code")]
            internal long Code { get; set; }

            [JsonProperty("message")]
            internal string Message { get; set; }
        }


        [JsonProperty("error")]
        internal MeasuresDataError Error { get; set; }
    
        private readonly JsonSerializerSettings Settings;

        public ErrorMeasuresData()
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

        public ErrorMeasuresData FromJson(string json) => JsonConvert.DeserializeObject<ErrorMeasuresData>(json, Settings);
    }
}
