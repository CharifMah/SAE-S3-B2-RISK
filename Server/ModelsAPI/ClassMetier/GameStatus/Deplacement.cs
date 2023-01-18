using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAPI.ClassMetier.GameStatus
{
    public class Deplacement : Etat
    {
        public bool Action(Carte carte, Joueur joueur, List<int> unitList)
        {
            throw new NotImplementedException();
        }

        public Etat TransitionTo(List<Joueur> joueurs, Carte carte)
        {
            // Passer au joueur suivant
            throw new NotImplementedException();
        }
    }
}
