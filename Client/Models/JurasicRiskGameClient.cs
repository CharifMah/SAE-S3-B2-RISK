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
            _ip = "25.50.92.160:5555";
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            _client = new HttpClient(httpClientHandler) { BaseAddress = new Uri("https://25.50.92.160:5555") };
            _connection = new HubConnectionBuilder().WithUrl($"wss://25.50.92.160:5555/JurrasicRisk",(opts) =>
            {
                opts.HttpMessageHandlerFactory = (message) =>
                {
                    if (message is HttpClientHandler clientHandler)
                        // always verify the SSL certificate
                        clientHandler.ServerCertificateCustomValidationCallback +=
                            (sender, certificate, chain, sslPolicyErrors) => { return true; };
                    return message;
                };
            }).Build();

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
