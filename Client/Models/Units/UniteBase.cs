using System.Runtime.Serialization;

namespace Models.Units
{
    [DataContract]
    public class UniteBase : IUnit
    {
        protected Elements element;
        protected int id;
        protected string name;
        protected string description;

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public int Id { get => id; set => id = value; }
        public Elements Element { get => element; set => element = value; }

        public UniteBase(int Id,string Description, string Name,Elements Element = Elements.EAU)
        {
            this.element = Element;
            this.id = Id;
            this.name = Name;
            this.description = Description;
        }

        public UniteBase() 
        { }
    }
}
