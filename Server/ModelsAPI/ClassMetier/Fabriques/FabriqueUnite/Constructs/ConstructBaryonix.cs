using ModelsAPI.ClassMetier.Units;

namespace ModelsAPI.ClassMetier.Fabriques.FabriqueUnite.Constructs
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
