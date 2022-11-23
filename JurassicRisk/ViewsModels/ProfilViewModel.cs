using Models;
using Réseaux.Connexion;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Profil = Models.Profil;

namespace JurassicRisk.ViewsModels
{
    public class ProfilViewModel : observable.Observable
    {
        #region Attributs

        private ClientConnection client;

        private Profil _selectedProfil;

        private string _ip;

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
       
        #endregion Properties

        #region Constructor
        private static ProfilViewModel _instance;
        public static ProfilViewModel Instance
        {
            get { return _instance ?? (_instance = new ProfilViewModel()); }
        }
        private ProfilViewModel()
        {
            _ip = "localhost:7215";
            client = new ClientConnection();           
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
