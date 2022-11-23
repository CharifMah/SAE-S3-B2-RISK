using Models.Map;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public interface ICombat
    {
        public void Attaquer(List<Unite> attaquant, TerritoireBase cible);

        public void Defendre(List<Unite> defenseurs, TerritoireBase territoireAttaquant);
    }
}
