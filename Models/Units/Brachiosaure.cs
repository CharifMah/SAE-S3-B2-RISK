using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Units
{
    internal class Brachiosaure : UniteBase
    {
        public Brachiosaure()
        {
            this.Element = Elements.EAU;
            this.id = 0;
            this.name = "ExempleUnite";
            this.description = "Description d'une unite";
        }
    }
}
