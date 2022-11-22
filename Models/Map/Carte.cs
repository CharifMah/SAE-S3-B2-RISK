using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Map
{
    /// <summary>
    /// Classe du plateau de jeu
    /// </summary>
    public class Carte
    {
        private List<Continent> continent;
        Dictionary<int, Continent> dicoContinents;

        public List<Continent> Continent
        {
            get { return continent; }
        }

        public Carte(List<Continent> continent)
        {
            dicoContinents = new Dictionary<int, Continent>();
            this.continent = continent;
            for (int i = 0; i < continent.Count; i++)
            {
                dicoContinents.Add(i, continent[i]);
            }
        }
    }
}