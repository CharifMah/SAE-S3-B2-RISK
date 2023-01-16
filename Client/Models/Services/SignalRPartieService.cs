using Microsoft.AspNetCore.SignalR.Client;
using Models.GameStatus;
using Models.Player;
using Models.Units;
using Newtonsoft.Json;

namespace Models.Services
{
    ///<Author>Mahmoud Charif</Author>
    public class SignalRPartieService
    {
        private readonly HubConnection _connection;

        public event Action<string> YourTurn;
        public event Action EndTurn;

        /// <summary>
        /// SignalRPartieService
        /// </summary>
        /// <param name="connection">HubConnection</param>
        public SignalRPartieService(HubConnection connection)
        {
            _connection = connection;
            _connection.On<string>("yourTurn", (turnType) => YourTurn?.Invoke(turnType));
            _connection.On("endTurn", () => EndTurn?.Invoke());
        }

        public async Task SendEndTurn(string lobbyName, string joueurName)
        {
            await _connection.SendAsync("EndTurn", lobbyName, joueurName);
        }

        public async Task Action(string lobbyName, List<int> units)
        {
            await _connection.SendAsync("Action", lobbyName, units);
        }
    }
}
