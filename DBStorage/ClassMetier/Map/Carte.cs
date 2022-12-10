using Microsoft.Graph;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DBStorage.ClassMetier.Map
{
    /// <summary>
    /// Classe du plateau de jeu
    /// </summary>
    public class Carte
    {
        #region Attributes

        private Dictionary<int, IContinent> _dicoContinents;

        private ITerritoireBase _selectedTerritoire;

        #endregion

        #region Property
        public Dictionary<int, IContinent> DicoContinents
        {
            get { return _dicoContinents; }
        }
        [JsonConverter(typeof(InterfaceConverter<TerritoireBase>))]
        public ITerritoireBase SelectedTerritoire
        {
            get => _selectedTerritoire;
            set => _selectedTerritoire = value;
        }

        #endregion

        public Carte(Dictionary<int, IContinent> _dicoContinents, ITerritoireBase _selectedTerritoire = null)
        {
            this._dicoContinents = _dicoContinents;
            this._selectedTerritoire = _selectedTerritoire;
        }

        [JsonConstructor]
        public Carte()
        {
            _selectedTerritoire = new TerritoireBase();
            _dicoContinents = new Dictionary<int, IContinent>();          
        }
    }
}