using Newtonsoft.Json.Converters;

namespace ModelsAPI.Converters
{
    public class TypeConverter<T, TSerialized> : CustomCreationConverter<T>
    where TSerialized : T, new()
    {
        public override T Create(Type objectType)
        {
            return new TSerialized();
        }
    }
}
