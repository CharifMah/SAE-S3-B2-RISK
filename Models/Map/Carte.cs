using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models.Map
{
    /// <summary>
    /// Classe du plateau de jeu
    /// </summary>
    [DataContract]
    public class Carte
    {
        #region Attributes
        [DataMember]
        private Dictionary<string, Continent> _dicoContinents;

        private TerritoireBase? _selectedTerritoire;

        #endregion

        #region Property
        ///// <summary>
        ///// Dictionary des Continents
        ///// </summary>
        public Dictionary<string, Continent> DicoContinents
        {
            get { return _dicoContinents; }
            set { _dicoContinents = value; }
        }

        /// <summary>
        /// Le Territoire Selectionne par le joueur
        /// </summary>
        public TerritoireBase? SelectedTerritoire
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
                    foreach (ITerritoireBase territoire in continent.DicoTerritoires.Values)
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

        public Carte(Dictionary<string, Continent> DicoContinents, TerritoireBase? SelectedTerritoire)
        {
            this._dicoContinents = DicoContinents;
            this._selectedTerritoire = SelectedTerritoire;
        }
    }
}