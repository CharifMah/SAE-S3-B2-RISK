using Models.Player;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAPI.ClassMetier.GameStatus
{
    public class Placement : Etat
    {
        protected Lobby partie;

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
                _joueur.AddUnits(unitToPlace, JurasicRiskGameClient.Get.Carte.SelectedTerritoire);
            }
        }

        public void SetContext(Lobby partie)
        {
            this.partie = partie;
        }
    }
}
