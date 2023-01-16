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
        public event Action<int, int> Deploiment; 
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
            _connection.On<int, int>("deploiment", (idUnit, idTerritoire) => Deploiment?.Invoke(idUnit, idTerritoire));
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

        public async Task ConnectedPartie(string lobbyName, string joueurName,string connectionID)
        {
            await _connection.SendAsync("ConnectedPartie", lobbyName, joueurName, connectionID);
        }

        public async Task ExitPartie()
        {
            try
            {
                await _connection.SendAsync("ExitPartie",JurasicRiskGameClient.Get.Lobby.Id);
            }
            catch (InvalidOperationException e)
            {
                throw e;
            }
        }
    }
}
