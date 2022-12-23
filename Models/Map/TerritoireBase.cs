using Models.Units;
using System.Runtime.Serialization;

namespace Models.Map
{
    /// <summary>
    /// Classe générique des territoires
    /// </summary>
    [KnownType(typeof(TerritoireBase))]
    [DataContract]
    public class TerritoireBase : ITerritoireBase
    {
        #region Attributes

        protected Teams _teams;

        protected List<IUnit> units;

        protected int _id;
        #endregion

        #region Property
        [DataMember]
        public Teams Team { get => this._teams; set => this._teams = value; }
        [DataMember]
        public int ID { get => this._id; set => this._id = value; }
        [DataMember]
        public List<IUnit> Units
        {
            get => this.units;
            set => this.units = value;
        }

        #endregion


        public TerritoireBase(int id)
        {
            this._id = id;
            this._teams = Teams.NEUTRE;
            this.units = new List<IUnit>();
        }

        public TerritoireBase(int ID, List<IUnit> Units, Teams Team = Teams.NEUTRE)
        {
            this._id = ID;
            this._teams = Team;
            this.units = Units;
        }

        public TerritoireBase()
        {

        }

        public void AddUnit(IUnit UniteBase)
        {
            this.units.Add(UniteBase);
        }

        public void RemoveUnit(IUnit UniteBase)
        {
            this.units.Remove(UniteBase);
        }
    }
}