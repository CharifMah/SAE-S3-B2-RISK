using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ModelsAPI.ClassMetier.Units
{
    public class UniteBase : IUnit
    {
        protected Elements element;
        protected int id;
        protected string name;
        protected string description;

        public string Name { get => name; }
        public string Description { get => description; }
        public int ID { get => id; }

        public Elements Element { get => element; set => element = value; }
        string IUnit.Name { get; set ; }

        string IUnit.Description { get; set; }
        int IUnit.ID { get; set; }

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
