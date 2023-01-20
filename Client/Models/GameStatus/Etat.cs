using Models.Map;
using Models.Player;

namespace Models.GameStatus
{
    public interface Etat
    {
        public int IdTerritoireUpdate { get; set; }
        public int IdUniteRemove { get; set; }
        /// <summary>
        /// TransitionTo next State
        /// </summary>
        /// <param name="joueurs"></param>
        /// <param name="carte"></param>
        /// <returns>State</returns>
        public Etat TransitionTo(List<Joueur> joueurs, Carte carte);

        /// <summary>
        /// Action of the State
        /// </summary>
        /// <param name="carte">carte</param>
        /// <param name="joueur">joueur</param>
        /// <param name="unitList">list d'unitée pour action</param>
        /// <returns>si l'action a ete realisée</returns>
        public bool Action(Carte carte, Joueur joueur, List<int> unitList);
    }
}
