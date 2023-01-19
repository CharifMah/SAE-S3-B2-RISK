using Models.Map;
using Models.Player;

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
