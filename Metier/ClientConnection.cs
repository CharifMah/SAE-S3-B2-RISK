using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Models
{
    public class ClientConnection
    {
        private HttpClient client;

        public ClientConnection()
        {
            client = new HttpClient();
        }

        public HttpClient Client
        {
            get { return client; }
        }

        //méthode get => reponse
        public async Task<Profil> GetProfile(string adresseDemande)
        {
    
            Profil obj = null;
            HttpResponseMessage ResponseMessage = await client.GetAsync(adresseDemande);

            if(ResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseBody = await ResponseMessage.Content.ReadAsStringAsync();
                obj = JsonConvert.DeserializeObject<Profil>(responseBody);
            }
            return obj;
        }

        //methode post
        public async Task PostProfile(string adresseDemande, Profil p)
        {
            string jsonString = JsonConvert.SerializeObject(p);
            HttpResponseMessage response = await HttpClientExtensions.PostAsJsonAsync<string>(client, adresseDemande, jsonString);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("Votre profil a bien été créer !");
            }
            else
            {
                MessageBox.Show("Essayez de changer de pseudo !");
            }
        }

        public async Task<bool> GetVerifUser(string adresseDemande)
        {

            bool obj = false;
            HttpResponseMessage ResponseMessage = await client.GetAsync(adresseDemande);

            if (ResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseBody = await ResponseMessage.Content.ReadAsStringAsync();
                obj = JsonConvert.DeserializeObject<bool>(responseBody);
            }
            return obj;
        }

    }
}
