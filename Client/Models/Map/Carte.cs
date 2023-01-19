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
    /// <summary>
    /// Classe du plateau de jeu
    /// </summary>
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
        public IContinent[] Continents
        {
            get { return _continents; }
            set { _continents = value; }
        }

        public int GetNbTerritoireLibre
        {
            get
            {
                int res = 0;
                foreach (Continent continent in _continents)
                {
                    foreach (ITerritoireBase territoire in continent.Territoires)
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

        /// <summary>
        /// Carte du jeux
        /// </summary>
        /// <param name="Continents"> Continent de la carte</param>
        /// <param name="SelectedTerritoire">territoire selectionnée</param>
        public Carte(IContinent[] Continents, ITerritoireBase? SelectedTerritoire)
        {
            this._continents = Continents;
            this._selectedTerritoire = SelectedTerritoire;
        }

        /// <summary>
        /// Recupère les terriroire d'un joueur
        /// </summary>
        /// <param name="j"></param>
        /// <returns></returns>
        public List<TerritoireBase> GetPlayerTerritory(Joueur j)
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

        /// <summary>
        /// Get un territoire
        /// </summary>
        /// <param name="ID">l'id du territoir</param>
        /// <returns></returns>
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