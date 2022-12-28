using ModelsAPI.ClassMetier.Player;
using Redis.OM.Modeling;

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
        public Lobby(string Id, string? Password = null)
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


    }
}
