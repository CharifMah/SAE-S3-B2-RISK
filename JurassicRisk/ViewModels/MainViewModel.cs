using JurassicRisk.Utilities;
using Models;
using System.Collections.ObjectModel;
using System.Net.Http;
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

        public ICommand DisplayProfilCommand { get; }

        #endregion Properties

        #region Constructor

        public MainViewModel()
        {
            _Profils = new ObservableCollection<Profil>();
            client = new ClientConnection();


            DisplayProfilCommand = new RelayCommand(o => DisplayProfil());
        }

        #endregion Constructor

        #region Private methods

        private void InitializeProfil()
        {
            _Profils.Add(client.Get("https://localhost:7215/Users/connexion?login=brian").Result as Profil);

            _selectedProfil = _Profils[0];
        }

        private void DisplayProfil()
        {
            InitializeProfil();
        }

        #endregion Private methods

    }
}
