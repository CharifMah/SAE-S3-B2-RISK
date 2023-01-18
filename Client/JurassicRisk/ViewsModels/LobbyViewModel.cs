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

namespace JurassicRisk.ViewsModels
{
    public class LobbyViewModel : Observable
    {
        #region Attributes
        private bool _isConnectedToLobby;

        private HubConnection _connection;
        private SignalRLobbyService _chatService;
        private Lobby? _lobby;
        private object application;
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
            _chatService.NotOwner += _chatService_NotOwner;
            _chatService.LobbyReceived += _chatService_LobbyReceived;
            _chatService.LobbyJoined += _chatService_LobbyJoined;
        }

        #endregion

        /// <summary>
        /// Connecte le lobby a la connection
        /// </summary>
        /// <returns></returns>
        public async Task ConnectLobby()
        {
            if (_connection.State == HubConnectionState.Connected)
            {
                _isConnectedToLobby = true;
            }
            else
            {
                await _connection.StartAsync().ContinueWith(task =>
                {
                    if (task.Exception != null)
                    {
                        _isConnectedToLobby = false;
                    }
                    else
                    {
                        _isConnectedToLobby = true;

                    }
                });
            }
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
            string profilJson = JsonConvert.SerializeObject(ProfilViewModel.Get.SelectedProfil);
            if (!String.IsNullOrEmpty(profilJson))
            {
                if ((_connection != null || _connection.ConnectionId == null))
                {
                    await ConnectLobby();
                }

                await _chatService.JoinLobby(profilJson, lobbyName, password);
            }
            else
            {
                MessageBox.Show("profil is null");
            }


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
                await _chatService.ExitLobby(ProfilViewModel.Get.SelectedProfil.Pseudo, _lobby.Id);
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
            if (_lobby != null)
            {

                Joueur j = JurassicRiskViewModel.Get.JoueurVm.Joueur;
                await _chatService.IsReady(j.IsReady, j.Profil.Pseudo, JurassicRiskViewModel.Get.LobbyVm.Lobby.Id);
            }

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

        public async Task StartGameOwnerOnly()
        {
            await _chatService.StartGameOtherPlayer(_lobby.Id);
        }

        #endregion

        #region events

        private void _chatService_Connected(string connectionid, string isconnected)
        {
            if (JurassicRiskViewModel.Get.JoueurVm.Joueur != null && connectionid != string.Empty)
                JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.ConnectionId = connectionid;

            if (isconnected != string.Empty)
            {
                _isConnectedToLobby = bool.Parse(isconnected);
                if (_isConnectedToLobby)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new LobbyPage());

                    });
                }

            }
        }

        private void _chatService_NotOwner()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageBox.Show(Ressource.Strings.NotOwner);
            });
        }

        private void _chatService_Disconnected()
        {
            if (JurassicRiskViewModel.Get.JoueurVm.Joueur != null)
                JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.ConnectionId = string.Empty;
        }

        private void _chatService_LobbyReceived(string lobbyjson)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Lobby? lobby = JsonConvert.DeserializeObject<Lobby>(lobbyjson);
                this._lobby = lobby;
                NotifyPropertyChanged("lobby");
            });
        }

        private void _chatService_LobbyJoined(string lobbyjson)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (lobbyjson != "false")
                {
                    Lobby? lobby = JsonConvert.DeserializeObject<Lobby>(lobbyjson);
                    this._lobby = lobby;
                    JurassicRiskViewModel.Get.LobbyVm.IsConnectedToLobby = true;
                }
                else
                {
                    JurassicRiskViewModel.Get.LobbyVm.IsConnectedToLobby = false;
                }
                NotifyPropertyChanged("lobby");
            });
        }


        #endregion
    }
}
