using Newtonsoft.Json;
using JsonConstructorAttribute = Newtonsoft.Json.JsonConstructorAttribute;

namespace DBStorage.ClassMetier.Map
{
    /// <summary>
    /// Classe représentant les différents continents du plateau
    /// </summary>
    public class Continent
    {
        private Dictionary<int, ITerritoireBase> dicoTerritoires;

        [JsonProperty(ItemConverterType = typeof(Dictionary<int, TerritoireBase>))]
        public Dictionary<int, ITerritoireBase> DicoTerritoires
        {
            get { return dicoTerritoires; }
            set { dicoTerritoires = value; }
        }


        public Continent(List<ITerritoireBase> DicoTerritoires)
        {
            dicoTerritoires = new Dictionary<int, ITerritoireBase>();
            for (int i = 0; i < DicoTerritoires.Count; i++)
            {
                dicoTerritoires.Add(i, DicoTerritoires[i]);
            }
        }

        [JsonConstructor]
        public Continent()
        {

        }
    }
}
