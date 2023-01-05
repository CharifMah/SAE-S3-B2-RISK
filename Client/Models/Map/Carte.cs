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

        private Dictionary<string, IContinent> _dicoContinents;
        private ITerritoireBase? _selectedTerritoire;

        #endregion

        #region Property
        [DataMember]
        ///// <summary>
        ///// Dictionary des Continents
        ///// </summary>
        [JsonConverter(typeof(ConcreteDictionnaryTypeConverter<Dictionary<string, IContinent>, Continent, string, IContinent>))]
        public Dictionary<string, IContinent> DicoContinents
        {
            get { return _dicoContinents; }
            set { _dicoContinents = value; }
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
                foreach (Continent continent in _dicoContinents.Values)
                {
                    foreach (ITerritoireBase? territoire in continent.DicoTerritoires.Values)
                    {
                        if (territoire.Team == Teams.NEUTRE)
                        {
                            res++;
                        }
                    }
                }
                return res;
            }
        }

        #endregion

        public Carte(Dictionary<string, IContinent> DicoContinents, ITerritoireBase? SelectedTerritoire)
        {
            this._dicoContinents = DicoContinents;
            this._selectedTerritoire = SelectedTerritoire;
        }

        public List<TerritoireBase> getPlayerTerritory(Joueur j)
        {
            List<TerritoireBase> res = new List<TerritoireBase>();
            foreach(Continent continent in _dicoContinents.Values)
            {
                foreach(TerritoireBase territoire in continent.DicoTerritoires.Values)
                {
                    if(territoire.Team == j.Team)
                    {
                        res.Add(territoire);
                    }
                }
            }
            return res;
        }
    }
}