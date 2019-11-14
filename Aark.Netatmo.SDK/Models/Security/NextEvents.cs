using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using static Aark.Netatmo.SDK.Models.Security.HomeData;

namespace Aark.Netatmo.SDK.Models.Security
{
    internal class NextEvents
    {
        internal struct NextEventBody
        {
            [JsonProperty("events_list")]
            internal List<Event> Events { get; set; }
        }

        [JsonProperty("body")]
        internal NextEventBody Body { get; set; }

        private readonly JsonSerializerSettings Settings;

        public NextEvents()
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

        internal NextEvents FromJson(string json) => JsonConvert.DeserializeObject<NextEvents>(json, Settings);
    }
}
