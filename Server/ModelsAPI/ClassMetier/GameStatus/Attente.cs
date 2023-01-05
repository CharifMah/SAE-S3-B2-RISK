using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAPI.ClassMetier.GameStatus
{
    public class Attente : Etat
    {
        protected Partie partie;

        public void Attaquer()
        {
        }

        public void FinDeTour()
        {
        }

        public void PositionnerTroupe()
        {
            throw new NotImplementedException();
        }

        public void SetContext(Partie partie)
        {
            this.partie = partie;
        }

    }
}
