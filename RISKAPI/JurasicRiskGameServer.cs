using StackExchange.Redis;

namespace RISKAPI
{
    public class JurasicRiskGameServer
    {
        #region Attributes

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

        }

        #endregion
    }
}
