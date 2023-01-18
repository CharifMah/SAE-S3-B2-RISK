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

        /// <summary>
        /// Phase de renforcement
        /// </summary>
        /// <param name="carte"></param>
        /// <param name="joueur"></param>
        /// <param name="unitList"></param>
        /// <returns></returns>
        public bool Action(Carte carte, Joueur joueur, List<int> unitList)
        {
            // Calcul des renforts du joueur
            int nbTerritoires = 0;
            int nbRenforts = 0;

            foreach(Continent c in carte.DicoContinents.Values)
            {
                foreach(TerritoireBase t in c.DicoTerritoires.Values)
                {
                    if(t.Team == joueur.Team)
                    {
                        nbTerritoires++;
                    }
                }
            }

            if(nbTerritoires/3 < 3)
            {
                nbRenforts = 3;
            }
            else
            {
                nbRenforts = nbTerritoires / 3;
            }
            

            // Ajout des renforts au joueur
            FabriqueUniteBase f = new FabriqueUniteBase();
            for (int i = 0; i < nbRenforts; i++)
            {
                Random random = new Random();
                switch (random.Next(3))
                {
                    case 1:
                        joueur.Units.Add(f.Create("Rex"));
                        break;
                    case 2:
                        joueur.Units.Add(f.Create("Brachiosaure"));
                        break;
                    case 3:
                        joueur.Units.Add(f.Create("Baryonyx"));
                        break;
                    case 4:
                        joueur.Units.Add(f.Create("Pterosaure"));
                        break;
                }
            }

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
