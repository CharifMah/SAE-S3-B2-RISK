using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Units;

namespace ModelsAPI.ClassMetier.Player
{
    public interface IGestionTroupe
    {
         void AddUnit(IUnit unit);
         void RemoveUnit(IUnit unit);
         void RemoveUnit(List<IUnit> unites, ITerritoireBase territoire);
    }
}
