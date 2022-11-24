using Models.Map;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Joueur
{
    public interface IGestionTroupe
    {
        public void PositionnerTroupe(List<Unite> unites, ITerritoireBase territoire);
    }
}
