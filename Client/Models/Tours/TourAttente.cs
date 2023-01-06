using Models.Player;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Tours
{
    public class TourAttente : ITour
    {
        private bool _tourEnd;

        public bool TourEnd => _tourEnd;

        public TourAttente()
        {
            _tourEnd = false;
        }

        public void PlaceUnits(IUnit unitToPlace, Joueur _joueur)
        {
        }

        public void TerminerTour()
        {
            _tourEnd = true;
        }
    }
}
