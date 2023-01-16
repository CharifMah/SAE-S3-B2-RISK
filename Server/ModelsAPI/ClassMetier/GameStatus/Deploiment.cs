using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAPI.ClassMetier.GameStatus
{
    public class Deploiment : Etat
    {
        public void Action(Carte carte, List<Joueur> joueur)
        {
            if (carte.SelectedTerritoire.Team == Teams.NEUTRE)
            {

            }
            
        }

        public Etat TransitionTo()
        {
            return new Deploiment();
        }


    }
}
