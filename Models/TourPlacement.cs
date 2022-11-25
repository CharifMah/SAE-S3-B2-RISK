using Models.Player;
using Models.Units;

namespace Models
{
    public class TourPlacement : ITour
    {
        private bool _tourEnd;

        public bool TourEnd => _tourEnd;

        public TourPlacement()
        {
            _tourEnd = false;
        }

        public void PlaceUnits(UniteBase unitToPlace, Joueur _joueur)
        {
            if (_joueur.Troupe.Count > 0)
            {
                _joueur.AddUnits(new List<UniteBase>() { unitToPlace }, JurasicRiskGame.Instance.Carte.SelectedTerritoire);
            }
        }

        public void TerminerTour()
        {
            _tourEnd = true;
        }
    }
}
