using ModelsAPI.ClassMetier.GameStatus;

namespace ModelsAPI.ClassMetier
{
    public class JurasicRiskGameServer
    {
        #region Attributes

        private List<Lobby> _lobby;
        private List<Partie?> _partie;
        #endregion

        #region Property

        public List<Lobby> Lobbys { get => _lobby; set => _lobby = value; }

        public List<Partie?> Parties 
        { 
            get => _partie; 

            set => _partie = value; 
        }

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
            _lobby = new List<Lobby>();
            _partie = new List<Partie>();
        }
        #endregion
    }
}
