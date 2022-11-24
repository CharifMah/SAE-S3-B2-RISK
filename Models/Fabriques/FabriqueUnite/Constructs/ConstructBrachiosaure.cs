using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Fabrique.FabriqueUnite;

namespace Models.Units.Constructs
{
    public class ConstructBrachiosaure : IMakeUnit
    {
        /// <summary>
        /// Instancie un objet de type Brachiosaure
        /// </summary>
        /// <returns>Renvoie un Brachiosaure</returns>
        /// <author>VERCHERE Brian</author>
        public Unite MakeUnit()
        {
            return new Brachiosaure();
        }
    }
}
