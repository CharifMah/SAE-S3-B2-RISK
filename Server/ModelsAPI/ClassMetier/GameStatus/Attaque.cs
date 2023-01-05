using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAPI.ClassMetier.GameStatus
{
    public class Attaque
    {
        protected Lobby partie;
        public void SetContext(Lobby partie)
        {
            this.partie = partie;
        }
    }
}
