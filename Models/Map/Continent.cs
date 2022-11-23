using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models.Map
{
    /// <summary>
    /// Classe représentant les différents continents du plateau
    /// </summary>
    public class Continent
    {
        private Dictionary<int, ITerritoireBase> dicoTerritoires;

        public Dictionary<int, ITerritoireBase> DicoTerritoires
        {
            get { return dicoTerritoires; }
            set { dicoTerritoires = value; }
        }

        public Continent(List<ITerritoireBase> territoires)
        {
            dicoTerritoires = new Dictionary<int, ITerritoireBase>();
            for (int i = 0; i < territoires.Count; i++)
            {
                dicoTerritoires.Add(i, territoires[i]);
            }
        }


    }
}
