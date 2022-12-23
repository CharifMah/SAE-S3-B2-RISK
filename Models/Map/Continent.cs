using ModelsAPI.Converters;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Models.Map
{
    /// <summary>
    /// Classe représentant les différents continents du plateau
    /// </summary>


    [DataContract]
    [KnownType(typeof(Continent))]
    [KnownType(typeof(TerritoireDecorator))]
    public class Continent : IContinent
    {
        #region Attributes

        private Dictionary<string, ITerritoireBase> _dicoTerritoires;
        #endregion

        #region Property

        [DataMember]
        [JsonConverter(typeof(ConcreteDictionnaryTypeConverter<Dictionary<string, ITerritoireBase>, TerritoireDecorator, string, ITerritoireBase>))]
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
        #endregion
    }
}
