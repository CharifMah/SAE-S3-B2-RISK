using DBStorage.ClassMetier.Units;
using Microsoft.Graph;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JsonConstructorAttribute = System.Text.Json.Serialization.JsonConstructorAttribute;

namespace DBStorage.ClassMetier.Map
{
    /// <summary>
    /// Classe générique des territoires
    /// </summary>
    public class TerritoireBase : ITerritoireBase
    {
        protected Teams _teams;
        protected List<IUnit> _troupe;
        protected int _id;

        public Teams Team { get => _teams; set => _teams = value; }
        public int ID { get => _id; set => _id = value; }
        //[JsonProperty(ItemConverterType = typeof(List<UniteBase>))]
        //public List<IUnit> Units
        //{
        //    get => _troupe;
        //    set => _troupe = value;
        //}

        public TerritoireBase(int id)
        {
            _id = id;
            _teams = Teams.NEUTRE;
            _troupe = new List<IUnit>();
        }

        public TerritoireBase(List<IUnit> Units, int id)
        {
            _id = id;
            _teams = Teams.NEUTRE;
            _troupe = Units;
        }

        public void AddUnit(IUnit UniteBase)
        {
            _troupe.Add(UniteBase);
        }

        public void RemoveUnit(IUnit UniteBase)
        {
            _troupe.Remove(UniteBase);
        }

        [JsonConstructor]
        public TerritoireBase()
        {

        }
    }
}