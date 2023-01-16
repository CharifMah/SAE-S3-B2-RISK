using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Player;
using ModelsAPI.ClassMetier.Units;

namespace ModelsAPI.ClassMetier.GameStatus
{
    public class Deploiment : Etat
    {
        public void Action(Carte carte, Joueur joueur, List<int> unitIndex)
        {
            if (unitIndex.Count == 1)
                joueur.PlaceUnits(joueur.Units[unitIndex[0]], carte.SelectedTerritoire);
        }

        public Etat TransitionTo(List<Joueur> joueurs)
        {
            int nbTroupe = 0;
            Etat etatSuivant = null;
            foreach(Joueur j in joueurs)
            {
                nbTroupe += j.Units.Count;
            }

            if (nbTroupe <= 0)
            {
                etatSuivant = new Attaque();
            }
            else
            {
                etatSuivant = new Deploiment();

            }
            Console.WriteLine($"nouveau tour de {etatSuivant.ToString()}");
            return etatSuivant;
        }

        public override string? ToString()
        {
            return "Deploiment";
        }
    }
}
