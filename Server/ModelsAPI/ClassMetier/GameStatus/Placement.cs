using ModelsAPI.ClassMetier.Player;
using ModelsAPI.ClassMetier.Units;

namespace ModelsAPI.ClassMetier.GameStatus
{
    public class Placement : Etat
    {
        protected Partie partie;

        public void Attaquer()
        {
        }

        public void FinDeTour()
        {
            throw new NotImplementedException();
        }

        public void PositionnerTroupe()
        {
            // PlaceUnits();
        }
        public void PlaceUnits(List<IUnit> unitToPlace, Joueur _joueur)
        {
            if (_joueur.Units.Count > 0)
            {
                _joueur.AddUnits(unitToPlace, partie.Carte.SelectedTerritoire);
            }
        }

        public void SetContext(Partie partie)
        {
            this.partie = partie;
        }
    }
}
