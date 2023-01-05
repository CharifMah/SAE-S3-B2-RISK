using Models.GameStatus;
using System.Net.Http;
using System.Net.WebSockets;

namespace Models
{
    public class JurasicRiskGameClient
    {
        #region Attributes
        private Lobby? _lobby;
        private HttpClient _client;
        private ClientWebSocket _clientWebSocket;
        private string _ip;

        #endregion

        #region Property

        public Lobby? Lobby
        {
            get { return _lobby; }
            set { _lobby = value; }
        }

        public HttpClient Client
        {
            get { return _client; }
        }

        public string Ip
        {
            get { return _ip; }
        }

        #endregion

        #region Singleton

        private static JurasicRiskGameClient? _instance;
        public static JurasicRiskGameClient Get
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new JurasicRiskGameClient();
                }
                return _instance;
            }
        }

        private JurasicRiskGameClient()
        {
            _ip = "localhost:7215";
            _client = new HttpClient();
            _lobby = null;
        }

        #endregion

    }
}
