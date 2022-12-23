using ModelsAPI.ClassMetier.Units;
using ModelsAPI.Converters;
using Newtonsoft.Json;

namespace ModelsAPI.ClassMetier.Map
{
    /// <summary>
    /// Classe générique des territoires
    /// </summary>

    public class TerritoireBase : ITerritoireBase
    {
        protected Teams _teams;
        protected List<IUnit>? _units;
        protected int _id;

        public Teams Team { get => _teams; set => _teams = value; }
        public int ID { get => _id; set => _id = value; }

        //[JsonConverter(typeof(ConcreteListConverter<IUnit,UniteBase>))]
        //public List<IUnit>? Units
        //{
        //    get => _units;
        //    set => _units = value;
        //}

        public TerritoireBase(int id)
        {
            _id = id;
            _teams = Teams.NEUTRE;
            _units = new List<IUnit>();
        }

        [JsonConstructor]
        public TerritoireBase(int ID, Teams Team = Teams.NEUTRE)
        {
            this._id = ID;
            this._teams = Team;
            //this._units = Units;
        }

        public TerritoireBase()
        {

        }


        public void AddUnit(IUnit UniteBase)
        {
            _units.Add(UniteBase);
        }

        public void RemoveUnit(IUnit UniteBase)
        {
            _units.Remove(UniteBase);
        }
    }
}