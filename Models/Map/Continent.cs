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

        Dictionary<int, TerritoireBase> dicoTerritoires;

        public List<TerritoireBase> Territoires
        {
            get { return territoires; }
            set { territoires = value; }
        }

        public Continent(List<TerritoireBase> territoires)
        {
            dicoTerritoires = new Dictionary<int, TerritoireBase>();
            this.territoires = territoires;
            for (int i = 0; i < territoires.Count; i++)
            {
                dicoTerritoires.Add(i, territoires[i]);
            }
        }


    }
}
