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
        public void PositionnerTroupe(List<UniteBase> UniteBases, ITerritoireBase territoire);
    }
}
