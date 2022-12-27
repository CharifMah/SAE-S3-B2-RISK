using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Units;

namespace ModelsAPI.ClassMetier.Player
{
    public interface IGestionTroupe
    {
        public void AddUnit(IUnit unit);
        public void AddUnits(List<IUnit> unites, ITerritoireBase territoire);
        public void RemoveUnit(IUnit unit);
        public void RemoveUnit(List<IUnit> unites, ITerritoireBase territoire);
    }
}
