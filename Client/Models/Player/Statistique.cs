using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Player
{
    public class Statistique
    {
        private string _partyPlay;
        private string _trpVaincus;
        private string _trpPerdu;
        private string _conquis;
        private string _conquisSmlt;

        public Statistique(string partyPlay, string trpVaincus, string trpPerdu, string conquis, string conquisSmlt)
        {
            PartyPlay = partyPlay;
            TrpVaincus = trpVaincus;
            TrpPerdu = trpPerdu;
            Conquis = conquis;
            ConquisSmlt = conquisSmlt;
        }

        public string PartyPlay { get => _partyPlay; set => _partyPlay = value; }
        public string TrpVaincus { get => _trpVaincus; set => _trpVaincus = value; }
        public string TrpPerdu { get => _trpPerdu; set => _trpPerdu = value; }
        public string Conquis { get => _conquis; set => _conquis = value; }
        public string ConquisSmlt { get => _conquisSmlt; set => _conquisSmlt = value; }
    }
}