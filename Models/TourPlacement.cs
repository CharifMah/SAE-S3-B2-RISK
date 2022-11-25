using Models.Player;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TourPlacement : ITour
    {
        private bool _tourEnd;
        private Joueur _joueur;
        public TourPlacement(Joueur joueur)
        {
            _tourEnd = false;
            _joueur = joueur;
        }

        public void PlaceUnits()
        {
            if (_joueur.Troupe.Count > 0)
            {
                _joueur.AddUnit(_joueur.Troupe, JurasicRiskGame.Instance.Carte.SelectedTerritoire);
            }
        }

        public void TerminerTour()
        {

            _tourEnd = true;
        }
    }
}
