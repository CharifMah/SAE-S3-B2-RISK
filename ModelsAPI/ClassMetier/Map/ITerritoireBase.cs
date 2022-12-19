using ModelsAPI.ClassMetier.Units;

namespace ModelsAPI.ClassMetier.Map
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