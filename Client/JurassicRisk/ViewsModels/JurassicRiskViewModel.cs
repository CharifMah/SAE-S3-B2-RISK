using Models;
using Models.GameStatus;
using Models.Services;
using Models.Tours;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;
using JurassicRisk.Views;
using Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using static JurassicRisk.ViewsModels.CarteViewModel;

namespace JurassicRisk.ViewsModels
{
    public class JurassicRiskViewModel : observable.Observable
    {
        #region Attributes
        private bool _carteLoaded;
        private double _progression;
        private CarteViewModel? _carteVm;
        private double _zoom;
        private static Point? lastPoint;
        private JoueurViewModel _joueurVm;
        private LobbyViewModel _lobbyVm;
        private TaskCompletionSource<bool> _clickWaitTask;
        private SignalRLobbyService _chatService;

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

        private JurassicRiskViewModel()
        {
            _joueurVm = new JoueurViewModel();
            _lobbyVm = new LobbyViewModel();

            _chatService = JurasicRiskGameClient.Get.ChatService;
            _chatService.YourTurn += _chatService_YourTurn;
            _chatService.EndTurn += _chatService_EndTurn;
        }
        #endregion

        public void _chatService_YourTurn(string turnType)
        {
            switch (turnType)
            {
                case "placement":
                    {
                        CarteVm.Tour = new TourPlacement();
                        break;
                    }
            }
        }

        public void _chatService_EndTurn()
        {
            //new tourAttente
        }

        public async Task SendEndTurn()
        {
            {
                _chatService.SendEndTurn(JurasicRiskGameClient.Get.Lobby.Id, JoueurVm.Joueur.Profil.Pseudo);
            }
            _carteLoaded = false;
            _progression = 0;
            _carteVm = null;
            _zoom = 1.0;
            lastPoint = new Point(0,0);
        }
        #endregion


        private void DrawEnd()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _carteLoaded = true;
                NotifyPropertyChanged("CarteLoaded");
            }, DispatcherPriority.Render);
        }

        private void Progression(double taux)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _progression = taux;
                NotifyPropertyChanged("Progress");
            }, DispatcherPriority.Render);
        }

        public void StartJeuPage()
        {
            _carteVm = new CarteViewModel(JurassicRiskViewModel.Get.JoueurVm, DrawEnd, Progression);
            (Window.GetWindow(App.Current.MainWindow) as MainWindow)?.frame.NavigationService.Navigate(new JeuPage());
        }

        public void DestroyVm()
        {
            _instance = null;
        }
    }
}
