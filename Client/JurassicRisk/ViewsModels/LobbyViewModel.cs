using GalaSoft.MvvmLight.Threading;
using JurassicRisk.observable;
using JurassicRisk.Services;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using Models.Player;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace JurassicRisk.ViewsModels
{
    public class LobbyViewModel : Observable
    {
        #region Attributes
        private bool _isConnectedToLobby;
        private HubConnection _connection;
        readonly Dispatcher _dispatcher;
        private SignalRLobbyService _chatService;
        private Lobby _lobby;
        private bool _isConnected;
        private string _errorMessage = string.Empty;
        #endregion

        #region Property

        public Lobby Lobby
        {
            get { return _lobby; }
            set
            {
                _lobby = value;
                NotifyPropertyChanged("Lobby");
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                NotifyPropertyChanged(nameof(ErrorMessage));
                NotifyPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
            set
            {
                _isConnected = value;
                NotifyPropertyChanged(nameof(IsConnected));
            }
        }

        public bool IsConnectedToLobby { get => _isConnectedToLobby; set => _isConnectedToLobby = value; }

        #endregion

        #region Constructor

        public LobbyViewModel()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            _lobby = new Lobby();
            _isConnected = false;
            _isConnectedToLobby = false;
        }

        #endregion

        #region Requests

        /// <summary>
        /// Starts a connection to the server
        /// </summary>
        /// <returns>Task</returns>
        private async Task Connect()
        {
            _connection = new HubConnectionBuilder().WithUrl($"wss://localhost:7215/LobbyHub").Build();
            _chatService = new SignalRLobbyService(_connection);

            _chatService.ConnectedToLobby += _chatService_ConnectedToLobby;
            _chatService.LobbyReceived += _chatService_LobbyReceived;
            _chatService.LobbyJoined += _chatService_LobbyJoined;
            _chatService.Connected += _chatService_Connected;
            _chatService.Disconnected += _chatService_Disconnected;

            await _connection.StartAsync().ContinueWith(async task =>
            {
                if (task.Exception != null)
                {
                    this._errorMessage = "Unable to connect to lobby chat hub";
                }
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

        /// <summary>
        /// Create a lobby
        /// </summary>
        /// <param name="lobby">lobby to create</param>
        /// <returns>response</returns>
        public async Task<string> CreateLobby(Lobby lobby)
        {
            string res = "Ok";
            try
            {
                JurasicRiskGameClient.Get.Client.DefaultRequestHeaders.Accept.Clear();
                JurasicRiskGameClient.Get.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage reponsePost = await JurasicRiskGameClient.Get.Client.PostAsJsonAsync<Lobby>($"https://{JurasicRiskGameClient.Get.Ip}/Lobby/CreateLobby", lobby);
                if (reponsePost.IsSuccessStatusCode)
                {
                    res = $"Lobby Created with name {lobby.Id}";
                }
                else
                {
                    res = "Un lobby avec le meme nom existe déja";
                }

            }
            catch (Exception e)
            {
                res = e.Message;
            }
            return res;
        }

        /// <summary>
        /// Join une partie existante
        /// </summary>
        /// <param name="lobbyName">le nom du lobby</param>
        /// <returns>bool</returns>
        public async Task<bool> JoinLobby(string lobbyName)
        {
            if (_connection == null || _connection.ConnectionId == null)
            {
                await Connect();
            }

            Joueur joueur = new Joueur(ProfilViewModel.Get.SelectedProfil, Teams.NEUTRE);
            await _chatService.JoinLobby(joueur, lobbyName);

            return true;
        }

        /// <summary>
        /// Exit actual lobby
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ExitLobby()
        {
            await _chatService.ExitLobby();
            JurassicRiskViewModel.Get.LobbyVm = new LobbyViewModel();
            return true;
        }

        public async Task<bool> IsReady()
        {
            await _chatService.IsReady(JurassicRiskViewModel.Get.JoueurVm.Joueur.IsReady);
            return true;
        }

        /// <summary>
        /// Set the team of the current player 
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        public async Task<bool> SetTeam(Teams team)
        {
            await _chatService.SetTeam(team);
            return true;
        }

        #endregion

        #region Events

        private void _chatService_Connected(string connectionId)
        {
            if (JurassicRiskViewModel.Get.JoueurVm.Joueur != null && connectionId != String.Empty)
            {
                JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.ConnectionId = connectionId;
                _isConnected = true;
            }
            else
            {
                _isConnected = false;
            }
        }

        private void _chatService_Disconnected()
        {
            if (JurassicRiskViewModel.Get.JoueurVm.Joueur != null)
                JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.ConnectionId = String.Empty;

            _isConnected = false;
        }

        private void _chatService_ConnectedToLobby(string connected)
        {
            if (connected != String.Empty)
            {
                _isConnectedToLobby = bool.Parse(connected);

            }
        }

        private void _chatService_LobbyReceived(string lobbyJson)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                Lobby? lobby = JsonConvert.DeserializeObject<Lobby>(lobbyJson);
                this._lobby = lobby;
                NotifyPropertyChanged("Lobby");
            });
        }

        private void _chatService_LobbyJoined(string lobbyJson)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                if (lobbyJson != "false")
                {
                    Lobby? lobby = JsonConvert.DeserializeObject<Lobby>(lobbyJson);
                    this._lobby = lobby;
                    _isConnected = true;
                }
                else
                {
                    _isConnected = false;
                }
                NotifyPropertyChanged("Lobby");
            });
        }

        #endregion
    }
}
