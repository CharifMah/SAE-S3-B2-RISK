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
    [KnownType(typeof(TerritoireDecorator))]
    [DataContract]
    public class Continent
    {
        [DataMember]
        private Dictionary<string, TerritoireBase> _dicoTerritoires;

        public Dictionary<string, TerritoireBase> DicoTerritoires
        {
            get { return _dicoTerritoires; }
            set { _dicoTerritoires = value; }
        }

        public Continent(Dictionary<string, TerritoireBase> _dicoTerritoires)
        {
            this._dicoTerritoires = _dicoTerritoires;
        }


    }
}
