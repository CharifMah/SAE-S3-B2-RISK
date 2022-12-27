using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAPI.ClassMetier.Units
{
    public class Rex : UniteBase
    {
        public Rex(int id, Elements element = Elements.EAU) : base(id, element)
        {
            this.id = id;
            this.element = element;

            name = "Rex";
            description = "this is a Rex";
        }
    }
}
