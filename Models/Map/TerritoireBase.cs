using Models.Units;

namespace Models.Map
{
    /// <summary>
    /// Classe générique des territoires
    /// </summary>
    public class TerritoireBase
    {
        private List<Unite> troupe;
        private Teams _teams;
        public Teams Team
        {
            get => _teams;

        public Teams Team { get => this._teams; set => this._teams = value; }
        public List<IMakeUnit> Units
        {
            get => this._makeUnits;
            set => this._makeUnits = value;
        }

        public List<Unite> Troupe
        {
            get { return troupe; }
            set { troupe = value; }
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