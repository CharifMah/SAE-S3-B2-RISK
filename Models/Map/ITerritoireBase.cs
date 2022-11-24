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
        List<UniteBase> Units { get; set; }

        void AddUnit(UniteBase UniteBase);
    }
}