using Microsoft.AspNetCore.SignalR;
using ModelsAPI.ClassMetier;
using ModelsAPI.ClassMetier.Player;
using Newtonsoft.Json;
using NReJSON;
using Redis.OM.Searching;
using Redis.OM;
using RISKAPI.Services;
using StackExchange.Redis;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.IO;
using System;

namespace RISKAPI.Hubs
{
    public class LobbyHub : Hub
    {
        private readonly RedisCollection<Lobby> _lobby;
        private readonly RedisConnectionProvider _provider;

        public LobbyHub(RedisConnectionProvider provider)
        {
            _provider = provider;
            _lobby = (RedisCollection<Lobby>)provider.RedisCollection<Lobby>();
            RedisProvider.Instance.ManageSubscriber(RefreshLobby);
        }



        public async Task RefreshLobby(string lobbyName)
        {
            string key = $"Lobby:{lobbyName}";

            if (RedisProvider.Instance.RedisDataBase.KeyExists(key))
            {
                RedisResult result = await RedisProvider.Instance.RedisDataBase.JsonGetAsync(key);
                Lobby? lobby = JsonConvert.DeserializeObject<Lobby>(result.ToString());
                if (lobby != null)
                {
                    string? lobbyJson = JsonConvert.SerializeObject(lobby);
                    await Clients.All.SendAsync("ReceiveLobby", lobbyJson);
                }
            }
        }

        public async Task SendLobby(string lobbyJson)
        {
            Lobby? lobby = JsonConvert.DeserializeObject<Lobby>(lobbyJson);
            Console.WriteLine($"Lobby try to send {lobby.Id}");
            await Clients.All.SendAsync("ReceiveLobby", lobbyJson);
        }

        public async Task SetTeam(Teams teams,string joueurName,string lobbyName)
        {
            string key = $"Lobby:{lobbyName}";
            string param = $"$.Joueurs[?(@Profil.Pseudo == {'"' +joueurName + '"'})]";
           
            if (RedisProvider.Instance.RedisDataBase.KeyExists(key))
            {

                RedisResult result = await RedisProvider.Instance.RedisDataBase.ExecuteAsync("JSON.GET", new object[] { key, param },CommandFlags.None);
                try
                {
                    List<Joueur?> joueur = JsonConvert.DeserializeObject<List<Joueur?>>(result.ToString());
                    if (joueur[0] != null)
                    {
                        Console.WriteLine($"{joueur[0].Profil.Pseudo} try to Set Team {joueur[0].Team} to {teams}");
                        joueur[0].Team = teams;

                        string? joueurJson = JsonConvert.SerializeObject(joueur[0]);
                        await RedisProvider.Instance.RedisDataBase.JsonSetAsync(key, joueurJson, $".Joueurs[?(@Profil.Pseudo == {'"' + joueurName + '"'})]");
                        await RefreshLobby(lobbyName);
                    }
                }
                catch (JsonSerializationException)
                {
                    throw;
                }           
            }    
        }

        public async Task JoinLobby(string joueurJson,string lobbyName)
        {
            Joueur? joueur = JsonConvert.DeserializeObject<Joueur>(joueurJson);
            Console.WriteLine($"{joueur.Profil.Pseudo} try to Join {lobbyName}");
            string key = $"Lobby:{lobbyName}";
            if (RedisProvider.Instance.RedisDataBase.KeyExists(key))
            {
                RedisResult result = await RedisProvider.Instance.RedisDataBase.JsonGetAsync(key);
                Lobby? lobby = JsonConvert.DeserializeObject<Lobby>(result.ToString());
                if (lobby != null)
                {
                    joueur.Profil.ConnectionId = Context.ConnectionId;
                    if (lobby.Joueurs.Count < 4)
                    {
                        lobby.JoinLobby(joueur);
                        await _lobby.UpdateAsync(lobby);

                        string? lobbyJson = JsonConvert.SerializeObject(lobby);
                        await Clients.All.SendAsync("JoinLobby", lobbyJson);
                        await Clients.Client(Context.ConnectionId).SendAsync("connected", Context.ConnectionId);
                    }
                    else
                    {
                        Console.WriteLine($"Plus de place dans le lobby pour que {joueur.Profil.Pseudo} rejoingne");
                    }
                   
                }
            }
        }

        public override Task OnConnectedAsync()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Connected {Context.ConnectionId} {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}" );
            Console.ForegroundColor = ConsoleColor.White;
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Disconnected : " + exception);
            await ExitLobby();
            Console.ForegroundColor = ConsoleColor.White;
            return base.OnDisconnectedAsync(exception);
        }  
        
        private async Task ExitLobby()
        {

        }
    }
}
