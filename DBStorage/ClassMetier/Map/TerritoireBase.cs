using DBStorage.ClassMetier.Units;
using System.Runtime.Serialization;

namespace DBStorage.ClassMetier.Map
{
    /// <summary>
    /// Classe générique des territoires
    /// </summary>
    [KnownType(typeof(TerritoireBase))]
    [DataContract]
    public class TerritoireBase : ITerritoireBase
    {
        [DataMember]
        protected Teams _teams;
        [DataMember]
        protected List<IUnit> _troupe;
        [DataMember]
        protected int _id;

        public Teams Team { get => _teams; set => _teams = value; }
        public int ID { get => _id; set => _id = value; }

        public List<IUnit> Units
        {
            get => _troupe;
            set => _troupe = value;
        }

        public TerritoireBase(int id)
        {
            _id = id;
            _teams = Teams.NEUTRE;
            _troupe = new List<IUnit>();
        }

        public TerritoireBase(List<IUnit> troupe, int id)
        {
            _id = id;
            _teams = Teams.NEUTRE;
            _troupe = troupe;
        }

        public void AddUnit(IUnit UniteBase)
        {
            _troupe.Add(UniteBase);
        }

        public void RemoveUnit(IUnit UniteBase)
        {
            _troupe.Remove(UniteBase);
        }
    }
}