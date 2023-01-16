using Models.Player;
using Models.Units;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.Map
{
    public interface ITerritoireBase
    {
        int ID { get; set; }
        Teams Team { get; set; }
        List<IUnit> Units { get; set; }

        void AddUnit(IUnit UniteBase);
        void RemoveUnit(IUnit UniteBase);
    }
}