using Models.Map;
using Models.Player;

namespace Models.GameStatus
{
    public class Deplacement : Etat
    {
        public int IdTerritoireUpdate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int IdUniteRemove { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Action(Carte carte, Joueur joueur, List<int> unitList)
        {
            throw new NotImplementedException();
        }

        public Etat TransitionTo(List<Joueur> joueurs, Carte carte)
        {
            // Passer au joueur suivant
            throw new NotImplementedException();
        }
    }
}
