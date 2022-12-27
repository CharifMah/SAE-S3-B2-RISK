using ModelsAPI.ClassMetier.Player;
using StackExchange.Redis;

namespace ModelsAPI.ClassMetier
{
    public class JurasicRiskGameServer
    {
        #region Attributes
        private List<Profil> _profils;
        #endregion

        #region Property



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
            _profils = new List<Profil>();
        }

        #endregion
    }
}
