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

        //private Dictionary<int, Continent> _dicoContinents;

        private ITerritoireBase _selectedTerritoire;

        #endregion

        #region Property
        //public Dictionary<int, Continent> DicoContinents
        //{
        //    get { return _dicoContinents; }
        //    set { _dicoContinents = value; }
        //}

        [JsonConverter(typeof(InterfaceConverter<TerritoireBase>))]
        public ITerritoireBase SelectedTerritoire
        {
            get => _selectedTerritoire;
            set => _selectedTerritoire = value;
        }

        public int NombreTerritoireOccupe { get => GetNombreTerritoireOccupe(); }

        #endregion

        public Carte(ITerritoireBase selectedTerritoire)
        {
            //this._dicoContinents = _dicoContinents;
            this._selectedTerritoire = selectedTerritoire;
        }

        [JsonConstructor]
        public Carte()
        {
            //_selectedTerritoire = new TerritoireBase();
            //_dicoContinents = new Dictionary<int, Continent>();          
        }

        public int GetNombreTerritoireOccupe()
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
}