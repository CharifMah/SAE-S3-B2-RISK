using Models.Map;
using Models.Player;

namespace ModelsAPI.ClassMetier.GameStatus
{
    public interface Etat
    {
        public Etat TransitionTo(List<Joueur> joueurs, Carte carte);

        public void Action(Carte carte, Joueur joueur, List<int> unitList);
    }
}
