using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Réseaux.Connexion
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
        public async Task<T> Get<T>(string adresseDemande)
        {
            T obj = default;
            HttpResponseMessage ResponseMessage = await client.GetAsync(adresseDemande);

            if (ResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseBody = await ResponseMessage.Content.ReadAsStringAsync();
                obj = JsonConvert.DeserializeObject<T>(responseBody);
            }
            return obj;
        }

        /// <summary>
        /// POST un profil dans la base de donnée 
        /// </summary>
        /// <param name="adresseDemande">Post Request Path</param>
        /// <param name="p">Profil a envoyer</param>
        /// <returns>awaitable Task</returns>
        public async Task Post<T>(string adresseDemande,T p)
        {
            string jsonString = JsonConvert.SerializeObject(p);
            HttpResponseMessage response = await client.PostAsJsonAsync(adresseDemande, jsonString);
        }
    }
}
