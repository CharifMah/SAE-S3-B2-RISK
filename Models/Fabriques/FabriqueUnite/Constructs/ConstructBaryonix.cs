using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Fabriques.FabriqueUnite;
using Models.Units;

namespace Models.Fabriques.FabriqueUnite.Constructs
{
    public class ConstructBaryonyx : IMakeUnit
    {
        /// <summary>
        /// Instancie un objet de type Brachiosaurus
        /// </summary>
        /// <returns>Renvoie un Brachiosaurus</returns>
        /// <author>VERCHERE Brian</author>
        public UniteBase MakeUnit()
        {
            return new Baryonyx();
        }
    }
}
