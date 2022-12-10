using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DBStorage.ClassMetier.Units
{
    public class UniteBase : IUnit
    {
        private Elements element;
        protected int id;
        protected string name;
        protected string description;

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public int Id { get => id; set => id = value; }
        public Elements Element { get => element; set => element = value; }

        [JsonConstructor]
        public UniteBase()
        {
            Element = Elements.EAU;
            id = 0;
            name = "ExempleUnite";
            description = "Description d'une unite";
        }
    }
}
