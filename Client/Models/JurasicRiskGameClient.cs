using Microsoft.AspNetCore.SignalR.Client;
using Models.GameStatus;
using Models.Services;
using System.Net.Http;

namespace Models
{
    public class JurasicRiskGameClient
    {
        #region Attributes
        private Lobby? _lobby;
        private HttpClient _client;
        private string _ip;
        private HubConnection _connection;
        private bool _isConnected;

        private SignalRLobbyService _chatService;
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

        public SignalRLobbyService ChatService { get => _chatService; set => _chatService = value; }

        public HubConnection Connection { get => _connection; set => _connection = value; }

        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
            set
            {
                _isConnected = value;
            }
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
            _connection = new HubConnectionBuilder().WithUrl($"wss://localhost:7215/JurrasicRisk").Build();

            _chatService = new SignalRLobbyService(Connection);
            _isConnected = false;
            _lobby = null;
        }

        private void StartPartie()
        {

        }

        public void StopPartie() { }

        public async Task Connect()
        {
            await _connection.StartAsync().ContinueWith(async task =>
            {
                if (task.Exception != null)
                {
                    //this._errorMessage = "Unable to connect to Lobby chat hub";
                }
                _isConnected = true;
            });


        }


        /// <summary>
        /// Disconnect the connection
        /// </summary>
        /// <returns></returns>
        public async Task Disconnect()
        {
            if (_connection != null)
            {
                await _connection.DisposeAsync();
            }
            _isConnected = false;
        }





        #endregion

    }
}
