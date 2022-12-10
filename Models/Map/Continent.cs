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
    public class Continent : IContinent
    {
        [DataMember]
        private Dictionary<int, ITerritoireBase> _dicoTerritoires;

        public Dictionary<int, ITerritoireBase> DicoTerritoires
        {
            get { return _dicoTerritoires; }
            set { _dicoTerritoires = value; }
        }

        public Continent(List<ITerritoireBase> territoires)
        {
            _dicoTerritoires = new Dictionary<int, ITerritoireBase>();
            for (int i = 0; i < territoires.Count; i++)
            {
                _dicoTerritoires.Add(i, territoires[i]);
            }
        }


    }
}
