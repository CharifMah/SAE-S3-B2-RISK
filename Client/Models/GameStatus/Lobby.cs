using Models.Player;

namespace Models.GameStatus
{
    public class Lobby
    {
        #region Attributes
        private string _id;
        private string? _password;
        private List<Joueur> _joueurs;
        private string? _owner;

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
                    if (joueur.Team == Teams.NEUTRE || !joueur.IsReady)
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

        public string? Owner
        {
            get => _owner;
            set => _owner = value;
        }

        public string? Password
        {
            get => _password;
            set => _password = value;
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Create a Lobby
        /// </summary>
        /// <param name="Id">Id of the Lobby</param>
        /// <param name="Password">not required Password</param>
        public Lobby(string Id, string? Password = null)
        {
            _joueurs = new List<Joueur>();

            _id = Id;
            _password = Password;
        }

        public Lobby()
        {
            _joueurs = new List<Joueur>();
        }

        #endregion

        /// <summary>
        /// Rejoins le Lobby (ajoute le joueur dans la liste des joueurs)
        /// </summary>
        /// <param name="joueur">le joueur qui rejoint</param>
        /// <returns>vrai si le joueur a rejoin</returns>
        public bool JoinLobby(Joueur joueur)
        {
            bool res = false;
            IEnumerable<Joueur?> j = _joueurs.Where(x => x.Profil.Pseudo == joueur.Profil.Pseudo);
            if (_joueurs != null)
            {
                if (_joueurs.Count == 4 && j.Count() == 1)
                {
                    res = true;
                }
                if (_joueurs.Count < 4 && j.Count() == 0)
                {
                    _joueurs.Add(joueur);
                    res = true;
                }
                if (_owner == null)
                    _owner = _joueurs[0].Profil.Pseudo;
            }
            return res;
        }

        /// <summary>
        /// Quitte le Lobby retire le joueur de la liste
        /// </summary>
        /// <param name="joueur">Joueur qui quitte</param>
        /// <returns>vrai si le joueur a quitter</returns>
        public bool ExitLobby(Joueur joueur)
        {
            bool res = false;
            Joueur joueurToRemove = _joueurs.Find(x => x.Profil.Pseudo == joueur.Profil.Pseudo);
            if (joueurToRemove != null)
            {
                _joueurs.Remove(joueurToRemove);
                res = true;
            }
            else
            {
                throw new ArgumentNullException();
            }


            return res;
        }
    }
}
