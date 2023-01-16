using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAPI.ClassMetier.GameStatus
{
    public interface Etat
    {
        public Etat TransitionTo();

        public void Action(Carte carte, Joueur joueur);
    }
}
