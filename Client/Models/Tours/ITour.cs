using Models.Map;
using Models.Player;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Tours
{
    public interface ITour
    {
        public bool TourEnd { get; }
        public void TerminerTour();
        public void PlaceUnits(IUnit unitToPlace, Joueur _joueur, ITerritoireBase territoireBase);

    }
}
