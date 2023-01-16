using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Player;

namespace ModelsAPI.ClassMetier.GameStatus
{
    public class Deploiment : Etat
    {
        public void Action(Carte carte, Joueur joueur)
        {
            joueur.PlaceUnits(joueur.Units[0], carte.SelectedTerritoire);
        }



        public Etat TransitionTo()
        {
            return new Deploiment();
        }

        public override string? ToString()
        {
            return "Deploiment";
        }
    }
}
