using Models.Player;
using Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using Newtonsoft.Json;
using JurassicRisk.observable;
using System.Windows;
using StackExchange.Redis;
using System.Threading.Channels;
using JurassicRisk.Views;

namespace JurassicRisk.ViewsModels
{
    public class LobbyViewModel : Observable
    {
        #region Attributes
        private Lobby _lobby;
        #endregion

        #region Property
        public Lobby Lobby { get { return _lobby; } }
        #endregion

        public LobbyViewModel()
        {
            _lobby = new Lobby();
        
        }

        /// <summary>
        /// Refresh the lobby to the lobby of the server
        /// </summary>
        /// <param name="lobbyName">le nom de lobby</param>
        /// <returns>Task</returns>
        public async Task RefreshLobby(string lobbyName)
        {
            JurasicRiskGameClient.Get.Client.DefaultRequestHeaders.Accept.Clear();
            JurasicRiskGameClient.Get.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage reponse = await JurasicRiskGameClient.Get.Client.GetAsync($"https://{JurasicRiskGameClient.Get.Ip}/Lobby/{lobbyName}");
            if (reponse.IsSuccessStatusCode)
            {
                string lobbyJson = await reponse.Content.ReadAsStringAsync();
                _lobby = JsonConvert.DeserializeObject<Lobby>(lobbyJson);
                NotifyPropertyChanged("Lobby");
            }
        }

        public async Task SetTeam(Teams team)
        {
            JurasicRiskGameClient.Get.Client.DefaultRequestHeaders.Accept.Clear();
            JurasicRiskGameClient.Get.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage reponse = await JurasicRiskGameClient.Get.Client.PutAsJsonAsync($"https://{JurasicRiskGameClient.Get.Ip}/Lobby/PutTeam/{_lobby.Id}/{JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo}",team);
            if (reponse.IsSuccessStatusCode)
                {
                NotifyPropertyChanged("Lobby");
            }
        }

        /// <summary>
        /// Join un partie existante
        /// </summary>
        /// <param name="lobbyName">le nom de lobby</param>
        /// <returns>string</returns>
        public async Task<string> JoinLobby(string lobbyName)
        {
            string res = "Ok";
            try
            {

                JurasicRiskGameClient.Get.Client.DefaultRequestHeaders.Accept.Clear();
                JurasicRiskGameClient.Get.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage reponse = await JurasicRiskGameClient.Get.Client.GetAsync($"https://{JurasicRiskGameClient.Get.Ip}/Lobby/{lobbyName}");
                if (reponse.IsSuccessStatusCode)
                {
                    string lobbyJson = await reponse.Content.ReadAsStringAsync();
                    _lobby = JsonConvert.DeserializeObject<Lobby>(lobbyJson);
                    bool joined = _lobby.JoinLobby(JurassicRiskViewModel.Get.JoueurVm.Joueur);

                    if (joined)
                    {
                        NotifyPropertyChanged("Lobby");
                        RedisProvider.Instance.ManageSubscriber(RefreshLobby);
                    }
                    else
                    {
                        res = "Plus de place dans le lobby";
                    }

                    HttpResponseMessage reponsePost = await JurasicRiskGameClient.Get.Client.PostAsJsonAsync<Lobby>($"https://{JurasicRiskGameClient.Get.Ip}/Lobby/SetLobby",_lobby);
                    if (reponsePost.IsSuccessStatusCode)
                    {
                        res = "lobby rejoint et refresh";
                    }                  
                }
                else
                {
                    res = "Le lobby n'existe pas";
                }

            }
            catch (Exception e)
            {
                res = e.Message;
            }
            return res;
        }

        /// <summary>
        /// Exit the lobby
        /// </summary>
        /// <param name="lobbyName">le nom de lobby</param>
        /// <returns>string</returns>
        public async Task<string> ExitLobby(string lobbyName)
        {
            string res = "Ok";
            try
            {
                JurasicRiskGameClient.Get.Client.DefaultRequestHeaders.Accept.Clear();
                JurasicRiskGameClient.Get.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage reponse = await JurasicRiskGameClient.Get.Client.GetAsync($"https://{JurasicRiskGameClient.Get.Ip}/Lobby/{lobbyName}");
                if (reponse.IsSuccessStatusCode)
                {
                    string lobbyJson = await reponse.Content.ReadAsStringAsync();
                    _lobby = JsonConvert.DeserializeObject<Lobby>(lobbyJson);
                    bool exited = _lobby.ExitLobby(JurassicRiskViewModel.Get.JoueurVm.Joueur);

                    if (exited)
                    {
                        NotifyPropertyChanged("Lobby");
                    }
                    else
                    {
                        MessageBox.Show("Echec de la deconnexion");
                    }

                    HttpResponseMessage reponsePost = await JurasicRiskGameClient.Get.Client.PostAsJsonAsync<Lobby>($"https://{JurasicRiskGameClient.Get.Ip}/Lobby/SetLobby", _lobby);
                    if (reponsePost.IsSuccessStatusCode)
                    {
                        res = "I left the lobby";
                    }
                }
                else
                {
                    res = "Le lobby n'existe pas";
                }

            }
            catch (Exception e)
            {
                res = e.Message;
            }
            return res;
        }
    }
}
