using Microsoft.Graph;
using ModelsAPI.Converters;
using System.Text.Json.Serialization;

namespace ModelsAPI.ClassMetier.Map
{
    /// <summary>
    /// Classe du plateau de jeu
    /// </summary>
    public class Carte
    {
        #region Attributes

        private Dictionary<int, IContinent> _dicoContinents;

        private ITerritoireBase? _selectedTerritoire;

        #endregion

        #region Property
        /// <summary>
        /// Dictionary des Continents
        /// </summary>
        public Dictionary<int, IContinent> DicoContinents
        {
            get { return _dicoContinents; }
            set { _dicoContinents = value; }
        }

        /// <summary>
        /// Le Territoire Selectionne par le joueur
        /// </summary>
        public ITerritoireBase? SelectedTerritoire
        {
            get => _selectedTerritoire;
            set => _selectedTerritoire = value;
        }

        public int GetNombreTerritoireOccupe
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

        public Carte(Dictionary<int, IContinent> DicoContinents, ITerritoireBase? SelectedTerritoire)
        {
            this._dicoContinents = DicoContinents;
            this._selectedTerritoire = SelectedTerritoire;
        }
    }
}