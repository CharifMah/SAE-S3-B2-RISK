using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Player;
using ModelsAPI.ClassMetier.Units;

namespace ModelsAPI.ClassMetier.GameStatus
{
    public class Renforcement : Etat
    {

        public void Action(Carte carte, Joueur joueur, List<int> unitList)
        {
            throw new NotImplementedException();
        }

        public Etat TransitionTo(List<Joueur> joueurs, Carte carte)
        {
            int nbTroupe = 0;
            Etat etatSuivant;
            foreach(Joueur j in joueurs)
            {
                if (j.Units.Count <= 0)
                {
                    nbTroupe++;
                }
            }
            if (nbTroupe <= 0)
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
