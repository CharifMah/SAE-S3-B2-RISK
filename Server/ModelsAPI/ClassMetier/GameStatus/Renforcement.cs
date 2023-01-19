using ModelsAPI.ClassMetier.Fabriques.FabriqueUnite;
using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Player;
using ModelsAPI.ClassMetier.Units;

namespace ModelsAPI.ClassMetier.GameStatus
{
    public class Renforcement : Etat
    {
        private Joueur _joueurActuel;

        public Joueur JoueurActuel { get => _joueurActuel; set => _joueurActuel = value; }
        public int IdTerritoireUpdate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int IdUniteRemove { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Phase de renforcement
        /// </summary>
        /// <param name="carte"></param>
        /// <param name="joueur"></param>
        /// <param name="unitList"></param>
        /// <returns></returns>
        public bool Action(Carte carte, Joueur joueur, List<int> unitList)
        {
          

            // Ajout des unités sur le territoire
            List<IUnit> unitsToPlace = new List<IUnit>();
            foreach(int i in unitList)
            {
                unitsToPlace.Add(joueur.Units[i]);
            }
            joueur.PlaceUnits(unitsToPlace, carte.SelectedTerritoire);

            return true;
        }

        public Etat TransitionTo(List<Joueur> joueurs, Carte carte)
        {
            Etat etatSuivant;
            if (_joueurActuel.Units.Count <= 0)
            {
                etatSuivant = new Attaque();
            }
            else
            {
                etatSuivant = new Renforcement();
            }
            Console.WriteLine($"nouveau tour de {etatSuivant}");
            return etatSuivant;
        }

        public override string? ToString()
        {
            return "Renforcement";
        }
    }
}
