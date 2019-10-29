using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Aark.Netatmo.SDK.Models
{
    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);


        readonly ResourceManager stringManager;

        public ParseStringConverter()
        {
            stringManager = new ResourceManager("en-US", Assembly.GetExecutingAssembly());
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (Int64.TryParse(value, out long l))
            {
                return l;
            }
            throw new Exception(stringManager.GetString("cannotUnmarshalTypeLong", CultureInfo.CurrentUICulture));
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString(CultureInfo.InvariantCulture));
            return;
        }

        internal static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
