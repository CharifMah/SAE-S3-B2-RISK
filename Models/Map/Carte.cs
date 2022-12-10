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
        private Dictionary<int, Continent> _dicoContinents;

        private ITerritoireBase _selectedTerritoire;

        #endregion

        #region Property

        public Dictionary<int, Continent> DicoContinents
        {
            get { return _dicoContinents; }
        }

        public int NombreTerritoireOccupe { get => GetNombreTerritoireOccupe(); }

        public ITerritoireBase SelectedTerritoire
        {
            get => _selectedTerritoire;
            set => _selectedTerritoire = value;
        }

        #endregion

        public Carte(Dictionary<int, Continent> _dicoContinents, ITerritoireBase _selectedTerritoire = null)
        {
            this._dicoContinents = _dicoContinents;
            this._selectedTerritoire = _selectedTerritoire;
        }   
        
        public int GetNombreTerritoireOccupe()
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
}