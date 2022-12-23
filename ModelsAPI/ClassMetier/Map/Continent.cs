using ModelsAPI.Converters;
using System.Text.Json.Serialization;

namespace ModelsAPI.ClassMetier.Map
{
    /// <summary>
    /// Classe représentant les différents continents du plateau
    /// </summary>
    public class Continent
    {
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
