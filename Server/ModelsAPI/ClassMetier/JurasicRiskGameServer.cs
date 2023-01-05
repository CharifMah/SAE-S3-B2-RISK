using ModelsAPI.ClassMetier.PartieTest;
using ModelsAPI.ClassMetier.Player;
using StackExchange.Redis;

namespace ModelsAPI.ClassMetier
{
    public class JurasicRiskGameServer
    {
        #region Attributes

        private List<Partie> _parties;

        #endregion

        #region Property

        public List<Partie> Parties { get => _parties; set => _parties = value; }

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
            Parties = new List<Partie>();
        }

        #endregion
    }
}
