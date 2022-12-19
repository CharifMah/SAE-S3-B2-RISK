using ModelsAPI.ClassMetier.Units;
using System.Text.Json.Serialization;

namespace ModelsAPI.ClassMetier.Map
{
    /// <summary>
    /// Classe générique des territoires
    /// </summary>

    public class TerritoireBase : ITerritoireBase
    {
        protected Teams _teams;
        protected List<IUnit>? units;
        protected int _id;

        public Teams Team { get => _teams; set => _teams = value; }
        public int ID { get => _id; set => _id = value; }

        [JsonConverter(typeof(List<IUnit>))]
        public List<IUnit>? Units
        {
            get => units;
            set => units = value;
        }

        public TerritoireBase(int id)
        {
            _id = id;
            _teams = Teams.NEUTRE;
            units = new List<IUnit>();
        }


        public TerritoireBase(List<IUnit> units, int ID)
        {
            _id = ID;
            _teams = Teams.NEUTRE;
            this.units = units;
        }

        public void AddUnit(IUnit UniteBase)
        {
            units.Add(UniteBase);
        }

        public void RemoveUnit(IUnit UniteBase)
        {
            units.Remove(UniteBase);
        }

        [JsonConstructor]
        public TerritoireBase()
        {

        }
    }
}