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

        public Continent(List<ITerritoireBase> DicoTerritoires)
        {
            _dicoTerritoires = new Dictionary<int, ITerritoireBase>();
            for (int i = 0; i < DicoTerritoires.Count; i++)
            {
                _dicoTerritoires.Add(i, DicoTerritoires[i]);
            }
        }

        [JsonConstructor]
        public Continent()
        {

        }
    }
}
