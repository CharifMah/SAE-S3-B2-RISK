using ModelsAPI.ClassMetier.Fabriques.FabriqueUnite.Constructs;
using ModelsAPI.ClassMetier.Units;

namespace ModelsAPI.ClassMetier.Fabriques.FabriqueUnite
{
    public class FabriqueUniteBase
    {
        private Dictionary<string, IMakeUnit> constructors = new Dictionary<string, IMakeUnit>();

        /// <summary>
        /// Initialise les constructeurs d'unité
        /// </summary>
        /// <author>VERCHERE Brian</author>
        public FabriqueUniteBase()
        {
            constructors["Brachiosaurus"] = new ConstructBrachiosaurus();
            constructors["Rex"] = new ConstructRex();
            constructors["Baryonyx"] = new ConstructBaryonyx();
            constructors["Pterosaure"] = new ConstructPterosaure();
        }

        /// <summary>
        /// Créer une unité en fonction du type en paramètre
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Renvoie une unité</returns>
        /// <author>VERCHERE Brian</author>
        public UniteBase Create(string type)
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
