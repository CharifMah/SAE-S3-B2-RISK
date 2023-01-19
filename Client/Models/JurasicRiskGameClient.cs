using Microsoft.AspNetCore.SignalR.Client;
using Models.Services;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

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
            var cert = new X509Certificate2("RiskApiCert.pfx", "/Riskapi123");

            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (request, certificate, chain, errors) => {
                    if (errors != SslPolicyErrors.None) return false;

                    // Here is your code...

                    return true;
                }, 
                
            };
            handler.ClientCertificates.Add(cert);


            _ip = "localhost:7215";
            _client = new HttpClient(handler);

        }

        #endregion
    
        public void DestroyClient()
        {
            _instance = null;
        }
    }
}
