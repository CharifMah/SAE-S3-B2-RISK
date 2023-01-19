using Models.Player;
using Newtonsoft.Json;
using Stockage.Converters;
using System.Runtime.Serialization;

namespace Models.Map
{
    /// <summary>
    /// Classe du plateau de jeu
    /// </summary>
    [DataContract]
    [KnownType(typeof(TerritoireDecorator))]
    [KnownType(typeof(Continent))]
    public class Carte
    {
        #region Attributes

        private IContinent[] _continents;
        private ITerritoireBase? _selectedTerritoire;

        #endregion

        #region Property
        [DataMember]
        ///// <summary>
        ///// Dictionary des Continents
        ///// </summary>
        public IContinent[] DicoContinents
        {
            get { return _continents; }
            set { _continents = value; }
        }

        [DataMember]
        /// <summary>
        /// Le Territoire Selectionne par le joueur
        /// </summary>
        [JsonConverter(typeof(ConcreteConverter<ITerritoireBase?, TerritoireDecorator>))]
        public ITerritoireBase? SelectedTerritoire
        {
            get => _selectedTerritoire;
            set => _selectedTerritoire = value;
        }

        public int GetNombreTerritoireOccupe
        {
            get
            {
                int res = 0;
                foreach (Continent continent in _continents)
                {
                    res += continent.Territoires.Length;
                }
                return res;
            }
        }

        #endregion

        public Carte(IContinent[] DicoContinents, ITerritoireBase? SelectedTerritoire)
        {
            this._continents = DicoContinents;
            this._selectedTerritoire = SelectedTerritoire;
        }

        public List<TerritoireBase> getPlayerTerritory(Joueur j)
        {
            List<TerritoireBase> res = new List<TerritoireBase>();
            foreach (Continent continent in _continents)
            {
                foreach (TerritoireBase territoire in continent.Territoires)
                {
                    if (territoire.Team == j.Team)
                    {
                        res.Add(territoire);
                    }
                }
            }
            return res;
        }

        public ITerritoireBase GetTerritoire(int ID)
        {
            foreach (Continent continent in _continents)
            {
                foreach (ITerritoireBase territoire in continent.Territoires)
                {
                    if (territoire.ID == ID)
                    {
                        return territoire;
                    }
                }
            }
            return null;
        }
    }
}