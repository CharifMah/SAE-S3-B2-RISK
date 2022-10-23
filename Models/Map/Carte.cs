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

        public List<Continent> Continent
        {
            get { return continent; }
        }

        public Carte(List<Continent> c)
        {
            continent = c;
        }
    }
}