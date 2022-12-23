using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models.Map
{
    /// <summary>
    /// Classe représentant les différents continents du plateau
    /// </summary>
    [KnownTypeAttribute(typeof(ITerritoireBase))]
    [DataContract]
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
