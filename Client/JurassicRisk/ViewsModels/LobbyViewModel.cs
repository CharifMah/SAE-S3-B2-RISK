using GalaSoft.MvvmLight.Threading;
using JurassicRisk.observable;
using JurassicRisk.Services;
using JurassicRisk.Views;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using Models.Player;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace JurassicRisk.ViewsModels
{
    public class LobbyViewModel : Observable
    {
        #region Attributes
        private HubConnection _connection;
        readonly Dispatcher _dispatcher;
        private SignalRLobbyService _chatService;
        private Lobby _lobby;
        private bool _isConnected;
        private string _errorMessage = string.Empty;
        #endregion

        #region Property

        public Lobby Lobby { 
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

        #endregion

        #region Constructor

        public LobbyViewModel()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            _connection = new HubConnectionBuilder().WithUrl($"wss://localhost:7215/LobbyHub").Build();
            _chatService = new SignalRLobbyService(_connection);
            _lobby = new Lobby();  
            _isConnected = false;
        }

        #endregion

        #region Requests

        /// <summary>
        /// Connect chatService Client to LobbyHub
        /// </summary>
        /// <returns>bool true if connected</returns>
        private async Task Connect()
        {
            _chatService.Connected += _chatService_Connected;
            await _chatService.Connect().ContinueWith(async task =>
            {
                if (task.Exception != null)
                {
                    this._errorMessage = "Unable to connect to lobby chat hub";
                }
            });
        }

        /// <summary>
        /// Join un partie existante
        /// </summary>
        /// <param name="lobbyName">le nom de lobby</param>
        /// <returns>bool</returns>
        public async Task<bool> JoinLobby(string lobbyName)
        {
            await Connect();

            _chatService.LobbyReceived += ChatService_LobbyReceived;
            _chatService.LobbyJoined += ChatService_LobbyJoined;

            Joueur joueur = new Joueur(JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil, Teams.NEUTRE);
            await _chatService.JoinLobby(joueur, lobbyName);

            return true;
        }

        public async Task<bool> SetTeam(Teams team)
        {
            await _chatService.SetTeam(team);
            return true;
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

        #endregion

        #region Events

        private void _chatService_Connected(string connectionId)
        {
            _isConnected = true;
        }

        private void ChatService_LobbyReceived(string lobbyJson)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                Lobby? lobby = JsonConvert.DeserializeObject<Lobby>(lobbyJson);
                this._lobby = lobby;
                NotifyPropertyChanged("Lobby");
            });
        }

        private void ChatService_LobbyJoined(string lobbyJson)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                Lobby? lobby = JsonConvert.DeserializeObject<Lobby>(lobbyJson);
                this._lobby = lobby;
                NotifyPropertyChanged("Lobby");
            });
        }

        #endregion
    }
}
