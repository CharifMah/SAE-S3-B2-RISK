using Stockage.Converters;
using System.Text.Json.Serialization;

namespace ModelsAPI.ClassMetier.Map
{
    /// <summary>
    /// Classe représentant les différents continents du plateau
    /// </summary>
    public class Continent : IContinent
    {
        #region Attributes
        private ITerritoireBase[] _territoires;
        #endregion

        #region Property

        public ITerritoireBase[] Territoires
        {
            get { return _territoires; }
            set { _territoires = value; }
        }

        #endregion

        #region Constructor
        public Continent(ITerritoireBase[] Territoires)
        {
            this._territoires = Territoires;
        }

        public Continent()
        {

        }
        #endregion

    }
}
