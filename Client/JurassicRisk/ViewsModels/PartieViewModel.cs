using JurassicRisk.Ressource;
using JurassicRisk.Views;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using Models.Exceptions;
using Models.GameStatus;
using Models.Player;
using Models.Services;
using Models.Son;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

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
        private string _actualPlayer;

        private LobbyViewModel _lobbyVm;
        private JoueurViewModel _joueurVm;
        private CarteViewModel? _carteVm;
        private Partie? _partie;


        #endregion

        #region Property
        private Joueur? _joueur;
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

        public Joueur Joueur
        {
            get
            {
                return _joueur;
            }
            set
            {
                _joueur.Profil.ConnectionId = _connection.ConnectionId;
                _joueur = value;
                NotifyPropertyChanged();
            }
        }

        public List<Joueur> OtherPlayers
        {
            get
            {
                if (_partie != null)
                {
                    return _partie.Joueurs.Where(j => j != _joueur).ToList();
                }

                return new List<Joueur>();
            }
        }

        public string ActualPlayer { get => _actualPlayer; set => _actualPlayer = value; }

        #endregion

        #region Constructor
        public PartieViewModel(JurassicRiskViewModel vm)
        {
            _lobbyVm = vm.LobbyVm;
            _joueurVm = vm.JoueurVm;
            _carteLoaded = false;
            _progression = 0;
            //_actualPlayer = Partie.Joueurs[Partie.PlayerIndex].Profil.Pseudo;

            _connection = new HubConnectionBuilder().WithUrl($"wss://{JurasicRiskGameClient.Get.Ip}/JurrasicRisk/PartieHub").Build();
            _partieChatService = new SignalRPartieService(_connection);

            _partieChatService.YourTurn += _chatService_YourTurn;
            _partieChatService.EndTurn += _chatService_EndTurn;
            _partieChatService.Disconnected += _partieChatService_Disconnected;
            _partieChatService.Deploiment += _partieChatService_Deploiment;
            _partieChatService.PartieReceived += _chatService_PartieReceived;

            NotifyPropertyChanged("Partie");

        }

        #endregion

        #region Connection

        public async Task<bool> StartPartie(string lobbyName, string joueurName, string carteName)
        {
            await _partieChatService.StartPartie(lobbyName, joueurName, carteName);
            return true;
        }

        public async Task ConnectPartie()
        {
            if (_connection.State == HubConnectionState.Connected)
            {
                _isConnectedToPartie = true;
            }
            else
            {
                await _connection.StartAsync().ContinueWith(async task =>
                {
                    if (task.Exception != null)
                    {
                        _isConnectedToPartie = false;
                    }
                    else
                    {
                        _isConnectedToPartie = true;
                        await _partieChatService.ConnectedPartie(_lobbyVm.Lobby.Id, _joueurVm.Joueur.Profil.Pseudo);

                    }
                });
            }
        }

        /// <summary>
        /// DisposeConnection the connection
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

        /// <summary>
        /// Exit actual Lobby
        /// </summary>
        /// <returns></returns>
        public async Task<bool> StopConnection()
        {
            try
            {
                if (_connection.State == HubConnectionState.Connected)
                {
                    await _partieChatService.ExitPartie(ProfilViewModel.Get.SelectedProfil.Pseudo, _lobbyVm.Lobby.Id);
                    await _connection.StopAsync();
                    await _lobbyVm.ConnectLobby();
                }

                _isConnectedToPartie = false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error not disconnected From The Server\n" + e.Message);
            }

            return true;
        }

        #endregion

        #region Request


        public async Task SendEndTurn()
        {
            await _partieChatService.SendEndTurn(JurassicRiskViewModel.Get.LobbyVm.Lobby.Id, JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo);
        }
        #endregion

        #region Event

        private void _chatService_PartieReceived(string joueursJson, string partieName, string etatJson, int playerindex)
        {
            Application.Current.Dispatcher.Invoke(async () =>
            {
                _carteVm = new CarteViewModel(JurassicRiskViewModel.Get.JoueurVm, DrawEnd, Progression);

                List<Joueur> l = JsonConvert.DeserializeObject<List<Joueur>>(joueursJson);

                Deploiment etat = JsonConvert.DeserializeObject<Deploiment>(etatJson);

                _partie = new Partie(await _carteVm.InitCarte(), l, partieName, etat, playerindex);
                _partie.PlayerIndex = playerindex;
                _joueur = _partie.Joueurs.FirstOrDefault(j => j.Profil.Pseudo == JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo);

                JurassicRiskViewModel.Get.JoueurVm.Joueur = _joueur;
                _joueur.Profil.ConnectionId = _connection.ConnectionId;

                _isConnectedToPartie = true;

                await _carteVm.InitCarte();

                (Window.GetWindow(App.Current.MainWindow) as MainWindow)?.frame.NavigationService.Navigate(new JeuPage());

            }, DispatcherPriority.Render);
            NotifyPropertyChanged("Joueur");
            NotifyPropertyChanged("Partie");
        }

        private void _chatService_YourTurn(string etatJson, string name)
        {

            switch (name)
            {
                case "Deploiment":
                    {
                        Deploiment etat = JsonConvert.DeserializeObject<Deploiment>(etatJson);
                        _partie.Etat = etat;
                        break;
                    }
                case "Renforcement":
                    {
                        Renforcement etat = JsonConvert.DeserializeObject<Renforcement>(etatJson);
                        _partie.Etat = etat;
                        break;
                    }
            }

            NotifyPropertyChanged("Partie");
        }

        private void _partieChatService_Deploiment(int idUnit, int idTerritoire, int playerIndex)
        {
            if (_partie.Joueurs[playerIndex] != null && _partie.Joueurs[playerIndex].Units.Count > 0)
            {
                _partie.Joueurs[playerIndex].PlaceUnit(idUnit, _carteVm.Carte.GetTerritoire(idTerritoire));
                JurassicRiskViewModel.Get.JoueurVm.PlaceUnit(idUnit, _carteVm.Carte.GetTerritoire(idTerritoire));
                if (_joueurVm.Joueur.Units.Count <= 0)
                {
                    SoundStore.Get("errorsound.mp3").Play();
                    MessageBox.Show(new NotUniteException(Strings.ErrorNotUnit).Message, Strings.ErrorMessage);
                }

            }

            NotifyPropertyChanged("Joueur");
            NotifyPropertyChanged("OtherPlayers");
        }

        private async void _chatService_EndTurn()
        {
            await JurassicRiskViewModel.Get.PartieVm.SendEndTurn();
        }

        private void _partieChatService_Disconnected()
        {

            if (_joueurVm.Joueur != null)
            {
                _joueurVm.Joueur.Profil.ConnectionId = String.Empty;
                _partie.Joueurs.Find(j => j.Profil.Pseudo == _joueurVm.Joueur.Profil.Pseudo).Profil.ConnectionId = String.Empty;
            }
              (Window.GetWindow(App.Current.MainWindow) as MainWindow)?.frame.NavigationService.Navigate(new MenuPage());
            _isConnectedToPartie = false;
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
