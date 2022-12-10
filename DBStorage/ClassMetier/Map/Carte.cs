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

        private Dictionary<int, IContinent> dicoContinents;

        private ITerritoireBase _selectedTerritoire;

        #endregion

        #region Property
        public Dictionary<int, IContinent> DicoContinents
        {
            get { return dicoContinents; }
        }
        [JsonConverter(typeof(InterfaceConverter<TerritoireBase>))]
        public ITerritoireBase SelectedTerritoire
        {
            get => _selectedTerritoire;
            set => _selectedTerritoire = value;
        }

        #endregion

        public Carte(Dictionary<int, IContinent> DicoContinents,ITerritoireBase SelectedTerritoire)
        {
            this.dicoContinents = DicoContinents;
            this.SelectedTerritoire= SelectedTerritoire;
        }

        [JsonConstructor]
        public Carte()
        {
            _selectedTerritoire = new TerritoireBase();
            dicoContinents = new Dictionary<int, IContinent>();
            for (int i = 0; i < DicoContinents.Count; i++)
            {
                dicoContinents.Add(i, DicoContinents[i]);
            }
        }
    }
}