using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Units;

namespace Models.Fabrique.FabriqueUnite
{
    public class FabriqueUnite
    {
        private Dictionary<string, IMakeUnit> constructors = new Dictionary<string, IMakeUnit>();

        /// <summary>
        /// Initialise les constructeurs d'unité
        /// </summary>
        /// <author>VERCHERE Brian</author>
        public FabriqueUnite()
        {
            constructors["brachiosaure"] = new ConstructBrachiosaure();
        }

        /// <summary>
        /// Créer une unité en fonction du type en paramètre
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Renvoie une unité</returns>
        /// <author>VERCHERE Brian</author>
        public Unite Create(string type)
        {
            if (constructors.ContainsKey(type))
                return constructors[type].MakeUnit();
            return null;
        }

        /// <summary>
        /// Renvoie tout les types étant considérés comme des unités
        /// </summary>
        /// <author>VERCHERE Brian</author>
        public string[] Types
        {
            get { return constructors.Keys.ToArray(); }
        }
    }
}
