using JurassicRisk.Views;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using Models.GameStatus;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace JurassicRisk.ViewsModels
{
    public class JurassicRiskViewModel : observable.Observable
    {
        #region Attributes
        private HubConnection _connection;
        private bool _carteLoaded;
        private double _progression;
        private CarteViewModel? _carteVm;
        private double _zoom;
        private JoueurViewModel _joueurVm;
        private LobbyViewModel _lobbyVm;
        private PartieViewModel _partieVm;

        #endregion

        #region Property

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
        public bool CarteLoaded { get => _carteLoaded; set => _carteLoaded = value; }
        public double Progress { get => _progression; set => _progression = value; }

        public CarteViewModel? CarteVm { get => _carteVm; }
        public JoueurViewModel JoueurVm { get => _joueurVm; }
        public LobbyViewModel LobbyVm { get => _lobbyVm; set => _lobbyVm = value; }
        public PartieViewModel PartieVm { get => _partieVm; set => _partieVm = value; }
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
            _carteLoaded = false;
            _progression = 0;
            _carteVm = null;
            _zoom = 1.0;
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

        public async Task StartJeuPage()
        {
            if (_connection == null || _connection.ConnectionId == null)
            {
                await JurasicRiskGameClient.Get.ConnectPartie();
            }

            _carteVm = new CarteViewModel(JurassicRiskViewModel.Get.JoueurVm, DrawEnd, Progression);
            Lobby l = JurassicRiskViewModel.Get.LobbyVm.Lobby;

            _partieVm = new PartieViewModel(_carteVm.Carte, l.Joueurs, l.Id);

            (Window.GetWindow(App.Current.MainWindow) as MainWindow)?.frame.NavigationService.Navigate(new JeuPage());
        }

        public void DestroyVm()
        {
            _instance = null;
        }
    }
}
