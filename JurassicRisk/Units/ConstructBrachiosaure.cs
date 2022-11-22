using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurassicRisk.Units
{
    public class ConstructBrachiosaure : IMakeUnit
    {
        public Unite MakeUnit()
        {
            return new Brachiosaure();
        }
    }
}
