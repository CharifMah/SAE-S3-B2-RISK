using Models.Map;
using Models.Player;

namespace Models.GameStatus
{
    public class Attaque : Etat
    {
        public int IdTerritoireUpdate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int IdUniteRemove { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Action(Carte carte, Joueur joueur, List<int> unitList)
        {
            throw new NotImplementedException();
        }

        public Etat TransitionTo(List<Joueur> joueurs, Carte carte)
        {
            Etat etatSuivant = new Deplacement();
            Console.WriteLine($"Passage à la phase de {etatSuivant}");
            return etatSuivant;
        }
        public override string? ToString()
        {
            return "Attaque";
        }

    }
}
