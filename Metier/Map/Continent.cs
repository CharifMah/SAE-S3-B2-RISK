using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Map
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
            set { territoires = value; }
        }

        public Continent()
        {
            this.territoires = new List<TerritoireBase>();
        }
    }
}
