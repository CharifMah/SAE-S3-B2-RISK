using Models.Player;
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

        protected Teams _teams = Teams.NEUTRE;

        protected List<IUnit> _units;

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
            get => this._units;
            set => this._units = value;
        }

        #endregion


        public TerritoireBase(int id)
        {
            this._id = id;
            this._teams = Teams.NEUTRE;
            this._units = new List<IUnit>();
        }

        public TerritoireBase(int ID, List<IUnit> Units, Teams Team = Teams.NEUTRE)
        {
            this._id = ID;
            this._teams = Team;
            if (Units == null)
                this._units = new List<IUnit>();
            else
                this._units = Units;

        }

        public TerritoireBase()
        {
            this._units = new List<IUnit>();
        }

        public void AddUnit(IUnit UniteBase)
        {
            this._units.Add(UniteBase);
        }

        public void RemoveUnit(IUnit UniteBase)
        {
            this._units.Remove(UniteBase);
        }
    }
}