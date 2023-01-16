using Microsoft.AspNetCore.SignalR;
using ModelsAPI.ClassMetier;
using Redis.OM.Searching;
using Redis.OM;
using RISKAPI.Services;
using ModelsAPI.ClassMetier.Player;
using Microsoft.AspNet.SignalR.Hubs;
using ModelsAPI.ClassMetier.Units;

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
            Joueur joueurSuivant = null;
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
                for (int i = 0; i < lobby.Joueurs.Count; i++)
                {
                    if (lobby.Joueurs[i].Profil.Pseudo == joueurName)
                    {
                        joueur = lobby.Joueurs[i];
                        joueurSuivant = lobby.Joueurs[i + 1 % (lobby.Joueurs.Count + 1)];
                    }
                }
            }
            if (joueurSuivant != null)
            {
                if (lobby.Partie.Etat.GetType().Name == "Placement")
                {
                    await Clients.Client(joueurSuivant.Profil.ConnectionId).SendAsync("yourTurn", "placement");
                }
                await Clients.Client(joueur.Profil.ConnectionId).SendAsync("EndTurn");
            }
        }

        public async Task Action(List<IUnit> unitlist)
        {

        }
    }
}
