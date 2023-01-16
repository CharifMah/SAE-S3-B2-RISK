using Microsoft.AspNetCore.SignalR;
using ModelsAPI.ClassMetier;
using Redis.OM.Searching;
using Redis.OM;
using RISKAPI.Services;
using ModelsAPI.ClassMetier.Player;
using Microsoft.AspNet.SignalR.Hubs;
using ModelsAPI.ClassMetier.Units;
using ModelsAPI.ClassMetier.GameStatus;
using ModelsAPI.ClassMetier.Map;
using Newtonsoft.Json;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace RISKAPI.Hubs
{
    [HubName("PartieHub")]
    public class PartieHub : Hub
    {
        private readonly RedisConnectionProvider _provider;

        public PartieHub(RedisConnectionProvider provider)
        {
            _provider = provider;
        }

        public async Task EndTurn(string lobbyName, string joueurName)
        {
            Lobby lobby = null;
            Joueur joueur = null;
            foreach (Lobby l in JurasicRiskGameServer.Get.Lobbys)
            {
                if (l.Id == lobbyName)
                {
                    lobby = l;
                    break;
                }
            }
            if (lobby != null && lobby.Partie.Joueurs[lobby.Partie.PlayerIndex].Profil.Pseudo == joueurName)
            {
                joueur = lobby.Partie.Joueurs[lobby.Partie.NextPlayer()];
                lobby.Partie.Transition();
                await Clients.Client(joueur.Profil.ConnectionId).SendAsync("yourTurn", lobby.Partie.Etat.ToString());
                Console.WriteLine($"c'est au tour de {joueur.Profil.Pseudo}");
            }
            else
            {
                Console.WriteLine($"{joueurName} essaye de finir le tour alors que c'est au tour de {lobby.Partie.Joueurs[lobby.Partie.PlayerIndex].Profil.Pseudo}");
            }
        }

        public async Task Action(string lobbyName, List<int> unitlist)
        {
            Partie p = JurasicRiskGameServer.Get.Lobbys.First(l => l.Id == lobbyName).Partie;
            p.Action(unitlist);
            switch (p.Etat.ToString())
            {
                case "Deploiment":
                    Deploiment d = (p.Etat as Deploiment);
                    foreach (Joueur joueur in p.Joueurs)
                    {
                        await Clients.Client(joueur.Profil.ConnectionId).SendAsync("deploiment", d.IdUniteRemove, d.IdTerritoireUpdate, p.PlayerIndex);
                    }
                    break;
            }
        }

        public async Task SetSelectedTerritoire(string lobbyName, int ID)
        {
            Partie p = JurasicRiskGameServer.Get.Lobbys.First(l => l.Id == lobbyName).Partie;
            p.Carte.SelectedTerritoire = p.Carte.GetTerritoire(ID);
        }

        public async Task ConnectedPartie(string lobbyName, string joueurName,string connectionID)
        {
            Lobby lobby = null;
            Joueur joueur = null;
            foreach (Lobby l in JurasicRiskGameServer.Get.Lobbys)
            {
                if (l.Id == lobbyName)
                {
                    lobby = l;
                    break;
                }
            }
            Joueur j = lobby.Partie.Joueurs.Find(j => j.Profil.Pseudo == joueurName);
            if (lobby != null && j.Profil.ConnectionId != connectionID)
            {
                j.Profil.ConnectionId = connectionID;
                if (connectionID != Context.ConnectionId)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Error of sync of connection ID");
                }
            }
        }

        public async Task ExitPartie(string lobbyName)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Disconnected {Context.ConnectionId} {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}");
            Console.ForegroundColor = ConsoleColor.White;
         
            try
            {
                Lobby lobby = null;
                Joueur joueur = null;
                foreach (Lobby l in JurasicRiskGameServer.Get.Lobbys)
                {
                    if (l.Id == lobbyName)
                    {
                        lobby = l;
                        break;
                    }
                }
                Joueur j = lobby.Partie.Joueurs.Find(j => j.Profil.ConnectionId == Context.ConnectionId);
                if (lobby != null)
                {
                    lobby.Partie.Joueurs.Remove(j);
                }
                Context.Items.Remove(Context.ConnectionId);
                Console.WriteLine($"the player ??? as succeffuluy leave the party ???");
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed to leave the party ????");
            }
        }

        #region Override
        public override async Task OnConnectedAsync()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Connected to the game {Context.ConnectionId} {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}");
            await Clients.Client(Context.ConnectionId).SendAsync("connected", Context.ConnectionId);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("disconnected");
            await base.OnDisconnectedAsync(exception);
        }
        #endregion
    }
}
