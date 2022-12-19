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

        [JsonConverter(typeof(DictionnaryContinentConverter))]
        public Dictionary<int, IContinent> DicoContinents
        {
            get { return _dicoContinents; }
            set { _dicoContinents = value; }
        }

        [JsonConverter(typeof(InterfaceConverter<TerritoireBase>))]
        public ITerritoireBase? SelectedTerritoire
        {
            get => _selectedTerritoire;
            set => _selectedTerritoire = value;
        }

        public int NombreTerritoireOccupe { get => GetNombreTerritoireOccupe(); }

        #endregion

        public Carte(Dictionary<int, IContinent> _dicoContinents = null, ITerritoireBase _selectedTerritoire = null)
        {
            this._dicoContinents = _dicoContinents;
            this._selectedTerritoire = _selectedTerritoire;
        }

        [JsonConstructor]
        public Carte()
        {

        }

        public int GetNombreTerritoireOccupe()
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
}