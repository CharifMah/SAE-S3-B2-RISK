using ModelsAPI.ClassMetier.Player;
using Newtonsoft.Json;
using Pipelines.Sockets.Unofficial.Buffers;
using Redis.OM.Modeling;
using Stockage.Converters;

namespace ModelsAPI.ClassMetier
{
    [Document(StorageType = StorageType.Json, Prefixes = new[] { "Lobby" })]
    public class Lobby
    {
        #region Attributes

        private string _id;
        private string _password;
        private List<Joueur> _joueurs;
        private string _owner;

        #endregion

        #region Property
        [RedisIdField]
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

        [Indexed]
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
        [Indexed]
        public List<Joueur> Joueurs
        {
            get { return _joueurs; }
        }
        [Indexed]
        public string? Owner
        {
            get => _owner;
            set => _owner = value;
        }
        [Indexed]
        public string? Password
        {
            get => _password;
            set => _password = value;
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Create a lobby
        /// </summary>
        /// <param name="Id">Id of the lobby</param>
        /// <param name="Password">not required Password</param>
        public Lobby(string Id,string? Password = null)
        {
            _joueurs = new List<Joueur>();

            this._id = Id;
            this._password = Password;
        }

        public Lobby() 
        {
            _joueurs = new List<Joueur>();
        }

        #endregion

        /// <summary>
        /// Rejoins le lobby (ajoute le joueur dans la liste des joueurs)
        /// </summary>
        /// <param name="joueur">le joueur qui rejoint</param>
        /// <returns>vrai si le joueur a rejoin</returns>
        public bool JoinLobby(Joueur joueur)
        {
            bool res = false;
            if (_joueurs != null)
            {
                if (_joueurs.Count < 4)
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
        /// Quitte le lobby retire le joueur de la liste
        /// </summary>
        /// <param name="joueur">Joueur qui quitte</param>
        /// <returns>vrai si le joueur a quitter</returns>
        public bool ExitLobby(Joueur joueur)
        {
            bool res = false;
            Joueur? j = _joueurs.FindLast(x => x.Profil.Pseudo == joueur.Profil.Pseudo);
            if (j != null)
            {
                _joueurs.Remove(j);
                res = true;
            }

            return res;
        }
    }
}
