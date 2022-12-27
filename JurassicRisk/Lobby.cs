using Models;
using Models.Player;
using System.Collections.Generic;

namespace ModelsAPI.ClassMetier
{
    public class Lobby
    {
        #region Attributes

        private string _id;
        private List<Joueur> _joueurs;

        #endregion

        #region Property
        public string Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }
        public bool PlayersReady
        {
            get
            {
                bool res = true;

                foreach (Joueur joueur in _joueurs)
                {
                    if (joueur.Team == Teams.NEUTRE)
                    {
                        res = false;
                    }
                }
                return res;
            }
        }
        public List<Joueur> Joueurs
        {
            get { return _joueurs; }
        }

        #endregion

        #region Constructor

        public Lobby(List<Joueur> joueurs = null)
        {
            _joueurs = new List<Joueur>();

            if (joueurs != null && joueurs.Count > 0)
            {
                foreach (Joueur joueur in joueurs)
                {
                    _joueurs.Add(joueur);
                }
            }        
        }

        #endregion

        public bool JoinLobby(Joueur joueur)
        {
            bool res = false;

            if (_joueurs.Count < 4)
            {
                _joueurs.Add(joueur);
                res = true;
            }

            return res;
        }
    }
}
