using Models.Units;
using System.Runtime.Serialization;

namespace Models.Map
{
    /// <summary>
    /// Classe générique des territoires
    /// </summary>
    [DataContract]
    public class TerritoireBase : ITerritoireBase
    {
        [DataMember]
        protected Teams _teams;
        [DataMember]
        protected List<IMakeUnit> _makeUnits;

        public Teams Team { get => this._teams; set => this._teams = value; }
        public List<IMakeUnit> Units
        {
            get => this._makeUnits;
            set => this._makeUnits = value;
        }
        public TerritoireBase()
        {
            this._teams = Teams.NEUTRE;
            this._makeUnits = new List<IMakeUnit>();
        }
        public TerritoireBase(List<IMakeUnit> makeUnits)
        {
            this._teams = Teams.NEUTRE;
            this._makeUnits = makeUnits;
        }
    }
}