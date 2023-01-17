using Microsoft.AspNetCore.SignalR.Client;
using Models.Services;
using System.Net.Http;

namespace Models
{
    public class JurasicRiskGameClient
    {
        #region Attributes
        private HttpClient _client;
        private string _ip;
        #endregion

        #region Property

        public HttpClient Client
        {
            get { return _client; }
        }

        public string Ip
        {
            get { return _ip; }
        }

        #endregion

        #region Singleton

        private static JurasicRiskGameClient? _instance;
        public static JurasicRiskGameClient Get
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new JurasicRiskGameClient();
                }
                return _instance;
            }
        }

        private JurasicRiskGameClient()
        {
            _ip = "localhost:7215";
            _client = new HttpClient();
        }

        #endregion
    
        public void DestroyClient()
        {
            _instance = null;
        }
    }
}
