using Models.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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

        public string Name { get => name; }
        public string Description { get => description; }
        public int Id { get => id; }
        public Elements Element { get => element; set => element = value; }

        public UniteBase()
        {
            this.Element = Elements.EAU;
            this.id = 0;
            this.name = "ExempleUnite";
            this.description = "Description d'une unite";
        }
    }
}
