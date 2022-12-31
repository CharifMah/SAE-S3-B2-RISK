using Microsoft.AspNetCore.SignalR;
using ModelsAPI.ClassMetier;
using ModelsAPI.ClassMetier.Player;
using Newtonsoft.Json;
using NReJSON;
using RISKAPI.Services;
using StackExchange.Redis;

namespace RISKAPI.Hubs
{
    public class LobbyHub : Hub
    {

        public async Task SendLobby(Lobby lobby)
        {
            await Clients.All.SendAsync("ReceiveLobby", lobby);
        }

        public async Task JoinLobby(Joueur joueur,string lobbyName)
        {
            string key = $"Lobby:{lobbyName}";
            if (RedisProvider.Instance.RedisDataBase.KeyExists(key))
            {
                RedisResult result = await RedisProvider.Instance.RedisDataBase.JsonGetAsync(key);
                Lobby? lobby = JsonConvert.DeserializeObject<Lobby>(result.ToString());
                if (lobby != null)
                {
                    lobby.JoinLobby(joueur);
                    await Groups.AddToGroupAsync(Context.ConnectionId, lobbyName);

                    await Clients.Group(lobbyName).SendAsync("JoinLobby", lobby);
                }
            }
        }
    }
}
