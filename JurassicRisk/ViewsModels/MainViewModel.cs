using Models;
using Réseaux.Connexion;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Profil = Models.Profil;

namespace JurassicRisk.ViewsModels
{
    public class MainViewModel : observable.Observable
    {
        #region Attributs

        private ClientConnection client;

        private Profil _selectedProfil;

        private string _ip;

        private ObservableCollection<Statistique> _statistique;

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
        public ObservableCollection<Statistique> SelectedListStatistique
        {
            get { return _statistique; }
            set
            {
                _statistique = value;
                NotifyPropertyChanged("SelectedListStatistique");
            }
        }
        #endregion Properties

        #region Constructor

        private static MainViewModel _instance;

        /// <summary>
        /// Singleton Instance du MainViewModel
        /// </summary>
        /// <returns>MainViewModel instance</returns>
        /// <Author>Charif Mahmoud</Author>
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
            _ip = "localhost:7215";
            client = new ClientConnection();
            _statistique = new ObservableCollection<Statistique>();
         
            for (int i = 0; i < 5; i++)
            {
                Statistique statistique = new Statistique($"Adam{i}");
                _statistique.Add(statistique);
            }

            NotifyPropertyChanged("SelectedListStatistique");

        }

        #endregion Constructor

        #region Public methods

        /// <summary>
        /// Set Value of the selected profil
        /// </summary>
        /// <param name="pseudo">string pseudo</param>
        /// <returns>awaitable Task</returns>
        /// <Author>Charif Mahmoud</Author>
        public async Task SetSelectedProfil(string pseudo)
        {
            _selectedProfil = null;
            Profil response = await client.Get<Profil>($"https://{_ip}/Users/connexion?pseudo={pseudo}");

            if (response != null)
            {
                _selectedProfil = response;
            }
        }

        /// <summary>
        /// Create Profil
        /// </summary>
        /// <param name="profil">Profil</param>
        /// <returns>awaitable Task</returns>
        public async Task CreateProfil(Profil profil)
        {
            await client.Post<Profil>($"https://{_ip}/Users/Inscription?pseudo={profil.Pseudo}", profil);
        }

        /// <summary>
        /// Verify if profil exist in database
        /// </summary>
        /// <param name="pseudo">string pseudo</param>
        /// <returns>awaitable Task with Hresult bool</returns>
        public async Task<bool> VerifProfilCreation(string pseudo)
        {
            return await client.Get<bool>($"https://{_ip}/Users/verifUser?pseudo={pseudo}");
        }

        #endregion Private methods

    }
}
