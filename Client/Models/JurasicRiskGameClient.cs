using Microsoft.AspNetCore.SignalR.Client;
using Models.GameStatus;
using Models.Services;
using System.Diagnostics;
using System.Net.Http;
using System.Windows;

namespace Models
{
    public class JurasicRiskGameClient
    {
        #region Attributes

        private Lobby? _lobby;
        private Partie? _partie;
        private HttpClient _client;
        private string _ip;
        private HubConnection _connectionLobby;
        private HubConnection _connectionPartie;

        private bool _isConnectedToLobby;
        private bool _isConnectedToPartie;
        private SignalRPartieService _partieChatService;

        private SignalRLobbyService _lobbyChatService;
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

        public SignalRLobbyService? LobbyChatService { get => _lobbyChatService; set => _lobbyChatService = value; }

        public SignalRPartieService? PartieChatService { get => _partieChatService; set => _partieChatService = value; }

        public HubConnection? Connection { get => _connectionLobby; set => _connectionLobby = value; }

        public bool IsConnectedToLobby { get => _isConnectedToLobby; set => _isConnectedToLobby = value; }
        public bool IsConnectedToPartie { get => _isConnectedToPartie; set => _isConnectedToPartie = value; }

        public Partie? Partie { get => _partie; set => _partie = value; }
        public HubConnection ConnectionPartie { get => _connectionPartie; set => _connectionPartie = value; }

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

            _connectionLobby = new HubConnectionBuilder().WithUrl($"wss://localhost:7215/JurrasicRisk/LobbyHub").Build();
            _lobbyChatService = new SignalRLobbyService(_connectionLobby);

            _connectionPartie = new HubConnectionBuilder().WithUrl($"wss://localhost:7215/JurrasicRisk/PartieHub").Build();
            _partieChatService = new SignalRPartieService(_connectionPartie);

            _lobby = null;
        }

        #endregion

        public async Task ConnectLobby()
        {

            await _connectionLobby.StartAsync().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    Trace.Write("Connect Lobby : " + task.Exception.Message);
                    _isConnectedToLobby = false;

                }
                _isConnectedToLobby = true;
            });
        }

        public async Task ConnectPartie()
        {
            await _connectionPartie.StartAsync().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    Trace.Write("Connect Partie : " + task.Exception.Message);
                    _isConnectedToPartie = false;
                }
                _isConnectedToPartie = true;
            });
        }

        /// <summary>
        /// Disconnect the connection
        /// </summary>
        /// <returns></returns>
        public async Task Disconnect()
        {
            if (_connectionLobby != null)
            {
                await _connectionLobby.DisposeAsync();
            }
            _isConnectedToPartie = false;
        }


        public void DestroyClient()
        {
            _instance = null;
        }

    }
}
