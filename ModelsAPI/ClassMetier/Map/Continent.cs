using ModelsAPI.Converters;
using System.Text.Json.Serialization;

namespace ModelsAPI.ClassMetier.Map
{
    /// <summary>
    /// Classe représentant les différents continents du plateau
    /// </summary>
    public class Continent : IContinent
    {
        private Dictionary<int, ITerritoireBase> _dicoTerritoires;

        [JsonConverter(typeof(DictionnaryTerritoireConverter))]
        public Dictionary<int, ITerritoireBase> DicoTerritoires
        {
            get { return _dicoTerritoires; }
            set { _dicoTerritoires = value; }
        }

        public Continent(Dictionary<int, ITerritoireBase> _dicoTerritoires)
        {
            this._dicoTerritoires = _dicoTerritoires;       
        }
    }
}
