using ModelsAPI.ClassMetier.Player;
using Redis.OM.Modeling;

namespace ModelsAPI.ClassMetier.Map
{
    /// <summary>
    /// Classe du plateau de jeu
    /// </summary>
    [Document(StorageType = StorageType.Json, Prefixes = new[] { "Carte" })]
    public class Carte
    {
        #region Attributes

        private IContinent[] _continents;

        private TerritoireBase? _selectedTerritoire;

        #endregion

        #region Property
        ///// <summary>
        ///// Dictionary des Continents
        ///// </summary>
        [Indexed]
        public IContinent[] Continent
        {
            get { return _continents; }
            set { _continents = value; }
        }

        /// <summary>
        /// Le Territoire Selectionne par le joueur
        /// </summary>
        [Indexed]
        public TerritoireBase? SelectedTerritoire
        {
            get => _selectedTerritoire;
            set => _selectedTerritoire = value;
        }


        [Indexed]
        public int GetNbTerritoireLibre
        {
            get
            {
                int res = 0;
                foreach (Continent continent in _continents)
                {
                    foreach (ITerritoireBase territoire in continent.Territoires)
                    {
                        if (territoire.Team == Teams.NEUTRE)
                        {
                            res++;
                        }
                    }
                }
                return res;
            }
        }

        #endregion

        /// <summary>
        /// Carte du jeux
        /// </summary>
        /// <param name="Continents"> Continent de la carte</param>
        /// <param name="SelectedTerritoire">territoire selectionnée</param>
        public Carte(IContinent[] Continents, TerritoireDecorator? SelectedTerritoire)
        {
            this._continents = Continents;
            this._selectedTerritoire = SelectedTerritoire;
        }

        /// <summary>
        /// Recupère les terriroire d'un joueur
        /// </summary>
        /// <param name="j"></param>
        /// <returns></returns>
        public List<TerritoireBase> GetPlayerTerritory(Joueur j)
        {
            List<TerritoireBase> res = new List<TerritoireBase>();
            foreach (Continent continent in _continents)
            {
                foreach (TerritoireBase territoire in continent.Territoires)
                {
                    if (territoire.Team == j.Team)
                    {
                        res.Add(territoire);
                    }
                }
            }
            return res;
        }
        /// <summary>
        /// Get un territoire
        /// </summary>
        /// <param name="ID">l'id du territoir</param>
        /// <returns></returns>
        public ITerritoireBase GetTerritoire(int ID)
        {
            foreach (Continent continent in _continents)
            {
                foreach (ITerritoireBase territoire in continent.Territoires)
                {
                    if (territoire.ID == ID)
                    {
                        return territoire;
                    }
                }
            }
            return null;
        }
    }
}