using Models.Map;
using Models.Player;

namespace Models.GameStatus
{
    public interface Etat
    {
        public Etat TransitionTo(List<Joueur> joueurs, Carte carte);

        public bool Action(Carte carte, Joueur joueur, List<int> unitList);
    }
}
