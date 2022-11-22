using Models.Units;

namespace Models.Map
{
    /// <summary>
    /// Classe générique des territoires
    /// </summary>
    public class TerritoireBase
    {
        protected Teams _teams;
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