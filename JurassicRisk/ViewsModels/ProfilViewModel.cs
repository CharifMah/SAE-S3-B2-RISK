using Models;
using Models.Player;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace JurassicRisk.ViewsModels
{
    public class ProfilViewModel : observable.Observable
    {
        #region Attributs

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

        #endregion Properties

        #region Constructor
        private static ProfilViewModel _instance;
        public static ProfilViewModel Instance
        {
            get { return _instance ?? (_instance = new ProfilViewModel()); }
        }
        private ProfilViewModel()
        {

        }

        #endregion Constructor

        #region Public methods

        /// <summary>
        /// Set Value of the selected profil
        /// </summary>
        /// <param name="pseudo">string pseudo</param>
        /// <returns>awaitable Task</returns>
        /// <Author>Charif Mahmoud</Author>
        public async Task<string> SetSelectedProfil(Profil profil)
        {
            string res = "Ok";
            try
            {
                JurasicRiskGame.Get.Client.DefaultRequestHeaders.Accept.Clear();
                JurasicRiskGame.Get.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _selectedProfil = null;
                HttpResponseMessage reponse = await JurasicRiskGame.Get.Client.PostAsJsonAsync<Profil>($"https://{JurasicRiskGame.Get.Ip}/Users/connexion", profil);
                if (reponse.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    Profil profilDemande = JsonSerializer.Deserialize<Profil>(reponse.Content.ReadAsStringAsync().Result, options);
                    string t = reponse.Content.ReadAsStringAsync().Result;
                    _selectedProfil = profilDemande;
                    NotifyPropertyChanged("SelectedProfil");
                }
                else
                {
                    res = reponse.Content.ReadAsStringAsync().Result;
                }

            }
            catch (Exception e)
            {
                res = e.Message;
            }
            return res;
        }

        /// <summary>
        /// Create Profil
        /// </summary>
        /// <param name="profil">Profil</param>
        /// <returns>awaitable Task</returns>
        public async Task<string> CreateProfil(Profil profil)
        {
            string res = "Ok";
            try
            {
                JurasicRiskGame.Get.Client.DefaultRequestHeaders.Accept.Clear();
                JurasicRiskGame.Get.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _selectedProfil = null;

                HttpResponseMessage reponse = await JurasicRiskGame.Get.Client.PostAsJsonAsync<Profil>($"https://{JurasicRiskGame.Get.Ip}/Users/Inscription", profil);
                if (!reponse.IsSuccessStatusCode)
                {
                    res = reponse.Content.ReadAsStringAsync().Result;
                }

            }
            catch(Exception e)
            {
                res = e.Message;
            }
            return res;
        }
        /// <summary>
        /// Verify if profil exist in database
        /// </summary>
        /// <param name="pseudo">string pseudo</param>
        /// <returns>awaitable Task with Hresult bool</returns>
        public async Task<bool> VerifProfilCreation(string pseudo)
        {
            bool res = false;
            HttpResponseMessage reponseMessage = await JurasicRiskGame.Get.Client.GetAsync($"https://{JurasicRiskGame.Get.Ip}/Users/verifUser?pseudo={pseudo}");
            if (reponseMessage.IsSuccessStatusCode)
            {
                res = await reponseMessage.Content.ReadAsAsync<bool>();
            }
            return res;
        }
        #endregion Private methods

    }
}
