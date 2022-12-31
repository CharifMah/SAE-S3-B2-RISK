using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Player
{
    public class Statistique
    {
        /// <summary>
        /// Statistique du joueur
        /// </summary>
        /// <param name="stats"></param>
        public Statistique(string stats)
        {

            PartyPlay = stats;
            TrpVaincus = stats + "1";
            TrpPerdu = stats + "2";
            Conquis = stats + "3";
            ConquisSmlt = stats + "4";
        }

        public string PartyPlay { get; set; }
        public string TrpVaincus { get; set; }
        public string TrpPerdu { get; set; }
        public string Conquis { get; set; }
        public string ConquisSmlt { get; set; }
    }
}