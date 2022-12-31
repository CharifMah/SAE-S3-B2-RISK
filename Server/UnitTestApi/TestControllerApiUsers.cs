using ModelsAPI.ClassMetier;
using ModelsAPI.ClassMetier.Player;
using RISKAPI.Controllers;
using System.Net.Http.Json;

namespace UnitTestApi
{
    public class TestControllerApiUsers
    {
        [Fact]
        public async void connexionTest()
        {
            string _ip = "localhost:7215";
            HttpClient client = new HttpClient();

            //var controller = new UsersController();
            Profil profil = new Profil("", "");

            HttpResponseMessage reponse = await client.PostAsJsonAsync($"https://{_ip}/Users/Connexion", profil);

            string res = reponse.Content.ReadAsStringAsync().Result;

            Assert.Equal("this account do not exist try to register or use an ather pseudo", res);


            profil = new Profil("romain", "12345");

            await client.PostAsJsonAsync($"https://{_ip}/Users/Connexion", profil);


            reponse = await client.PostAsJsonAsync($"https://{_ip}/Users/Connexion", profil);


            Assert.True(reponse.IsSuccessStatusCode);

            profil.Password = "azerty";

            reponse = await client.PostAsJsonAsync($"https://{_ip}/Users/Connexion", profil);

            res = reponse.Content.ReadAsStringAsync().Result;

            Assert.Equal("wrong password", res);

        }

        [Fact]
        public async void inscriptionTest()
        {
            string _ip = "localhost:7215";
            HttpClient client = new HttpClient();

            //var controller = new RISKAPI.Controllers.UsersController();
            Profil profil = new Profil("", "");

            HttpResponseMessage reponse = await client.PostAsJsonAsync($"https://{_ip}/Users/Inscription", profil);

            string res = reponse.Content.ReadAsStringAsync().Result;

            Assert.Equal("password too short", res);


            profil = new Profil("romain", "12345");

            await client.PostAsJsonAsync($"https://{_ip}/Users/Inscription", profil);

            reponse = await client.PostAsJsonAsync($"https://{_ip}/Users/Inscription", profil);


            Assert.True(!reponse.IsSuccessStatusCode);

            res = reponse.Content.ReadAsStringAsync().Result;

            Assert.Equal("pseudo already used", res);
        }
    }
}
