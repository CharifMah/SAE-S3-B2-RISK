using ModelsAPI.ClassMetier.Map;
using System.Formats.Asn1;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace ModelsAPI.Converters
{
    internal class DictionnaryTerritoireConverter : JsonConverter<Dictionary<string, ITerritoireBase>>
    {
        public override Dictionary<string, ITerritoireBase>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<string, ITerritoireBase> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
