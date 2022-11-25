﻿using Models.Player;

namespace Models
{
    public class Tour : ITour
    {
        private Joueur _joueur;
        private bool _tourEnd;
        public Tour(Joueur joueur)
        {
            _joueur = joueur;
            _tourEnd = false;
        }

        public void Attack()
        {

        }

        public void Move()
        {

        }

        public void Draft() 
        { 

        }

        public void TerminerTour()
        {
            _tourEnd = true;
        }
    }
}
