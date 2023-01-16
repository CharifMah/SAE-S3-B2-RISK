using Microsoft.AspNetCore.SignalR.Client;

namespace Models.Services
{
    ///<Author>Mahmoud Charif</Author>
    public class SignalRPartieService
    {
        private readonly HubConnection _connection;

        public event Action<string> YourTurn;
        public event Action EndTurn;
        public event Action<string> Connected;
        public event Action Disconnected;

        /// <summary>
        /// SignalRPartieService
        /// </summary>
        /// <param name="connection">HubConnection</param>
        public SignalRPartieService(HubConnection connection)
        {
            _connection = connection;
            _connection.On<string>("yourTurn", (turnType) => YourTurn?.Invoke(turnType));
            _connection.On("endTurn", () => EndTurn?.Invoke());
            _connection.On<string>("connected", (connexionId) => Connected?.Invoke(connexionId));
            _connection.On("disconnected", () => Disconnected?.Invoke());

        }

        public async Task SendEndTurn(string lobbyName, string joueurName)
        {
            await _connection.SendAsync("EndTurn", lobbyName, joueurName);
        }

        public async Task Action(string lobbyName, List<int> units)
        {
            await _connection.SendAsync("Action", lobbyName, units);
        }

        public async Task SetSelectedTerritoire(string lobbyName, int ID)
        {
            await _connection.SendAsync("SetSelectedTerritoire", lobbyName, ID);
        }
    }
}
