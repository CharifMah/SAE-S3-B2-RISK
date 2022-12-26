using Microsoft.Graph;
using Newtonsoft.Json;
using Stockage.Converters;

namespace ModelsAPI.ClassMetier.Map
{
    /// <summary>
    /// Classe du plateau de jeu
    /// </summary>
    public class Carte
    {
        #region Attributes

        private Dictionary<string, IContinent> _dicoContinents;

        private TerritoireDecorator? _selectedTerritoire;

        #endregion

        #region Property
        ///// <summary>
        ///// Dictionary des Continents
        ///// </summary>
        [JsonConverter(typeof(ConcreteDictionnaryTypeConverter<Dictionary<string, IContinent>, Continent, string, IContinent>))]
        public Dictionary<string, IContinent> DicoContinents
        {
            get { return _dicoContinents; }
            set { _dicoContinents = value; }
        }

        /// <summary>
        /// Le Territoire Selectionne par le joueur
        /// </summary>
        public TerritoireDecorator? SelectedTerritoire
        {
            get => _selectedTerritoire;
            set => _selectedTerritoire = value;
        }

        public int GetNombreTerritoireOccupe
        {
            get
            {
                int res = 0;
                //foreach (Continent continent in _dicoContinents.Values)
                //{
                //    foreach (ITerritoireBase territoire in continent.DicoTerritoires.Values)
                //    {
                //        if (territoire.Team == Teams.NEUTRE)
                //        {
                //            res++;
                //        }
                //    }
                //}
                return res;
            }
        }

        #endregion

        public Carte(Dictionary<string, IContinent> DicoContinents, TerritoireDecorator? SelectedTerritoire)
        {
            this._dicoContinents = DicoContinents;
            this._selectedTerritoire = SelectedTerritoire;
        }
    }
}