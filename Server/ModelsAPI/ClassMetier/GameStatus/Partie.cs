using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Player;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAPI.ClassMetier.GameStatus
{
    public class Partie
    {
        #region Attributes
        private int _playerIndex;

        private Etat etat = null;

        private List<Joueur> _joueurs;

        private Carte _carte;

        private string _id;
        #endregion

        #region Property
        public Carte Carte { get => _carte; set => _carte = value; }

        public List<Joueur> Joueurs
        {
            get { return _joueurs; }
        }

        public Etat Etat { get { return etat; } }

        public string Id { get => _id; set => _id = value; }
        #endregion

        #region Constructor

        public Partie(Carte carte, List<Joueur> joueurs, string id)
        {
            this._carte = carte;
            this._joueurs = joueurs;
            this._id = id;
            this.etat = new Deploiment();
            _playerIndex = -1;
        }

        #endregion

        public int NextPlayer()
        {
            _playerIndex++;
            _playerIndex %= _joueurs.Count;

            return _playerIndex;
        }

        public void Action()
        {
            etat.Action(this._carte, this._joueurs[_playerIndex]);
        }
    }
}
