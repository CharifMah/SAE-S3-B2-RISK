using Models.Map;
using Models.Player;

namespace Models.GameStatus
{
    public class Deploiment : Etat
    {
        private int idTerritoireUpdate;
        private int idUniteRemove;

        public int IdTerritoireUpdate { get => idTerritoireUpdate; set => idTerritoireUpdate = value; }
        public int IdUniteRemove { get => idUniteRemove; set => idUniteRemove = value; }

        public bool Action(Carte carte, Joueur joueur, List<int> unitIndex)
        {
            return false;
        }

        public Etat TransitionTo(List<Joueur> joueurs, Carte carte)
        {
            return null;
        }

        public override string? ToString()
        {
            return "Deploiment";
        }
    }
}
