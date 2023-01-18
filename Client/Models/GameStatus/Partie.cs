using Models.Map;
using Models.Player;

namespace Models.GameStatus
{
    public class Partie
    {
        #region Attributes
        private int _playerIndex;

        private List<Joueur> _joueurs;

        private Etat _etat = null;

        private Carte _carte;

        private string _id;

        private string _owner;
        #endregion

        #region Property
        public Carte Carte { get => _carte; set => _carte = value; }

        public List<Joueur> Joueurs
        {
            get { return _joueurs; }
            set { _joueurs = value;}
        }

        public string Id { get => _id; set => _id = value; }
        public int PlayerIndex { get => _playerIndex;  set { _playerIndex = value; } }
        public string Owner { get => _owner; set => _owner = value; }


        public Etat Etat { get { return _etat; } set => _etat = value; }
        #endregion

        #region Constructor

        public Partie(Carte carte, List<Joueur> joueurs, string id, Etat etat,int playerindex)
        {
            this._carte = carte;
            this._joueurs = joueurs;
            this._id = id;
            _playerIndex = playerindex;
            this._etat = etat;
        }

        #endregion
      
    }
}
