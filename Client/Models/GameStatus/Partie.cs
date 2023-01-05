using Models.Map;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.GameStatus
{
    public class Partie
    {
        private Etat etat = null;

        private Carte _carte;

        public Carte Carte { get => _carte; set => _carte = value; }

        public Partie(Etat etat, Carte carte)
        {
            TransitionTo(etat);
            this.Carte = carte;
        }

        #region Attributes
        private Etat etat = null;

        private Lobby lobby;
        #endregion

        #region Property
        public Lobby Lobby { get => lobby; }
        #endregion

        #region Constructor

        public Partie(Etat etat, Lobby lobby)
        {
            TransitionTo(etat);
            this.lobby = lobby;
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
