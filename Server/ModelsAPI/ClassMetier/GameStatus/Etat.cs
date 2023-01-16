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
    public interface Etat
    {
        public Etat TransitionTo(List<Joueur> joueurs);

        public void Action(Carte carte, Joueur joueur, List<int> unitList);
    }
}
