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

        /// <summary>
        /// Crée un connexion HTTPClient
        /// </summary>
        public ClientConnection()
        {
            client = new HttpClient();
        }

        /// <summary>
        /// Fait un requete GET qui renvoie un json Deserialiser en Profil
        /// </summary>
        /// <param name="adresseDemande"></param>
        /// <returns>awaitable Task avec comme result le Profil demander</returns>
        /// <Author>Charif Mahmoud, Brian VERCHERE</Author>
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

        /// <summary>
        /// POST un profil dans la base de donnée 
        /// </summary>
        /// <param name="adresseDemande">Post Request Path</param>
        /// <param name="p">Profil a envoyer</param>
        /// <returns>awaitable Task</returns>
        public async Task PostProfile(string adresseDemande, Profil p)
        {
            string jsonString = JsonConvert.SerializeObject(p);
            HttpResponseMessage response = await HttpClientExtensions.PostAsJsonAsync<string>(client, adresseDemande, jsonString);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("Votre profil a bien été créer !");
            }
        }

        /// <summary>
        /// GET un bool qui renvoie vrai si pseudo demander existe
        /// </summary>
        /// <param name="adresseDemande">Get Request Path for pseudo verification</param>
        /// <returns>awaitable Task avec comme result un bool</returns>
        public async Task<bool> GetVerifUser(string adresseDemande)
        {

            bool obj = false;
            HttpResponseMessage ResponseMessage = await client.GetAsync(adresseDemande);

            if (ResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                try
                {
                    string responseBody = await ResponseMessage.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<bool>(responseBody);
                }
                catch (Exception)
                {
                   obj = false;
                }
                
            }
            return obj;
        }

    }
}
