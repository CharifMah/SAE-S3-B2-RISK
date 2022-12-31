using Models.Map;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Player
{
    public interface IGestionTroupe
    {
        public void AddUnit(IUnit unit);
        public void AddUnits(List<IUnit> unites, ITerritoireBase territoire);
        public void RemoveUnit(IUnit unit);
        public void RemoveUnit(List<IUnit> unites, ITerritoireBase territoire);
    }
}
