using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ModelsAPI.ClassMetier.Units
{
    public class UniteBase : IUnit
    {
        #region Attributes
        protected Elements element;
        protected int id;
        protected string name;
        protected string description;
        #endregion

        #region Property
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public int ID { get => id; set => id = value; }

        public Elements Element { get => element; set => element = value; }
        #endregion



        public UniteBase(int ID, Elements Element = Elements.EAU)
        {
            this.element = Element;
            this.id = ID;
            name = "ExempleUnite";
            description = "Description d'une unite";
        }

        public UniteBase()
        {

        }
    }
}
