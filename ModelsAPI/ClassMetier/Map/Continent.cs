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
        private Dictionary<string, TerritoireDecorator> _dicoTerritoires;
        #endregion

        #region Property

        [JsonConverter(typeof(ConcreteDictionnaryTypeConverter<Dictionary<string, ITerritoireBase>, TerritoireDecorator, string, ITerritoireBase>))]
        public Dictionary<string, TerritoireDecorator> DicoTerritoires
        {
            get { return _dicoTerritoires; }
            set { _dicoTerritoires = value; }
        }

        #endregion

        #region Constructor
        public Continent(Dictionary<string, TerritoireDecorator> DicoTerritoires)
        {
            this._dicoTerritoires = DicoTerritoires;
        }

        public Continent()
        {

        }
        #endregion

    }
}
