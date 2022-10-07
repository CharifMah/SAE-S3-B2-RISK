using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<object> Get(string adresseDemande)
        {
            object obj = null;
            HttpResponseMessage ResponseMessage = await client.GetAsync(adresseDemande);

            if(ResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseBody = await ResponseMessage.Content.ReadAsStringAsync();
                obj = JsonConvert.DeserializeObject<object>(responseBody);
            }
            return obj;
        }

        //methode post
        public void Post(string adresseDemande, object o)
        {

        }
    }
}
