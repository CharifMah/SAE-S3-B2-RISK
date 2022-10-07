using JurassicRisk.Utilities;
using Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace JurassicRisk.ViewModels
{
    public class MainViewModel : observable.Observable
    {
        #region Attributs

        private ClientConnection client;

        private Profil _selectedProfil;

        private ObservableCollection<Profil> _Profils;

        #endregion

        #region Properties

        public Profil SelectedProfil
        {
            get { return _selectedProfil; }
            set
            {
                _selectedProfil = value;
                NotifyPropertyChanged("SelectedProfil");
            }
        }

        public ObservableCollection<Profil> Profils
        {
            get { return _Profils; }

            set
            {
                _Profils = value;
                NotifyPropertyChanged("Profils");
            }
        }

        public ICommand GetRequestProfilCommand { get; }

        #endregion Properties

        #region Constructor

        private static MainViewModel _instance;
        public static MainViewModel Get()
        {
            if (_instance == null)
            {
                _instance = new MainViewModel();
            }
            return _instance;
        }

        private MainViewModel()
        {
            _Profils = new ObservableCollection<Profil>();
            client = new ClientConnection();


            GetRequestProfilCommand = new RelayCommand(o => InitializeProfil(o.ToString()));
        }

        #endregion Constructor

        #region Private methods

        private async Task InitializeProfil(string pseudo)
        {

            Profil response = await client.GetProfile($"https://localhost:7215/Users/connexion?login={pseudo}");

            if (response != null)
            {
                _Profils.Add(new Profil(response.Pseudo));

                _selectedProfil = _Profils[0];
            }
            else
            {
                MessageBox.Show("Ce Profil n'existe pas \n");
            }
        }

        #endregion Private methods

    }
}
