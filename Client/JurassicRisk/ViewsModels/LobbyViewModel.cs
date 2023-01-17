using JurassicRisk.observable;
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

namespace JurassicRisk.ViewsModels
{
    public class LobbyViewModel : Observable
    {
        #region Attributes
        private bool _isConnectedToLobby;

        private HubConnection _connection;
        private SignalRLobbyService _chatService;
        private Lobby? _lobby;
        #endregion

        #region Property
        public Lobby? Lobby
        {
            get { return _lobby; }
            set
            {
                _lobby = value;
                NotifyPropertyChanged("Lobby");
            }
        }
        public bool IsConnectedToLobby { get => _isConnectedToLobby; set => _isConnectedToLobby = value; }
        public HubConnection Connection { get => _connection; set => _connection = value; }
        public SignalRLobbyService ChatService { get => _chatService; set => _chatService = value; }
        #endregion

        #region Constructor
        public LobbyViewModel()
        {
            _lobby = null;
            _connection = new HubConnectionBuilder().WithUrl($"wss://localhost:7215/JurrasicRisk/LobbyHub").WithAutomaticReconnect().Build();
            _chatService = new SignalRLobbyService(_connection);

            _isConnectedToLobby = false;

            _chatService.Connected += _chatService_Connected;
            _chatService.Disconnected += _chatService_Disconnected;

            _chatService.LobbyReceived += _chatService_LobbyReceived;
            _chatService.LobbyJoined += _chatService_LobbyJoined;
        }
        #endregion

        public async Task ConnectLobby()
        {
            await _connection.StartAsync().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    _isConnectedToLobby = false;
                    if (_connection.State != HubConnectionState.Connected)
                    {
                        //Reset lobby and connection
                        JurassicRiskViewModel.Get.LobbyVm = new LobbyViewModel();
                    }
                }
                else
                {
                    _isConnectedToLobby = true;

                }
            });
        }

        /// <summary>
        /// DisposeConnection the connection
        /// </summary>
        /// <returns></returns>
        public async Task DisposeConnection()
        {
            if (_connection != null)
            {
                await _connection.DisposeAsync();
            }
            _isConnectedToLobby = false;
        }


        #region Requests

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
                    res = "Un Lobby avec le meme nom existe déja";
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
            if (_connection != null || _connection.ConnectionId == null)
            {
                await ConnectLobby();
            }

            string profilJson = JsonConvert.SerializeObject(ProfilViewModel.Get.SelectedProfil);
            await _chatService.JoinLobby(profilJson, lobbyName, password);

            return true;
        }

        /// <summary>
        /// Exit actual Lobby
        /// </summary>
        /// <returns></returns>
        public async Task<bool> StopConnection()
        {
            try
            {
                await _connection.StopAsync();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error not disconnected From The Server\n" + e.Message);
            }

            return true;
        }

        /// <summary>
        /// Check if player is ready
        /// </summary>
        /// <returns></returns>
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
            if (_lobby != null)
            {
                await _chatService.SetTeam(team, JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo, _lobby.Id);
            }

            return true;
        }



        #endregion

        #region Events

        private void _chatService_Connected(string connectionId,string isConnected)
        {
            if (JurassicRiskViewModel.Get.JoueurVm.Joueur != null && connectionId != String.Empty)
                JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.ConnectionId = connectionId;

            if (isConnected != String.Empty)
            {
                _isConnectedToLobby = bool.Parse(isConnected);
                if (_isConnectedToLobby)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new LobbyPage());

                    });
                }

            }
        }

        private void _chatService_Disconnected()
        {
            if (JurassicRiskViewModel.Get.JoueurVm.Joueur != null)
                JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.ConnectionId = String.Empty;
        }

        private void _chatService_LobbyReceived(string lobbyJson)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Lobby? lobby = JsonConvert.DeserializeObject<Lobby>(lobbyJson);
                this._lobby = lobby;
                NotifyPropertyChanged("Lobby");
            });
        }

        private void _chatService_LobbyJoined(string lobbyJson)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (lobbyJson != "false")
                {
                    Lobby? lobby = JsonConvert.DeserializeObject<Lobby>(lobbyJson);
                    this._lobby = lobby;
                    JurassicRiskViewModel.Get.LobbyVm.IsConnectedToLobby = true;
                }
                else
                {
                    JurassicRiskViewModel.Get.LobbyVm.IsConnectedToLobby = false;
                }
                NotifyPropertyChanged("Lobby");
            });
        }


        #endregion
    }
}
