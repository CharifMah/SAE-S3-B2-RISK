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

        public Etat TransitionTo(List<Joueur> joueurs, Carte carte)
        {
            Etat etatSuivant;
            if (carte.GetNbTerritoireLibre <= 0)
            {
                etatSuivant = new Renforcement();
            }
            else
            {
                etatSuivant = new Deploiment();

            }
            Console.WriteLine($"nouveau tour de {etatSuivant}");
            return etatSuivant;
        }

        public override string? ToString()
        {
            return "Deploiment";
        }
    }
}
