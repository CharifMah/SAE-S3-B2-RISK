using Models.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Units
{
    public interface IGestionTroupe
    {
        public void PositionnerTroupe(List<Unite> unites, TerritoireBase territoire);
    }
}
