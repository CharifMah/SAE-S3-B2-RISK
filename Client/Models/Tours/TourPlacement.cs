﻿using Models.Map;
using Models.Player;
using Models.Units;
using System.Windows;

namespace Models.Tours
{
    public class TourPlacement : ITour
    {
        private bool _tourEnd;

        public bool TourEnd => _tourEnd;

        public TourPlacement()
        {
            _tourEnd = false;
        }

        public void PlaceUnits(IUnit unitToPlace, Joueur _joueur,ITerritoireBase territoireBase)
        {
            if (_joueur.Units.Count > 0)
            {
                _joueur.PlaceUnits(new List<IUnit>() { unitToPlace }, territoireBase);
            }
            else
            {
                MessageBox.Show("teste");
            }
        }

        public void TerminerTour()
        {
            _tourEnd = true;
        }
    }
}
