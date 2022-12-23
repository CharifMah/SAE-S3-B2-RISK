using ModelsAPI.Converters;
using System.Text.Json.Serialization;

namespace ModelsAPI.ClassMetier.Map
{
    /// <summary>
    /// Classe représentant les différents continents du plateau
    /// </summary>
    public class Continent : IContinent
    {
        //private Dictionary<string, ITerritoireBase> _dicoTerritoires;

        //[JsonConverter(typeof(ConcreteDictionnaryTypeConverter<Dictionary<string, ITerritoireBase>, TerritoireBase, string, ITerritoireBase>))]
        //public Dictionary<string, ITerritoireBase> DicoTerritoires
        //{
        //    get { return _dicoTerritoires; }
        //    set { _dicoTerritoires = value; }
        //}

        //public Continent(Dictionary<string, ITerritoireBase> _dicoTerritoires)
        //{
        //    this._dicoTerritoires = _dicoTerritoires;       
        //}

        public Continent()
        {

        }
    }
}
