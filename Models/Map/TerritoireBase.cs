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
        [DataMember]
        protected Teams _teams;
        [DataMember]
        protected List<UniteBase> _troupe;
        [DataMember]
        protected int _id;

        public Teams Team { get => this._teams; set => this._teams = value; }
        public int ID { get => this._id; set => this._id = value; }

        public List<UniteBase> Units
        {
            get => this._troupe;
            set => this._troupe = value;
        }

        public TerritoireBase(int id)
        {
            this._id = id;
            this._teams = Teams.NEUTRE;
            this._troupe = new List<UniteBase>();
        }

        public TerritoireBase(List<UniteBase> troupe, int id)
        {
            this._id = id;
            this._teams = Teams.NEUTRE;
            this._troupe = troupe;
        }

        /// <summary>
        /// Ajoute une UniteBase
        /// </summary>
        /// <param name="UniteBase">UniteBase</param>
        public void AddUnit(UniteBase UniteBase)
        {
            _troupe.Add(UniteBase);
        }
    }
}