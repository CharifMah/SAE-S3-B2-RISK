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
            _partieVm = new PartieViewModel(this);
            _zoom = 1.0;
        }

        #endregion

      
        public void DestroyVm()
        {
            _instance = null;
        }
    }
}
