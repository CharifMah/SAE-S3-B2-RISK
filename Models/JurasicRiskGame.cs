using Models.Map;
using Models.Player;
using Models.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class JurasicRiskGame 
    {
        #region Attributes
        private HttpClient _client;
        private string _ip;
        private Carte _carte;
        private List<Joueur> _joueurs;
        private List<ITour> _tours;
        private TaskCompletionSource<bool> _clickWaitTask;
        #endregion

        #region Property

        public Carte Carte
        {
            get
            {
                return _carte;
            }
            set
            {
                _carte = value;
            }
        }

        public List<Joueur> Joueurs
        {
            get { return _joueurs; } set { _joueurs = value; } 
        }

        public HttpClient Client
        {
            get { return _client; }
        }

        public string Ip
        {
            get { return _ip; }
        }

        #endregion

        #region Singleton

        private static JurasicRiskGame _instance;
        public static JurasicRiskGame Get
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new JurasicRiskGame();
                }
                return _instance;
            }
        }

        #endregion


        private JurasicRiskGame()
        {
            _ip = "localhost:7215";
            _client = new HttpClient();
        }

        public async Task StartGame()
        {
            TourPlacement t = new TourPlacement();
            _clickWaitTask = new TaskCompletionSource<bool>();

            while (_carte.GetNombreTerritoireOccupe != 0)
            {
                foreach (Joueur joueur in _joueurs)
                {
                    t.PlaceUnits(joueur.Troupe[0], joueur);
                    
                    await _clickWaitTask.Task;
                }          
            }
        }

    }

   
}
