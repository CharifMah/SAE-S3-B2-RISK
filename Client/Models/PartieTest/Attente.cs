using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PartieTest
{
    public class Attente
    {
        protected Partie partie;
        public void SetContext(Partie partie)
        {
            this.partie = partie;
        }
    }
}
