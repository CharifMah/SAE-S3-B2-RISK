using Microsoft.AspNetCore.SignalR;
using ModelsAPI.ClassMetier;
using ModelsAPI.ClassMetier.Player;
using Newtonsoft.Json;
using NReJSON;
using Redis.OM;
using Redis.OM.Searching;
using RISKAPI.Services;
using StackExchange.Redis;

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
            RedisProvider.Instance.ManageSubscriber(RefreshLobbyToClients);

        }

        public async Task RefreshLobbyToClients(string lobbyName)
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

        public async Task JoinLobby(string joueurJson, string lobbyName)
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
                        await Clients.Client(Context.ConnectionId).SendAsync("connected", Context.ConnectionId);
                        Context.Items.Add(Context.ConnectionId, new object[] { lobby, joueur });

                        await RefreshLobbyToClients(lobbyName);
                    }
                    else
                    {
                        Console.WriteLine($"Plus de place dans le lobby pour que {joueur.Profil.Pseudo} rejoingne");
                    }
                }
            }
        }

        public async Task SetTeam(Teams teams, string joueurName, string lobbyName)
        {
            string key = $"Lobby:{lobbyName}";
            string param = $"$.Joueurs[?(@Profil.Pseudo == {'"' + joueurName + '"'})]";

            if (RedisProvider.Instance.RedisDataBase.KeyExists(key))
            {
                RedisResult result = await RedisProvider.Instance.RedisDataBase.ExecuteAsync("JSON.GET", new object[] { key, param }, CommandFlags.None);
                try
                {
                    List<Joueur?> joueur = JsonConvert.DeserializeObject<List<Joueur?>>(result.ToString());
                    if (joueur != null && joueur.Count > 0)
                    {
                        Console.WriteLine($"{joueur[0].Profil.Pseudo} try to Set Team {joueur[0].Team} to {teams}");
                        joueur[0].Team = teams;
                        string? joueurJson = JsonConvert.SerializeObject(joueur[0]);
                        await RedisProvider.Instance.RedisDataBase.JsonSetAsync(key, joueurJson, $".Joueurs[?(@Profil.Pseudo == {'"' + joueurName + '"'})]");
                        await RefreshLobbyToClients(lobbyName);
                    }
                }
                catch (JsonSerializationException)
                {
                    throw;
                }
            }
        }

        public async Task IsReady(bool ready, string joueurName, string lobbyName)
        {
            string key = $"Lobby:{lobbyName}";
            string param = $"$.Joueurs[?(@Profil.Pseudo == {'"' + joueurName + '"'})]";

            if (RedisProvider.Instance.RedisDataBase.KeyExists(key))
            {
                RedisResult result = await RedisProvider.Instance.RedisDataBase.ExecuteAsync("JSON.GET", new object[] { key, param }, CommandFlags.None);
                try
                {
                    List<Joueur?> joueur = JsonConvert.DeserializeObject<List<Joueur?>>(result.ToString());
                    if (joueur != null && joueur.Count > 0)
                    {
                        Console.WriteLine($"{joueur[0].Profil.Pseudo} try to Set Ready to {ready}");
                        joueur[0].IsReady = ready;
                        string? joueurJson = JsonConvert.SerializeObject(joueur[0]);
                        await RedisProvider.Instance.RedisDataBase.JsonSetAsync(key, joueurJson, $".Joueurs[?(@Profil.Pseudo == {'"' + joueurName + '"'})]");
                        await RefreshLobbyToClients(lobbyName);
                    }
                }
                catch (JsonSerializationException)
                {
                    throw;
                }
            }
        }

        public override Task OnConnectedAsync()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Connected {Context.ConnectionId} {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}");
            Console.ForegroundColor = ConsoleColor.White;
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await ExitLobby();

            await base.OnDisconnectedAsync(exception);
        }

        public async Task ExitLobby()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Disconnected {Context.ConnectionId} {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}");
            Console.ForegroundColor = ConsoleColor.White;

            object[]? l = (Context.Items.First(x => x.Key == Context.ConnectionId).Value as object[]);
            try
            {
                (l[0] as Lobby).ExitLobby((l[1] as Joueur));
                await SendLobby(JsonConvert.SerializeObject((l[0] as Lobby)));
                await _lobby.UpdateAsync((l[0] as Lobby));
                Context.Items.Remove(Context.ConnectionId);
                Console.WriteLine($"the player {(l[1] as Joueur).Profil.Pseudo} as succeffuluy leave the lobby {(l[0] as Lobby).Id}");
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed to leave the lobby '{(l[0] as Lobby).Id}'");
            }
        }
    }
}
