namespace Models.Map
{
    /// <summary>
    /// Classe générique des territoires
    /// </summary>
    public class TerritoireBase
    {
        private Teams _teams;
        public Teams Team
        {
            get => Teams.NEUTRE;

            set {_teams = value;}
        }
        public TerritoireBase()
        {

        }
    }
}