using Microsoft.Graph;
using ModelsAPI.ClassMetier.Player;
using Newtonsoft.Json;
using Redis.OM.Modeling;
using Stockage.Converters;

namespace ModelsAPI.ClassMetier.Map
{
    /// <summary>
    /// Classe du plateau de jeu
    /// </summary>
    [Document(StorageType = StorageType.Json, Prefixes = new[] { "Carte" })]
    public class Carte
    {
        #region Attributes

        private Dictionary<string, IContinent> _dicoContinents;

        private TerritoireBase? _selectedTerritoire;

        #endregion

        #region Property
        ///// <summary>
        ///// Dictionary des Continents
        ///// </summary>
        [Indexed]
        [JsonConverter(typeof(ConcreteDictionnaryTypeConverter<Dictionary<string, IContinent>, Continent, string, IContinent>))]
        public Dictionary<string, IContinent> DicoContinents
        {
            get { return _dicoContinents; }
            set { _dicoContinents = value; }
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
                foreach (Continent continent in _dicoContinents.Values)
                {
                    foreach (ITerritoireBase territoire in continent.DicoTerritoires.Values)
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

        public Carte(Dictionary<string, IContinent> DicoContinents, TerritoireDecorator? SelectedTerritoire)
        {
            this._dicoContinents = DicoContinents;
            this._selectedTerritoire = SelectedTerritoire;
        }

        public TerritoireBase GetTerritoire(int ID)
        {
            foreach (Continent continent in _dicoContinents.Values)
            {
                foreach (TerritoireBase territoire in continent.DicoTerritoires.Values)
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