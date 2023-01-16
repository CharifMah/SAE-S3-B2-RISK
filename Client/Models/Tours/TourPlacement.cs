using Models.Player;
using Models.Units;

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

        public void PlaceUnits(IUnit unitToPlace, Joueur _joueur)
        {
            if (_joueur.Units.Count > 0)
            {
                _joueur.PlaceUnits(new List<IUnit>() { unitToPlace }, JurasicRiskGameClient.Get.Partie.Carte.SelectedTerritoire);
            }
        }

        public void TerminerTour()
        {
            _tourEnd = true;
        }
    }
}
