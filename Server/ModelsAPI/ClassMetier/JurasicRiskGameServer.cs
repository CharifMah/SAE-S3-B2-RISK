namespace ModelsAPI.ClassMetier
{
    public class JurasicRiskGameServer
    {
        #region Attributes

        private List<Lobby> _lobby;

        #endregion

        #region Property

        public List<Lobby> Lobbys { get => _lobby; set => _lobby = value; }

        #endregion

        #region Singleton

        private static JurasicRiskGameServer _instance;

        public static JurasicRiskGameServer Get
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new JurasicRiskGameServer();
                }
                return _instance;
            }
        }


        private JurasicRiskGameServer()
        {
            Lobbys = new List<Lobby>();
        }

        #endregion
    }
}
