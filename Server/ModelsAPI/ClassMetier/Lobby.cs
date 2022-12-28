using ModelsAPI.ClassMetier.Player;
using Newtonsoft.Json;
using Redis.OM.Modeling;
using Stockage.Converters;

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

        /// <summary>
        /// Rejoins le lobby (ajoute le joueur dans la liste des joueurs)
        /// </summary>
        /// <param name="joueur">le joueur qui rejoint</param>
        /// <returns>vrai si le joueur a rejoin</returns>
        public bool JoinLobby(Joueur joueur)
        {
            bool res = false;
            List<Joueur> joueurs = _joueurs.FindAll(x => x.Profil.Pseudo == joueur.Profil.Pseudo);
            if (joueurs != null)
            {
                foreach (Joueur j in joueurs)
                {
                    _joueurs.Remove(j);
                }
            }
            if (_joueurs.Count < 4)
            {
                _joueurs.Add(joueur);
                res = true;
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
