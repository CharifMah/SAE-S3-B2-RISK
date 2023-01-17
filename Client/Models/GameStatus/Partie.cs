using Models.Map;
using Models.Player;

namespace Models.GameStatus
{
    public class Partie
    {
        #region Attributes
        private int _playerIndex;

        private List<Joueur> _joueurs;

        private Carte _carte;

        private string _id;

        private string _owner;
        #endregion

        #region Property
        public Carte Carte { get => _carte; set => _carte = value; }

        public List<Joueur> Joueurs
        {
            get { return _joueurs; }
        }

        public string Id { get => _id; set => _id = value; }
        public int PlayerIndex { get => _playerIndex; }
        public string Owner { get => _owner; set => _owner = value; }
        #endregion

        #region Constructor

        public Partie(Carte carte, List<Joueur> joueurs, string id)
        {
            _carte = carte;
            _joueurs = joueurs;
            _id = id;

            _playerIndex = -1;
        }

        #endregion
      
    }
}
