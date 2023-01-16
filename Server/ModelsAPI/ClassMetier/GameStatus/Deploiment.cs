using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Player;
using ModelsAPI.ClassMetier.Units;

namespace ModelsAPI.ClassMetier.GameStatus
{
    public class Deploiment : Etat
    {
        public void Action(Carte carte, Joueur joueur, List<IUnit> unitList)
        {
            joueur.PlaceUnits(unitList, carte.SelectedTerritoire);
        }



        public Etat TransitionTo()
        {
            return new Attaque();
        }

        public override string? ToString()
        {
            return "Deploiment";
        }
    }
}
