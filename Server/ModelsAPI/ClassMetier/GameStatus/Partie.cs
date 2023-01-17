using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Player;
using ModelsAPI.ClassMetier.Units;
using Pipelines.Sockets.Unofficial.Buffers;
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
        private string _owner;
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
        public int PlayerIndex { get => _playerIndex; }
        public string Owner { get => _owner; set => _owner = value; }
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

        public void Action(List<int> unitList)
        {
            etat.Action(this._carte, this._joueurs[_playerIndex], unitList);
        }

        public void Transition()
        {
            this.etat = etat.TransitionTo(this._joueurs, this._carte);
        }

        /// <summary>
        /// Rejoins le lobby (ajoute le joueur dans la liste des joueurs)
        /// </summary>
        /// <param name="joueur">le joueur qui rejoint</param>
        /// <returns>vrai si le joueur a rejoin</returns>
        public bool JoinPartie(Joueur joueur)
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
            }
            return res;
        }

        /// <summary>
        /// Quitte le lobby retire le joueur de la liste
        /// </summary>
        /// <param name="joueur">Joueur qui quitte</param>
        /// <returns>vrai si le joueur a quitter</returns>
        public bool ExitPartie(Joueur joueur)
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
