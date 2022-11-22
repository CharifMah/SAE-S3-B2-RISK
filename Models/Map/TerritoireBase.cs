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

            set {_teams = value;}
        }

        public List<Unite> Troupe
        {
            get { return troupe; }
            set { troupe = value; }
        }

        public TerritoireBase()
        {
            troupe = new List<Unite>();
        }

        public void MaJTroupe(List<Unite> unites)
        {
            foreach(var unit in unites)
            {
                troupe.Add(unit);
            }
        }
    }
}