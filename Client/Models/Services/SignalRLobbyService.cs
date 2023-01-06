using Microsoft.AspNetCore.SignalR.Client;
using Models.GameStatus;
using Models.Player;
using Newtonsoft.Json;

namespace Models.Services
{
    ///<Author>Mahmoud Charif</Author>
    public class SignalRLobbyService
    {
        private readonly HubConnection _connection;
        public event Action PartieReceived;
        public event Action<string> LobbyReceived;
        public event Action<string> LobbyJoined;
        public event Action<string> Connected;
        public event Action Disconnected;
        public event Action<string> ConnectedToLobby;
        public event Action<string> YourTurn;
        public event Action EndTurn;

        /// <summary>
        /// SignalRLobbyService
        /// </summary>
        /// <param name="connection">HubConnection</param>
        public SignalRLobbyService(HubConnection connection)
        {
            _connection = connection;
            _connection.On("ReceivePartie", () => PartieReceived?.Invoke());
            _connection.On<string>("ReceiveLobby", (lobbyJson) => LobbyReceived?.Invoke(lobbyJson));
            _connection.On<string>("JoinLobby", (lobbyJson) => LobbyJoined?.Invoke(lobbyJson));
            _connection.On<string>("connectedToLobby", (connected) => ConnectedToLobby?.Invoke(connected));
            _connection.On<string>("connected", (connexionId) => Connected?.Invoke(connexionId));
            _connection.On("disconnected", () => Disconnected?.Invoke());
            _connection.On<string>("yourTurn", (turnType) => YourTurn?.Invoke(turnType));
            _connection.On("endTurn", () => EndTurn?.Invoke());
        }

        public async Task StartPartie(string lobbyName, string joueurName, string carteName)
        {
            await _connection.SendAsync("StartPartie", lobbyName, joueurName, carteName);
        }

        /// <summary>
        /// Send Lobby to server
        /// </summary>
        /// <param name="lobby">Lobby to send</param>
        /// <returns>Task</returns>
        public async Task SendLobby(Lobby lobby)
        {
            string lobbyJson = JsonConvert.SerializeObject(lobby);
            await _connection.SendAsync("SendLobby", lobbyJson);
        }

        /// <summary>
        /// Join a Lobby
        /// </summary>
        /// <param name="joueur">player qui rejoint</param>
        /// <param name="lobbyName">Lobby to join</param>
        /// <returns>Task</returns>
        public async Task JoinLobby(Joueur joueur, string lobbyName, string password)
        {
            string joueurJson = JsonConvert.SerializeObject(joueur);
            await _connection.SendAsync("JoinLobby", joueurJson, lobbyName, password);
        }

        public async Task ExitLobby()
        {
            try
            {
                await _connection.SendAsync("ExitLobby");
            }
            catch (InvalidOperationException e)
            {
                throw e;
            }
        }

        public async Task SetTeam(Teams teams, string pseudo, string lobbyId)
        {
            await _connection.SendAsync("SetTeam", teams, pseudo, lobbyId);
        }

        public async Task IsReady(bool ready, string pseudo, string lobbyId)
        {
            await _connection.SendAsync("IsReady", ready, pseudo, lobbyId);
        }






    }
}
