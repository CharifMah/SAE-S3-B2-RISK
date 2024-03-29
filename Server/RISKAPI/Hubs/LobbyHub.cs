﻿using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using ModelsAPI;
using ModelsAPI.ClassMetier.GameStatus;
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

        /// <summary>
        /// RefreshLobby to all clients
        /// </summary>
        /// <param name="lobbyName"></param>
        /// <returns>Task</returns>
        public async Task RefreshLobbyToClients(string lobbyName)
        {
            string key = $"Lobbys:{lobbyName}";
            Lobby? lobby = null;
            if (RedisProvider.Instance.RedisDataBase.KeyExists(key))
            {
                RedisResult result = await RedisProvider.Instance.RedisDataBase.JsonGetAsync(key);
                lobby = JsonConvert.DeserializeObject<Lobby>(result.ToString());
            }

            if (lobby != null)
            {
                string? lobbyJson = JsonConvert.SerializeObject(lobby);

                await Clients.Group(lobbyName).SendAsync("ReceiveLobby", lobbyJson);


            }
            else
            {
                Console.WriteLine("Lobby is null");
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

        /// <summary>
        /// John lobby 
        /// </summary>
        /// <param name="profilJson">profil json who join</param>
        /// <param name="lobbyName">the lobby name</param>
        /// <param name="password">password of the lobby</param>
        /// <returns></returns>
        public async Task JoinLobby(string profilJson, string lobbyName, string password)
        {
            Profil? profil = JsonConvert.DeserializeObject<Profil>(profilJson);
            if (profil != null)
            {
                Lobby lobby = null;

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
                    Joueur j = lobby.Joueurs.Find(j => j.Profil.Pseudo == profil.Pseudo);
                    if (j == null)
                    {
                        j = new Joueur(profil, Teams.NEUTRE);
                    }
                    if (j != null)
                    {
                        Console.WriteLine($"{profil.Pseudo} try to Join {lobbyName}");

                        PasswordHasher<Lobby> passwordHasher = new PasswordHasher<Lobby>();
                        if (lobby.Password == "" || passwordHasher.VerifyHashedPassword(lobby, lobby.Password, password) != 0)
                        {
                            if (lobby.Joueurs.Count < 4)
                            {
                                j.Profil.ConnectionId = Context.ConnectionId;
                                await Groups.AddToGroupAsync(Context.ConnectionId, lobbyName);

                                lobby.JoinLobby(j);


                                List<Lobby> lobbyList = JurasicRiskGameServer.Get.Lobbys;
                                if (lobbyList.Find(l => l.Id == lobby.Id) == null)
                                {
                                    lobbyList.Add(lobby);
                                }

                                await _lobby.UpdateAsync(lobby);
                                await RefreshLobbyToClients(lobbyName);
                                await Clients.Client(Context.ConnectionId).SendAsync("connected", "true");
                            }
                            else
                            {
                                await Clients.Client(Context.ConnectionId).SendAsync("connected", Context.ConnectionId, "false");
                                Console.WriteLine($"Plus de place dans le lobby pour que {j.Profil.Pseudo} rejoingne");
                            }
                        }
                        else
                        {
                            await Clients.Client(Context.ConnectionId).SendAsync("connected", Context.ConnectionId, "false");
                            Console.WriteLine("mauvais mot de passe");
                        }
                    }
                }
                else
                {
                    await Clients.Client(Context.ConnectionId).SendAsync("connected", Context.ConnectionId, "false");
                    Console.WriteLine("Le lobby n'existe pas");
                }
            }
            else
            {
                await Clients.Client(Context.ConnectionId).SendAsync("connected", Context.ConnectionId, "false");
                Console.WriteLine("Le profil envoyer est null");
            }

        }

        /// <summary>
        /// Set team for the player
        /// </summary>
        /// <param name="teams">Team of the player</param>
        /// <param name="joueurName">name of the player</param>
        /// <param name="lobbyName">lobby name where the player is</param>
        /// <returns></returns>
        public async Task SetTeam(Teams teams, string joueurName, string lobbyName)
        {
            string key = $"Lobbys:{lobbyName}";
            string param = $"$.Joueurs[?(@Profil.Pseudo == {'"' + joueurName + '"'})]";

            Lobby lobby = null;
            Joueur j = null;
            foreach (Lobby p in JurasicRiskGameServer.Get.Lobbys)
            {
                if (p.Id == lobbyName)
                {
                    lobby = p;
                    j = lobby.Joueurs.Find(jo => jo.Profil.Pseudo == joueurName);
                    if (j != null)
                    {
                        Console.WriteLine($"{j.Profil.Pseudo} try to Set Team {j.Team} to {teams}");
                        j.Team = teams;
                        string? joueurJson = JsonConvert.SerializeObject(j);
                        await RedisProvider.Instance.RedisDataBase.JsonSetAsync(key, joueurJson, param);
                        await RefreshLobbyToClients(lobbyName);
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Set ready for the player
        /// </summary>
        /// <param name="ready">status ready or not</param>
        /// <param name="joueurName">joueurName</param>
        /// <param name="lobbyName">lobby Name</param>
        /// <returns></returns>
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

        /// <summary>
        /// Exit Lobby
        /// </summary>
        /// <param name="joueurName">joueur Name</param>
        /// <param name="lobbyName">lobby Name</param>
        /// <returns>Task</returns>
        public async Task ExitLobby(string joueurName, string lobbyName)
        {

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Disconnected from methode ExitLobby {Context.ConnectionId} {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}");
            Console.ForegroundColor = ConsoleColor.White;
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, lobbyName);
            try
            {
                Lobby lobby = null;
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
                        await Clients.Client(Context.ConnectionId).SendAsync("disconnected");
                        lobby.Joueurs.Remove(j);

                        Console.WriteLine($"the player {j.Profil.Pseudo} as succeffuluy leave the lobby {lobby.Id}");
                    }

                    await Clients.Client(Context.ConnectionId).SendAsync("disconnected");
                    await _lobby.UpdateAsync(lobby);
                    await RefreshLobbyToClients(lobbyName);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to leave the lobby {e.Message}");
            }
        }

        #region Override
        public override async Task OnConnectedAsync()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Connected {Context.ConnectionId} {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Disconnected from OnDisconnectAsync ExitLobby {Context.ConnectionId} {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}");
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
