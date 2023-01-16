﻿using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using ModelsAPI.ClassMetier;
using ModelsAPI.ClassMetier.GameStatus;
using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Player;
using Newtonsoft.Json;
using NReJSON;
using Redis.OM;
using Redis.OM.Searching;
using RISKAPI.Services;
using StackExchange.Redis;

namespace RISKAPI.Hubs
{
    [HubName("LobbyHub")]
    public class LobbyHub : Hub
    {
        #region Injection
        private readonly RedisCollection<Lobby> _lobby;
        private readonly RedisConnectionProvider _provider;
        #endregion

        #region Constructor
        public LobbyHub(RedisConnectionProvider provider)
        {
            _provider = provider;
            _lobby = (RedisCollection<Lobby>)provider.RedisCollection<Lobby>();
            RedisProvider.Instance.ManageSubscriber(RefreshLobbyToClients);
        }
        #endregion

        public async Task RefreshLobbyToClients(string lobbyName)
        {
            Lobby? lobby = await GetLobby(lobbyName);

            if (lobby != null)
            {
                string? lobbyJson = JsonConvert.SerializeObject(lobby);
                foreach (Joueur j in lobby.Joueurs)
                {
                    await Clients.Client(j.Profil.ConnectionId).SendAsync("ReceiveLobby", lobbyJson);
                }
            }
        }

        /// <summary>
        /// Send Lobbys to Clients
        /// </summary>
        /// <param name="lobbyJson"></param>
        /// <returns></returns>
        public async Task SendLobby(string lobbyJson)
        {
            Lobby? lobby = JsonConvert.DeserializeObject<Lobby>(lobbyJson);
            Console.WriteLine($"Lobbys try to send {lobby.Id}");
            foreach (Joueur j in lobby.Joueurs)
            {
                await Clients.Client(j.Profil.ConnectionId).SendAsync("ReceiveLobby", lobbyJson);
            }
        }

        public async Task JoinLobby(string joueurJson, string lobbyName, string password)
        {
            Joueur? joueur = JsonConvert.DeserializeObject<Joueur>(joueurJson);
            Console.WriteLine($"{joueur.Profil.Pseudo} try to Join {lobbyName}");

            string key = $"Lobbys:{lobbyName}";

            if (RedisProvider.Instance.RedisDataBase.KeyExists(key))
            {
                RedisResult result = await RedisProvider.Instance.RedisDataBase.JsonGetAsync(key);
                Lobby? lobby = JsonConvert.DeserializeObject<Lobby>(result.ToString());
                if (lobby != null)
                {
                    PasswordHasher<Lobby> passwordHasher = new PasswordHasher<Lobby>();
                    if (passwordHasher.VerifyHashedPassword(lobby, lobby.Password, password) != 0)
                    {
                        joueur.Profil.ConnectionId = Context.ConnectionId;
                        if (lobby.Joueurs.Count < 4)
                        {
                            lobby.JoinLobby(joueur);
                            await _lobby.UpdateAsync(lobby);
                            await Clients.Client(Context.ConnectionId).SendAsync("connectedToLobby", "true");
                            List<Lobby> lobbyList = JurasicRiskGameServer.Get.Lobbys;
                            if (lobbyList.Find(l => l.Id == lobby.Id) == null)
                            {
                                lobbyList.Add(lobby);
                            }
                            else
                            {
                                Lobby l = lobbyList.Find(l => l.Id == lobby.Id);
                                l = lobby;
                            }
                            await RefreshLobbyToClients(lobbyName);
                        }
                        else
                        {
                            await Clients.Client(Context.ConnectionId).SendAsync("connectedToLobby", "false");
                            Console.WriteLine($"Plus de place dans le lobby pour que {joueur.Profil.Pseudo} rejoingne");
                        }
                    }
                    else
                    {
                        await Clients.Client(Context.ConnectionId).SendAsync("connectedToLobby", "false");
                        Console.WriteLine("mauvais mot de passe");
                    }
                }
            }
            else
            {
                await Clients.Client(Context.ConnectionId).SendAsync("connectedToLobby", "false");
                Console.WriteLine("Le lobby n'existe pas");
            }
        }

