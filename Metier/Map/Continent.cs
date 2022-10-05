using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier.Map
{
    /// <summary>
    /// Classe représentant les différents continents du plateau
    /// </summary>
    public class Continent
    {
        private List<TerritoireBase> territoires;

        public List<TerritoireBase> Territoires
        {
            get { return territoires; }
        }

        public Continent(List<TerritoireBase> t)
        {
            this.territoires = t;
        }    
    }
}
