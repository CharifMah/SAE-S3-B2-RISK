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
        public void AddUnit(Unite unit);

        public void AddUnit(List<Unite> unites, TerritoireBase territoire);

        public void RemoveUnit(Unite unit);

        public void RemoveUnit(List<Unite> unites, TerritoireBase territoire);
    }
}
