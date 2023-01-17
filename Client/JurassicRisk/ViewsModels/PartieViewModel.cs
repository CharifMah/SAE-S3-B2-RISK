using JurassicRisk.Views;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using Models.GameStatus;
using Models.Player;
using Models.Services;
using Models.Tours;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Threading;
using static JurassicRisk.ViewsModels.CarteViewModel;

namespace JurassicRisk.ViewsModels
{
    public class PartieViewModel : observable.Observable
    {
        #region Attributes
        private HubConnection _connection;
        private SignalRPartieService _partieChatService;

        private bool _isConnectedToPartie;
        private bool _carteLoaded;
        private double _progression;

        private LobbyViewModel _lobbyVm;
        private JoueurViewModel _joueurVm;
        private CarteViewModel? _carteVm;
        private Partie? _partie;

        #endregion

        #region Property

        public double Progress { get => _progression; set => _progression = value; }
        public bool CarteLoaded { get => _carteLoaded; set => _carteLoaded = value; }
        public bool IsConnectedToPartie { get => _isConnectedToPartie; set => _isConnectedToPartie = value; }

        public HubConnection Connection { get => _connection; set => _connection = value; }
        public SignalRPartieService ChatService { get => _partieChatService; set => _partieChatService = value; }

        public CarteViewModel? CarteVm { get => _carteVm; }
        public Partie? Partie
        {
            get => _partie;

            set
            {
                _partie = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Constructor
        public PartieViewModel(JurassicRiskViewModel vm)
        {
            _lobbyVm = vm.LobbyVm;
            _joueurVm = vm.JoueurVm;
            _carteLoaded = false;
            _progression = 0;

            _connection = new HubConnectionBuilder().WithUrl($"wss://localhost:7215/JurrasicRisk/PartieHub").WithAutomaticReconnect().Build();
            _partieChatService = new SignalRPartieService(_connection);

            _partieChatService.YourTurn += _chatService_YourTurn;
            _partieChatService.EndTurn += _chatService_EndTurn;
            _partieChatService.Connected += async (connectionId) => await _partieChatService_Connected(connectionId);
            _partieChatService.Disconnected += _partieChatService_Disconnected;
            _partieChatService.Deploiment += _partieChatService_Deploiment;
            _partieChatService.PartieReceived += _chatService_PartieReceived;

            NotifyPropertyChanged("Partie");

        }


        #endregion

        public async Task<bool> StartPartie(string lobbyName, string joueurName, string carteName)
        {
            await _partieChatService.StartPartie(lobbyName, joueurName, carteName);
            return true;
        }

        public async Task ConnectPartie()
        {
            await _connection.StartAsync().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    _isConnectedToPartie = false;
                    if (_connection.State == HubConnectionState.Disconnected)
                    {
                        _connection.StartAsync();
                    }
                    else
                    {
                        _isConnectedToPartie = false;
                    }

                }
                else
                {
                    _isConnectedToPartie = true;
                }
            });
        }

        /// <summary>
        /// DisconnectLobby the connection
        /// </summary>
        /// <returns></returns>
        public async Task DisconnectPartie()
        {
            if (_connection != null)
            {
                await _connection.DisposeAsync();
            }
            _isConnectedToPartie = false;
        }

        #region Request
        public async Task SendEndTurn()
        {

            await _partieChatService.SendEndTurn(JurassicRiskViewModel.Get.LobbyVm.Lobby.Id, JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo);
        }

        /// <summary>
        /// Exit actual Lobby
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ExitPartie()
        {
            await _partieChatService.ExitPartie(_joueurVm.Joueur.Profil.Pseudo, _partie.Id);
            return true;
        }
        #endregion

        #region Event
        private async Task _partieChatService_Connected(string connectionId)
        {
            if (_joueurVm.Joueur != null && connectionId != String.Empty)
            {
                _joueurVm.Joueur.Profil.ConnectionId = connectionId;
                _isConnectedToPartie = true;
                await _partieChatService.ConnectedPartie(_lobbyVm.Lobby.Id, _joueurVm.Joueur.Profil.Pseudo);
            }
            else
            {
                _isConnectedToPartie = false;
            }
            NotifyPropertyChanged("Partie");
        }

        private void _chatService_YourTurn(string turnType)
        {
            switch (turnType)
            {
                case "Deploiment":
                    {
                        _carteVm.Tour = new TourPlacement();
                        break;
                    }
            }
        }

        private void _chatService_EndTurn()
        {
            //new tourAttente
        }

        private void _partieChatService_Disconnected()
        {

            if (_joueurVm.Joueur != null)
            {
                _joueurVm.Joueur.Profil.ConnectionId = String.Empty;
                _partie.Joueurs.Find(j => j.Profil.Pseudo == _joueurVm.Joueur.Profil.Pseudo).Profil.ConnectionId = String.Empty;
            }

            _isConnectedToPartie = false;
        }

        private void _partieChatService_Deploiment(int idUnit, int idTerritoire, int playerIndex)
        {

            if (_partie.Joueurs[playerIndex].Profil.Pseudo != JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo)
            {
                _partie.Joueurs[playerIndex].PlaceUnits(_partie.Joueurs[playerIndex].Units[idUnit], _carteVm.Carte.GetTerritoire(idTerritoire));
            }
            else
            {
                _joueurVm.Joueur.PlaceUnits(_joueurVm.Joueur.Units[0], _carteVm.Carte.GetTerritoire(idTerritoire));
            }
        }


        private void _chatService_PartieReceived(string joueursJson,string partieName)
        {
            Application.Current.Dispatcher.Invoke(async () =>
            {
                _carteVm = new CarteViewModel(JurassicRiskViewModel.Get.JoueurVm, DrawEnd, Progression);

                if (_isConnectedToPartie && JurassicRiskViewModel.Get.LobbyVm.IsConnectedToLobby)
                {
                    await JurassicRiskViewModel.Get.LobbyVm.DisconnectLobby();
                }

                if (_connection == null || _connection.ConnectionId == null)
                {
                    await ConnectPartie();
                }
                List<Joueur> l = JsonConvert.DeserializeObject<List<Joueur>>(joueursJson);
                _partie = new Partie(await _carteVm.InitCarte(),l, partieName);
                await _carteVm.InitCarte();

                (Window.GetWindow(App.Current.MainWindow) as MainWindow)?.frame.NavigationService.Navigate(new JeuPage());

            }, DispatcherPriority.Render);

        }

        #region Delegate

        private void Progression(double taux)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _progression = taux;
                NotifyPropertyChanged("Progress");
            }, DispatcherPriority.Render);
        }

        private void DrawEnd()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _carteLoaded = true;
                NotifyPropertyChanged("CarteLoaded");
            }, DispatcherPriority.Render);
        }

        #endregion<

        #endregion
    }
}
