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
        public int Id { get => id; }
        public Elements Element { get => element; set => element = value; }

        public UniteBase(int id, Elements element = Elements.EAU)
        {
            this.element = element;
            this.id = id;
            name = "ExempleUnite";
            description = "Description d'une unite";
        }
    }
}
