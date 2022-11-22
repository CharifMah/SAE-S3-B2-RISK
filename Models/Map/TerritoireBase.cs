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

        public Teams Team { get => this._teams; set => this._teams = value; }
        public List<Unite> Units
        {
            get => this.troupe;
            set => this.troupe = value;
        }

        public List<Unite> Troupe
        {
            get { return troupe; }
            set { troupe = value; }
        }

        public TerritoireBase()
        {
            this._teams = Teams.NEUTRE;
            this.troupe = new List<Unite>();
        }
        public TerritoireBase(List<Unite> makeUnits)
        {
            this._teams = Teams.NEUTRE;
            this.troupe = makeUnits;
        }
    }
}