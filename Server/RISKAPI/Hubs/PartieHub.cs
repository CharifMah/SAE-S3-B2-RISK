using Microsoft.AspNetCore.SignalR;
using ModelsAPI.ClassMetier;
using Redis.OM.Searching;
using Redis.OM;
using RISKAPI.Services;
using ModelsAPI.ClassMetier.Player;
using Microsoft.AspNet.SignalR.Hubs;
using ModelsAPI.ClassMetier.Units;
using ModelsAPI.ClassMetier.GameStatus;

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
            if (lobby != null)
            {
                joueur = lobby.Joueurs[lobby.Partie.NextPlayer()];
                lobby.Partie.Transition();
                await Clients.Client(joueur.Profil.ConnectionId).SendAsync("yourTurn", lobby.Partie.Etat.ToString());
                Console.WriteLine($"c'est au tour de {joueur.Profil.Pseudo}");
            }
        }

        public async Task Action(string lobbyName, List<int> unitlist)
        {
            Partie p = JurasicRiskGameServer.Get.Lobbys.First(l => l.Id == lobbyName).Partie;
            p.Action(unitlist);
        }      
    }
}
