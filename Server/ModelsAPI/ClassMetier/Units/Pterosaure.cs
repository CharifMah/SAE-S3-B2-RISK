using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAPI.ClassMetier.Units
{
    internal class Pterosaure : UniteBase
    {
        public Pterosaure(int id, Elements element = Elements.EAU) : base(id, element)
        {
            this.id = id;
            this.element = element;
            name = "Pterosaure";
            description = "this is a Pterosaure";
        }
    }
}
