using System.Runtime.Serialization;

namespace Models.Units
{
    [DataContract]
    public class UniteBase : IUnit
    {
        [DataMember]
        private Elements element;
        [DataMember]
        protected int id;
        [DataMember]
        protected string name;
        [DataMember]
        protected string description;

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public int Id { get => id; set => id = value; }
        public Elements Element { get => element; set => element = value; }

        public UniteBase(int Id, Elements element = Elements.EAU)
        {
            this.element = element;
            this.id = id;
            this.name = "ExempleUnite";
            this.description = "Description d'une unite";
        }

        public UniteBase()
        {

        }
    }
}
