using GalaSoft.MvvmLight.Threading;
using JurassicRisk.observable;
using JurassicRisk.Ressource;
using JurassicRisk.Views;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using Models.GameStatus;
using Models.Player;
using Models.Services;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace JurassicRisk.ViewsModels
{
    public class LobbyViewModel : Observable
    {
        #region Attributes
        private HubConnection _connection;
        private bool _isConnectedToLobby;
        private SignalRLobbyService _chatService;
        readonly Dispatcher _dispatcher;
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



        public bool IsConnectedToLobby { get => _isConnectedToLobby; set => _isConnectedToLobby = value; }

        #endregion

        #region Constructor

        public LobbyViewModel()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            _lobby = JurasicRiskGameClient.Get.Lobby;

            _isConnectedToLobby = false;
            _connection = JurasicRiskGameClient.Get.Connection;
            _chatService = JurasicRiskGameClient.Get.ChatService;

            _chatService.Connected += _chatService_Connected;
            _chatService.Disconnected += _chatService_Disconnected;
            _chatService.PartieReceived += _chatService_PartieReceived; ;
        }




        #endregion

        #region Requests

        /// <summary>
        /// Starts a connection to the server
        /// </summary>
        /// <returns>Task</returns>
        public async Task Connect()
        {
            await JurasicRiskGameClient.Get.Connect();

            _chatService.ConnectedToLobby += _chatService_ConnectedToLobby;
            _chatService.LobbyReceived += _chatService_LobbyReceived;
            _chatService.LobbyJoined += _chatService_LobbyJoined;
        }


        /// <summary>
        /// Create a Lobby
        /// </summary>
        /// <param name="lobby">Lobby to create</param>
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
                    res = Strings.ErrorLobbyExist;
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
        /// <param name="lobbyName">le nom du Lobby</param>
        /// <returns>bool</returns>
        public async Task<bool> JoinLobby(string lobbyName, string password)
        {
            if (_connection == null || _connection.ConnectionId == null)
            {
                await Connect();
            }

            Joueur joueur = new Joueur(ProfilViewModel.Get.SelectedProfil, Teams.NEUTRE);
            await _chatService.JoinLobby(joueur, lobbyName, password);

            return true;
        }

        /// <summary>
        /// Exit actual Lobby
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

            Joueur j = JurassicRiskViewModel.Get.JoueurVm.Joueur;
            await _chatService.IsReady(j.IsReady, j.Profil.Pseudo, JurassicRiskViewModel.Get.LobbyVm.Lobby.Id);
            return true;
        }

        /// <summary>
        /// Set the team of the current player 
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        public async Task<bool> SetTeam(Teams team)
        {
            await _chatService.SetTeam(team, JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo, JurassicRiskViewModel.Get.LobbyVm.Lobby.Id);
            return true;
        }

        public async Task<bool> StartPartie(string lobbyName, string joueurName, string carteName)
        {
            await _chatService.StartPartie(lobbyName,joueurName, carteName);
            return true;
        }

        #endregion

        #region Events

        private void _chatService_PartieReceived()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new JeuPage());
            });
        }

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
