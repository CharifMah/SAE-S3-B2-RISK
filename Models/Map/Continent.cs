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
        [DataMember]
        private Dictionary<string, ITerritoireBase> _dicoTerritoires;

        public Dictionary<string, ITerritoireBase> DicoTerritoires
        {
            get { return _dicoTerritoires; }
            set { _dicoTerritoires = value; }
        }
        public Continent(Dictionary<string, ITerritoireBase> _dicoTerritoires)
        {
            this._dicoTerritoires = _dicoTerritoires;
        }


    }
}
