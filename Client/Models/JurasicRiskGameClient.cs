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

            ConnectHubLobby();
            ConnectHubPartie();
        }


        private void ConnectHubLobby()
        {
            _connectionLobby = new HubConnectionBuilder().WithUrl($"wss://localhost:7215/JurrasicRisk/LobbyHub").Build();
            _lobbyChatService = new SignalRLobbyService(_connectionLobby);
        }

        private void ConnectHubPartie() 
        {
            _connectionPartie = new HubConnectionBuilder().WithUrl($"wss://localhost:7215/JurrasicRisk/PartieHub").Build();
            _partieChatService = new SignalRPartieService(_connectionPartie);
        }
        #endregion

        public async Task ConnectLobby()
        {

            await _connectionLobby.StartAsync().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    MessageBox.Show("Connect Lobby : " + task.Exception.Message);
                    _isConnectedToLobby = false;
                    ConnectHubLobby();
                    return;

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
                    ConnectHubPartie();
                    MessageBox.Show("Connect Partie : " + task.Exception.Message);
                    _isConnectedToPartie = false;
                }
                _isConnectedToPartie = true;
            });
        }

        /// <summary>
        /// DisconnectLobby the connection
        /// </summary>
        /// <returns></returns>
        public async Task DisconnectLobby()
        {
            if (_connectionLobby != null)
            {
                await _connectionLobby.DisposeAsync();
            }
            _isConnectedToLobby = false;
        }

        /// <summary>
        /// DisconnectLobby the connection
        /// </summary>
        /// <returns></returns>
        public async Task DisconnectPartie()
        {
            if (_connectionPartie != null)
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
