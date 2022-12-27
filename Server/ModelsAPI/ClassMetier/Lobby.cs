using ModelsAPI.ClassMetier.Player;
using Redis.OM.Modeling;

namespace ModelsAPI.ClassMetier
{
    [Document(StorageType = StorageType.Json, Prefixes = new[] { "Lobby" })]
    public class Lobby
    {
        #region Attributes

        private string _id;
        private List<Joueur> _joueurs;

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

        #endregion

        #region Constructor

        public Lobby(List<Joueur>? joueurs = null)
        {
            if (joueurs == null)
                _joueurs = new List<Joueur>();
            else
                _joueurs = joueurs;

        }

        #endregion

        public bool JoinLobby(Joueur joueur)
        {
            bool res = false;

            if (_joueurs.Count < 4)
            {
                _joueurs.Add(joueur);
            }

            return res;
        }
    }
}
