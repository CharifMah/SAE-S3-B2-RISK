using Newtonsoft.Json;
using Stockage.Converters;
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

        private ITerritoireBase[] _territoires;
        #endregion

        #region Property

        [DataMember]
        public ITerritoireBase[] Territoires
        {
            get { return _territoires; }
            set { _territoires = value; }
        }

        #endregion

        #region Constructor
        public Continent(ITerritoireBase[] DicoTerritoires)
        {
            this._territoires = DicoTerritoires;
        }
        #endregion
    }
}
