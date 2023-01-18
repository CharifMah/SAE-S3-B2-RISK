using Models.Map;
using Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.GameStatus
{
    public class Attaque : Etat
    {
        public bool Action(Carte carte, Joueur joueur, List<int> unitList)
        {
            throw new NotImplementedException();
        }

        public Etat TransitionTo(List<Joueur> joueurs, Carte carte)
        {
            throw new NotImplementedException();
        }
    }
}
