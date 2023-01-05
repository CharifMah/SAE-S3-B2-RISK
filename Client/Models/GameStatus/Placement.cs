using Models.Player;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.GameStatus
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
                _joueur.AddUnits(unitToPlace, JurasicRiskGameClient.Get.Lobby.Partie.Carte.SelectedTerritoire);
            }
        }

        public void SetContext(Partie partie)
        {
            this.partie = partie;
        }
    }
}
