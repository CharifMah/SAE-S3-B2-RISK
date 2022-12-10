using DBStorage.ClassMetier.Units;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DBStorage.ClassMetier.Map
{
    public interface ITerritoireBase
    {
        int ID { get; set; }
        Teams Team { get; set; }
        //List<IUnit> Units { get; set; }

        void AddUnit(IUnit UniteBase);
        void RemoveUnit(IUnit UniteBase);
    }
}