using Models.Units;

namespace Models.Map
{
    /// <summary>
    /// Classe générique des territoires
    /// </summary>
    public class TerritoireBase
    {
        private List<IMakeUnit> troupe;
        private Teams _teams;
        public Teams Team
        {
            get => Teams.NEUTRE;

            set {_teams = value;}
        }
        public TerritoireBase()
        {
            troupe = new List<IMakeUnit>();
        }

        public void MaJTroupe(List<IMakeUnit> unites)
        {
            foreach(var unit in unites)
            {
                troupe.Add(unit);
            }
        }
    }
}