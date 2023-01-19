using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Player;
using ModelsAPI.ClassMetier.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.GameStatus
{
    public interface Etat
    {
        public int IdTerritoireUpdate { get; set; }
        public int IdUniteRemove { get; set; }

        public Etat TransitionTo(List<Joueur> joueurs, Carte carte);

        public bool Action(Carte carte, Joueur joueur, List<int> unitList);
    }
}
