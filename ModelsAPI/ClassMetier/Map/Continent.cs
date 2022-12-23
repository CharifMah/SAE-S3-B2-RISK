using ModelsAPI.Converters;
using System.Text.Json.Serialization;

namespace ModelsAPI.ClassMetier.Map
{
    /// <summary>
    /// Classe représentant les différents continents du plateau
    /// </summary>
    public class Continent : IContinent
    {
        #region Attributes
        private Dictionary<string, ITerritoireBase> _dicoTerritoires;
        #endregion

        #region Property

        [JsonConverter(typeof(ConcreteDictionnaryTypeConverter<Dictionary<string, ITerritoireBase>, TerritoireBase, string, ITerritoireBase>))]
        public Dictionary<string, ITerritoireBase> DicoTerritoires
        {
            get { return _dicoTerritoires; }
            set { _dicoTerritoires = value; }
        }

        #endregion

        #region Constructor
        public Continent(Dictionary<string, ITerritoireBase> DicoTerritoires)
        {
            this._dicoTerritoires = DicoTerritoires;
        }

        public Continent()
        {

        }
        #endregion

    }
}
