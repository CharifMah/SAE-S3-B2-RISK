using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PartieTest
{
    public class Partie
    {
        private Etat etat = null;

        private Lobby lobby;
        
        public Lobby Lobby { get => lobby; }

        public Partie(Etat etat,Lobby lobby)
        {
            TransitionTo(etat);
            this.lobby = lobby;
        }

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
