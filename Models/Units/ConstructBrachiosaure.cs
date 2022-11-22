using JurassicRisk.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Units
{
    public class ConstructBrachiosaure : IMakeUnit
    {
        public Unite MakeUnit()
        {
            return new Brachiosaure();
        }
    }
}
