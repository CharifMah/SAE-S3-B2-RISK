using GalaSoft.MvvmLight.Threading;
using JurassicRisk.observable;
using JurassicRisk.Services;
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
        readonly Dispatcher _dispatcher;
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
            _lobby = new Lobby();  
            _isConnected = false;
        }

        #endregion


        /// <summary>
        /// Join un partie existante
        /// </summary>
        /// <param name="lobbyName">le nom de lobby</param>
        /// <returns>string</returns>
        public async Task<bool> JoinLobby(string lobbyName)
        {
            HubConnection connection = new HubConnectionBuilder().WithUrl($"wss://localhost:7215/LobbyHub").Build();
            SignalRChatService chatService = new SignalRChatService(connection);
            chatService.LobbyReceived += ChatService_LobbyReceived;
            chatService.LobbyJoined += ChatService_LobbyJoined;
            await chatService.Connect().ContinueWith(async task =>
            {
                if (task.Exception != null)
                {
                    this._errorMessage = "Unable to connect to lobby chat hub";
                }
                Joueur joueur = new Joueur(JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil, Teams.NEUTRE);
                await chatService.JoinLobby(joueur,lobbyName);
                await chatService.SendLobby(new Lobby(lobbyName));
                _isConnected = true;
            });

            return _isConnected;
        }



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
        /// Exit the lobby
        /// </summary>
        /// <param name="lobbyName">le nom de lobby</param>
        /// <returns>string</returns>
        public async Task<string> ExitLobby(string lobbyName)
        {
            string res = "Ok";
            try
            {
                JurasicRiskGameClient.Get.Client.DefaultRequestHeaders.Accept.Clear();
                JurasicRiskGameClient.Get.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage reponse = await JurasicRiskGameClient.Get.Client.GetAsync($"https://{JurasicRiskGameClient.Get.Ip}/Lobby/{lobbyName}");
                if (reponse.IsSuccessStatusCode)
                {
                    string lobbyJson = await reponse.Content.ReadAsStringAsync();
                    _lobby = JsonConvert.DeserializeObject<Lobby>(lobbyJson);

                    bool exited = _lobby.ExitLobby(JurassicRiskViewModel.Get.JoueurVm.Joueur);

                    if (exited)
                    {
                        NotifyPropertyChanged("Lobby");
                    }

                    HttpResponseMessage reponsePost = await JurasicRiskGameClient.Get.Client.PutAsJsonAsync<Lobby>($"https://{JurasicRiskGameClient.Get.Ip}/Lobby/UpdateLobby", _lobby);
                    if (reponsePost.IsSuccessStatusCode)
                    {
                        res = "I left the lobby";
                    }

                }
                else
                {
                    res = "Le lobby n'existe pas";
                }

            }
            catch (Exception e)
            {
                res = e.Message;
            }
            return res;
        }


        #region Events

        private void ChatService_LobbyReceived(Lobby lobby)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                this._lobby = lobby;
                NotifyPropertyChanged("Lobby");
            });
        }

        private void ChatService_LobbyJoined(Lobby lobby)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                this._lobby = lobby;
                NotifyPropertyChanged("Lobby");
            });
        }

        #endregion
    }
}
