using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PartieTest
{
    public interface Etat
    {
        public void SetContext(Partie partie);

        public void PositionnerTroupe();

        public void Attaquer();

        public void FinDeTour();
    }
}
