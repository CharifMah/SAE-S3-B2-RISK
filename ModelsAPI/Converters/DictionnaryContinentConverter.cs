using Microsoft.VisualBasic;
using ModelsAPI.ClassMetier.Map;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ModelsAPI.Converters
{
    internal class DictionnaryContinentConverter : JsonConverter<Dictionary<int, IContinent>>
    {
        private static string json;
        public override Dictionary<int, IContinent>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var root = new Dictionary<int, IContinent>();

            reader.Read();
            while (reader.Read())
            {
                var rootPropertyName = reader.ValueSpan;
                reader.Read();
                ParseRootObjectProperty(ref reader, rootPropertyName, root);
            }
            return root;
           
        }

        private static void ParseRootObjectProperty(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, Dictionary<int, IContinent> dico)
        {
            
            json += reader.GetByte();
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<int, IContinent> value, JsonSerializerOptions options)
        {
            writer.WriteRawValue(JsonSerializer.Serialize(value));
        }

       
    }
}
