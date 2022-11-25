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
        public void AddUnit(UniteBase unit);
        public void AddUnits(List<UniteBase> unites, ITerritoireBase territoire);
        public void RemoveUnit(UniteBase unit);
        public void RemoveUnit(List<UniteBase> unites, ITerritoireBase territoire);
    }
}
