using Models.Player;
using Models;
using ModelsAPI.ClassMetier;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using Newtonsoft.Json;
using JurassicRisk.observable;

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
    }
}
