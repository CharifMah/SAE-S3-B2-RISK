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
        private Dictionary<int, Continent> dicoContinents;

        #endregion

        #region Property

        public Dictionary<int, Continent> DicoContinents
        {
            get { return dicoContinents; }
        }

        public int NombreTerritoireOccupe { get => GetNombreTerritoireOccupe(); }

        #endregion

        public Carte(List<Continent> continent)
        {
            dicoContinents = new Dictionary<int, Continent>();
            for (int i = 0; i < continent.Count; i++)
            {
                dicoContinents.Add(i, continent[i]);
            }
        }   
        
        public int GetNombreTerritoireOccupe()
        {
            int res = 0;
            foreach (Continent continent in dicoContinents.Values)
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