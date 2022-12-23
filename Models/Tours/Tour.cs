using Models.Player;

namespace Models.Tours
{
    public class Tour : ITour
    {
        private Joueur _joueur;
        private bool _tourEnd;

        public bool TourEnd => throw new NotImplementedException();

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
