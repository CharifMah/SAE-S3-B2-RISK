using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Player;
using ModelsAPI.ClassMetier.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAPI.ClassMetier.GameStatus
{
    public class Attaque : Etat
    {
        public void Action(Carte carte, Joueur joueur, List<int> unitList)
        {
            throw new NotImplementedException();
        }

        public Etat TransitionTo(List<Joueur> joueurs, Carte carte)
        {
            throw new NotImplementedException();
        }
        public override string? ToString()
        {
            return "Attaque";
        }

    }
}
