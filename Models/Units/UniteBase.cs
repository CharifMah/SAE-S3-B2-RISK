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
    public class UniteBase
    {
        protected Elements element;
        protected int id;
        protected string name;
        protected string description;

        protected string Name { get => name; }
        protected string Description { get => description; }
        protected int Id { get => id; }
        protected Elements Element { get => element; set => element = value; }

        public UniteBase()
        {
            this.Element = Elements.EAU;
            this.id = 0;
            this.name = "ExempleUnite";
            this.description = "Description d'une unite";
        }
    }
}
