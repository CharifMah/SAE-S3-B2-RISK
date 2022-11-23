using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models.Map
{
    /// <summary>
    /// Classe du plateau de jeu
    /// </summary>
    public class Carte
    {
        private Dictionary<int, Continent> dicoContinents;
        public Dictionary<int, Continent> DicoContinents
        {
            get { return dicoContinents; }
        }

        public Carte(List<Continent> continent)
        {
            dicoContinents = new Dictionary<int, Continent>();
            for (int i = 0; i < continent.Count; i++)
            {
                dicoContinents.Add(i, continent[i]);
            }

        }    
    }
}