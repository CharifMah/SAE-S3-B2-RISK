using Models.Units;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.Map
{
    [JsonConverter(typeof(ITerritoireBase))]
    public interface ITerritoireBase
    {
        int ID { get; set; }
        Teams Team { get; set; }
        List<Unite> Units { get; set; }

        void AddUnit(Unite unite);
    }
}