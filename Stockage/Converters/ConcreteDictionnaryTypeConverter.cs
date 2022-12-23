using Newtonsoft.Json;

namespace ModelsAPI.Converters
{
    public class ConcreteDictionnaryTypeConverter<TDictionary, TItem, TKey,TValue> : JsonConverter where TDictionary : IDictionary<TKey, TValue>, new() where TItem : TValue
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var collection = new TDictionary();
            var items = serializer.Deserialize<Dictionary<TKey, TItem>>(reader);

            if (items != null)
            {
                foreach (var item in items)
                {
                    collection.Add(item.Key, item.Value);
                }
            }
       
            return collection;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IDictionary<TKey, TValue>).IsAssignableFrom(objectType);
        }
    }
}