        public async Task SetTeam(Teams teams, string joueurName, string lobbyName)
        {
            string key = $"Lobbys:{lobbyName}";
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
            string key = $"Lobbys:{lobbyName}";
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

        public async Task ExitLobby(string joueurName,string lobbyName)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Disconnected {Context.ConnectionId} {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}");
            Console.ForegroundColor = ConsoleColor.White;
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, lobbyName);
            try
            {
                Lobby lobby = null;
                Joueur joueur = null;
                foreach (Lobby p in JurasicRiskGameServer.Get.Lobbys)
                {
                    if (p.Id == lobbyName)
                    {
                        lobby = p;
                        break;
                    }
                }
                if (lobby != null)
                {
                    Joueur j = lobby.Joueurs.Find(j => j.Profil.Pseudo == joueurName);
                    if (j != null)
                    {
                        lobby.Joueurs.Remove(j);
                        await _lobby.UpdateAsync(lobby);
                        await RefreshLobbyToClients(lobbyName);
                        Console.WriteLine($"the player {j.Profil.Pseudo} as succeffuluy leave the lobby {lobby.Id}");
                    }
                    else
                    {
                        Console.WriteLine($"there is {lobby.Joueurs.Count} in the lobby {lobby.Id}");
                    }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to leave the lobby {e.Message}");
            }
        }

        public async Task StartPartie(string partieName, string joueurName, string carteName)
        {
            string key = $"Lobbys:{partieName}";


            if (RedisProvider.Instance.RedisDataBase.KeyExists(key))
            {
                RedisResult result = await RedisProvider.Instance.RedisDataBase.JsonGetAsync(key);

                try
                {
                    Lobby? lobby = JsonConvert.DeserializeObject<Lobby?>(result.ToString());
                    Carte carte = CreateCarte1();

                    if (lobby.Owner == joueurName)
                    {
                        Console.WriteLine($"{joueurName} try to Start the game");

                        //Create Partie For the Server
                        Partie p = new Partie(carte, lobby.Joueurs, lobby.Id);

                        List<Partie> partieList = JurasicRiskGameServer.Get.Parties;
                        if (partieList.Find(partie => partie.Id == p.Id) == null)
                        {
                            partieList.Add(p);
                        }



                        foreach (Joueur j in lobby.Joueurs)
                        {
                            await Clients.Client(j.Profil.ConnectionId).SendAsync("ReceivePartie");
                        }
                        await Clients.Client(lobby.Joueurs[p.NextPlayer()].Profil.ConnectionId).SendAsync("YourTurn", p.Etat.ToString());
                        await ExitLobby(joueurName,partieName);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        #region Private

        private Carte CreateCarte1()
        {
            Dictionary<string, ITerritoireBase> territoires = new Dictionary<string, ITerritoireBase>();
            for (int i = 0; i < 41; i++)
            {
                territoires.Add(i.ToString(), new TerritoireBase(i, null));
            }
            List<IContinent> _continents = new List<IContinent>
            {
                new Continent(territoires.Take(7).ToDictionary(x => x.Key, y => y.Value)),
                new Continent(territoires.Skip(7).Take(7).ToDictionary(x => x.Key, y => y.Value)),
                new Continent(territoires.Skip(14).Take(8).ToDictionary(x => x.Key, y => y.Value)),
                new Continent(territoires.Skip(22).Take(7).ToDictionary(x => x.Key, y => y.Value)),
                new Continent(territoires.Skip(29).Take(5).ToDictionary(x => x.Key, y => y.Value)),
                new Continent(territoires.Skip(34).Take(7).ToDictionary(x => x.Key, y => y.Value))
            };
            Dictionary<string, IContinent> dic = new Dictionary<string, IContinent>();
            for (int i = 0; i < _continents.Count; i++)
            {
                dic.Add(i.ToString(), _continents[i]);
            }


            return new Carte(dic, null);
        }


        private async Task<Lobby> GetLobby(string lobbyName)
        {
            string key = $"Lobbys:{lobbyName}";
            Lobby? lobby = null;
            if (RedisProvider.Instance.RedisDataBase.KeyExists(key))
            {
                RedisResult result = await RedisProvider.Instance.RedisDataBase.JsonGetAsync(key);
                lobby = JsonConvert.DeserializeObject<Lobby>(result.ToString());
            }

            return lobby;
        }
        #endregion

        #region Override
        public override async Task OnConnectedAsync()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Connected {Context.ConnectionId} {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}");
            await Clients.Client(Context.ConnectionId).SendAsync("connected", Context.ConnectionId);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {

            await Clients.Client(Context.ConnectionId).SendAsync("disconnected");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Disconnected {Context.ConnectionId} {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}");
            Console.ForegroundColor = ConsoleColor.White;
            if (exception != null)
            {
                Console.WriteLine(exception.Message);
            }
            await base.OnDisconnectedAsync(exception);


        }
        #endregion
    }
}
