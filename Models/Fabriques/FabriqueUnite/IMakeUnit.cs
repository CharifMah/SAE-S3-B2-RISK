using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Units;

namespace Models.Fabriques.FabriqueUnite
{
    public interface IMakeUnit
    {
        public UniteBase MakeUnit();
    }
}
