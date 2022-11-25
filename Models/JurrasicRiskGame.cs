using Models.Map;
using Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class JurrasicRiskGame
    {
        private Carte _carte;
        private List<Joueur> _joueurs;
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


        #endregion
        #region Singleton

        private static JurrasicRiskGame _instance;
        public static JurrasicRiskGame Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new JurrasicRiskGame();
                }
                return _instance;
            }
        }

        #endregion

        private JurrasicRiskGame()
        {

        }


    }
}
