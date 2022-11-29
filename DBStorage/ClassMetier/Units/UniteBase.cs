using System.Runtime.Serialization;

namespace DBStorage.ClassMetier.Units
{
    [DataContract]
    public class UniteBase : IUnit
    {
        private Elements element;
        protected int id;
        protected string name;
        protected string description;

        public string Name { get => name; }
        public string Description { get => description; }
        public int Id { get => id; }
        public Elements Element { get => element; set => element = value; }

        public UniteBase()
        {
            Element = Elements.EAU;
            id = 0;
            name = "ExempleUnite";
            description = "Description d'une unite";
        }
    }
}
