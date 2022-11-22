using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Units
{
    public class FabriqueUnite
    {
        private Dictionary<string, IMakeUnit> constructors = new Dictionary<string, IMakeUnit>();

        public Unite Create(string type)
        {
            if (constructors.ContainsKey(type))
                return constructors[type].MakeUnit();
            return null;
        }

        public string[] Types
        {
            get { return constructors.Keys.ToArray(); }
        }
    }
}
