using JurassicRisk.Views;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using Models.GameStatus;
using Models.Services;
using Models.Tours;
using System;
using System.Data.Common;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace JurassicRisk.ViewsModels
{
    public class JurassicRiskViewModel : observable.Observable
    {
        #region Attributes
        private HubConnection _connection;
        private Partie? _partie;
        private bool _isConnected;
        private bool _carteLoaded;
        private double _progression;
        private CarteViewModel? _carteVm;
        private double _zoom;
        private static Point? lastPoint;
        private JoueurViewModel _joueurVm;
        private LobbyViewModel _lobbyVm;
        private TaskCompletionSource<bool> _clickWaitTask;
        private SignalRLobbyService _lobbyChatService;
        private SignalRPartieService _partieChatService;

        #endregion

        #region Property

        public CarteViewModel? CarteVm { get => _carteVm; }
        public bool CarteLoaded { get => _carteLoaded; set => _carteLoaded = value; }
        public double Progress { get => _progression; set => _progression = value; }
        public JoueurViewModel JoueurVm { get => _joueurVm; }
        public LobbyViewModel LobbyVm { get => _lobbyVm; set => _lobbyVm = value; }
        public double Zoom
        {
            get => _zoom;

            set
            {
                if (0 < value && value < 5)
                    _zoom = value;
                NotifyPropertyChanged();
            }
        }
        public Partie? Partie { get => _partie; set => _partie = value; }

        #endregion

        #region Singleton
        private static JurassicRiskViewModel _instance;
        public static JurassicRiskViewModel Get
        {
            get
            {
                if (_instance == null)
                    _instance = new JurassicRiskViewModel();
                return _instance;
            }
        }

        public bool IsConnected { get => _isConnected; set => _isConnected = value; }

        private JurassicRiskViewModel()
        {
            _joueurVm = new JoueurViewModel();
            _lobbyVm = new LobbyViewModel();
            _carteLoaded = false;
            _progression = 0;
            _carteVm = null;
            _zoom = 1.0;
            lastPoint = new Point(0, 0);
            _lobbyChatService = JurasicRiskGameClient.Get.LobbyChatService;
            _partieChatService = JurasicRiskGameClient.Get.PartieChatService;
            _partieChatService.YourTurn += _chatService_YourTurn;
            _partieChatService.EndTurn += _chatService_EndTurn;
            _partieChatService.Connected += _partieChatService_Connected; ;
            _partieChatService.Disconnected += _partieChatService_Disconnected; ;

        }


        #endregion

        #region Request

        public async Task SendEndTurn()
        {
            await _partieChatService.SendEndTurn(this._lobbyVm.Lobby.Id, JoueurVm.Joueur.Profil.Pseudo);
        }

        /// <summary>
        /// Exit actual Lobby
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ExitPartie()
        {
            await _partieChatService.ExitPartie(_joueurVm.Joueur.Profil.Pseudo);
            return true;
        }

        #endregion

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

        #region Events

        public void _chatService_YourTurn(string turnType)
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

        public void _chatService_EndTurn()
        {
            //new tourAttente
        }

        private async void _partieChatService_Connected(string connectionId)
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
            await _partieChatService.ConnectedPartie(this._lobbyVm.Lobby.Id, this._joueurVm.Joueur.Profil.Pseudo);
        }

      

        private void _partieChatService_Disconnected()
        {
            if (JurassicRiskViewModel.Get.JoueurVm.Joueur != null)
                JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.ConnectionId = String.Empty;

            _isConnected = false;
        }

        #endregion

        public async void StartJeuPage()
        {
            if (_connection == null || _connection.ConnectionId == null)
            {
                await JurasicRiskGameClient.Get.ConnectPartie();
            }

            _carteVm = new CarteViewModel(JurassicRiskViewModel.Get.JoueurVm, DrawEnd, Progression);
            Lobby l = JurassicRiskViewModel.Get.LobbyVm.Lobby;
            _partie = new Partie(_carteVm.Carte, l.Joueurs, l.Id);
            (Window.GetWindow(App.Current.MainWindow) as MainWindow)?.frame.NavigationService.Navigate(new JeuPage());
        }

        public void DestroyVm()
        {
            _instance = null;
        }
    }
}
