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
            client = new ClientConnection();
        }

        #endregion Constructor

        #region Private methods

        public async Task InitializeProfil(string pseudo)
        {
            _selectedProfil = null;

            Profil response = await client.GetProfile($"https://localhost:7215/Users/connexion?login={pseudo}");
        
            if (response != null)
            {
                _selectedProfil = response;
            }
            else
            {
                MessageBox.Show("Ce Profil n'existe pas \n");
            }
        }

        #endregion Private methods

    }
}
