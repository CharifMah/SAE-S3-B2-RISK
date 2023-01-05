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

        private Etat etat = null;

        private List<Joueur> _joueurs;

        private Carte _carte;

        #endregion

        #region Property
        public Carte Carte { get => _carte; set => _carte = value; }

        public List<Joueur> Joueurs
        {
            get { return _joueurs; }
        }

        #endregion

        #region Constructor

        public Partie(Etat etat, Carte carte, List<Joueur> joueur)
        {
            TransitionTo(etat);
            this._carte = carte;
            this._joueurs = joueur;
        }

        #endregion

        public void TransitionTo(Etat etat)
        {
            Console.WriteLine($"Context: Transition to {etat.GetType().Name}.");
            this.etat = etat;
            etat.SetContext(this);
        }

        public void PositionnerTroupe()
        {
            etat.PositionnerTroupe();
        }

        public void FinDeTour()
        {
            etat.FinDeTour();
        }
    }
}
