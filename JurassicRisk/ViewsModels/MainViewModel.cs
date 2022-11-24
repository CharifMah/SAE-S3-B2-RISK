using System.Collections.Generic;
using System.Collections.ObjectModel;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Réseaux.Connexion;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Ubiety.Dns.Core;
using Profil = Models.Profil;

namespace JurassicRisk.ViewsModels
{
    public class MainViewModel : observable.Observable
    {
        #region Attributs

        private HttpClient client;

        private Profil _selectedProfil;

        private string _ip;

        private ObservableCollection<Profil> _profils;

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
        public ObservableCollection<Profil> SelectedListProfil
        {
            get { return _profils; }
            set
            {
                _profils = value;
                NotifyPropertyChanged("SelectedListProfil");
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
            client = new HttpClient();
            _profils = new ObservableCollection<Profil>();
            NotifyPropertyChanged("SelectedListProfil");


        }

        #endregion Constructor

        #region Public methods

        /// <summary>
        /// Set Value of the selected profil
        /// </summary>
        /// <param name="pseudo">string pseudo</param>
        /// <returns>awaitable Task</returns>
        /// <Author>Charif Mahmoud</Author>
        public async Task SetSelectedProfil(Profil profil)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
            _selectedProfil = null;
            HttpResponseMessage response = await client.PostAsJsonAsync<Profil>($"https://{_ip}/Users/connexion", profil);
            if (response.IsSuccessStatusCode)
           {
                var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
                Profil profilDemande = JsonSerializer.Deserialize<Profil>(response.Content.ReadAsStringAsync().Result,options);
                string t = response.Content.ReadAsStringAsync().Result;
                _selectedProfil = profilDemande;
            }
            else
            {
                string error = response.Content.ReadAsStringAsync().Result;
            }
        }

        /// <summary>
        /// Create Profil
        /// </summary>
        /// <param name="profil">Profil</param>
        /// <returns>awaitable Task</returns>
        public async Task CreateProfil(Profil profil)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _selectedProfil = null;

            HttpResponseMessage reponse = await client.PostAsJsonAsync<Profil>($"https://{_ip}/Users/Inscription", profil);
        }

        /// <summary>
        /// Verify if profil exist in database
        /// </summary>
        /// <param name="pseudo">string pseudo</param>
        /// <returns>awaitable Task with Hresult bool</returns>
        public async Task<bool> VerifProfilCreation(string pseudo)
        {
            bool res = false;
            HttpResponseMessage reponseMessage =  await client.GetAsync($"https://{_ip}/Users/verifUser?pseudo={pseudo}");
            if (reponseMessage.IsSuccessStatusCode)
            {
                res = await reponseMessage.Content.ReadAsAsync<bool>();
            }
            return res;
        }

        #endregion Private methods

    }
}
