using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.GameStatus
{
    public class Deplacement : Etat
    {
        public int IdTerritoireUpdate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int IdUniteRemove { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
