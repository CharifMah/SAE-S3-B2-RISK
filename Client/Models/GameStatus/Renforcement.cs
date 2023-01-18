using Models.Map;
using Models.Player;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.GameStatus
{
    public class Renforcement: Etat
    {
        private Joueur _joueurActuel;

        public Joueur JoueurActuel { get => _joueurActuel; set => _joueurActuel = value; }

        /// <summary>
        /// Phase de renforcement
        /// </summary>
        /// <param name="carte"></param>
        /// <param name="joueur"></param>
        /// <param name="unitList"></param>
        /// <returns></returns>
        public bool Action(Carte carte, Joueur joueur, List<int> unitList)
        {
            return false;
        }

        public Etat TransitionTo(List<Joueur> joueurs, Carte carte)
        {
            return null;
        }

        public override string? ToString()
        {
            return "Renforcement";
        }
    }
}
