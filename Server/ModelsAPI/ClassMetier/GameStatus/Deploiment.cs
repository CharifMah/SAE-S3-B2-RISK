using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Player;
using ModelsAPI.ClassMetier.Units;

namespace ModelsAPI.ClassMetier.GameStatus
{
    public class Deploiment : Etat
    {
        private int idTerritoireUpdate;
        private int idUniteRemove;

        public int IdTerritoireUpdate { get => idTerritoireUpdate; set => idTerritoireUpdate = value; }
        public int IdUniteRemove { get => idUniteRemove; set => idUniteRemove = value; }

        public bool Action(Carte carte, Joueur joueur, List<int> unitIndex)
        {
            bool res = false;
            if (unitIndex.Count == 1 && carte.SelectedTerritoire.Team == Teams.NEUTRE)
            {
                joueur.PlaceUnits(joueur.Units[unitIndex[0]], carte.SelectedTerritoire);
                this.idTerritoireUpdate = carte.SelectedTerritoire.ID;
                this.idUniteRemove = unitIndex[0];
                res = true;
            }
            return res;
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
