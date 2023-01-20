using Microsoft.AspNetCore.SignalR.Client;
using Models.GameStatus;
using Models.Player;
using Newtonsoft.Json;
using System.Windows;

namespace Models.Services
{
    ///<Author>Mahmoud Charif</Author>
    public class SignalRLobbyService
    {
        private readonly HubConnection _connection;
        public event Action<string> LobbyReceived;
        public event Action<string> LobbyJoined;
        public event Action<string,string> Connected;
        public event Action Disconnected;
        public event Action<string> ConnectedToLobby;
        public event Action NotOwner;

        /// <summary>
        /// SignalRLobbyService
        /// </summary>
        /// <param name="connection">HubConnection</param>
        public SignalRLobbyService(HubConnection connection)
        {
            _connection = connection;

            _connection.On<string>("ReceiveLobby", (lobbyJson) => LobbyReceived?.Invoke(lobbyJson));
            _connection.On<string>("JoinLobby", (lobbyJson) => LobbyJoined?.Invoke(lobbyJson));
            _connection.On<string,string>("connected", (connexionId, connected) => Connected?.Invoke(connexionId, connected));
            _connection.On("disconnected", () => Disconnected?.Invoke());
            _connection.On("NotOwner", () => NotOwner?.Invoke());
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
        public async Task JoinLobby(string profilJson, string lobbyName, string password)
        {
            await _connection.SendAsync("JoinLobby", profilJson, lobbyName, password);
        }

        /// <summary>
        /// Exit Lobby
        /// </summary>
        /// <param name="joueurName">joueurName</param>
        /// <param name="id">id of the player</param>
        /// <returns></returns>
        public async Task ExitLobby(string joueurName,string id)
        {
            await _connection.SendAsync("ExitLobby", joueurName, id); 
        }

        /// <summary>
        /// Force Exit Lobby to force player to exit lobby 
        /// </summary>
        /// <param name="joueurName">player name</param>
        /// <returns>Task</returns>
        public async Task ForceExitLobby(string joueurName)
        {
            await _connection.SendAsync("ForceExitLobby", joueurName);
        }

        /// <summary>
        /// Set Team of the player
        /// </summary>
        /// <param name="team">team</param>
        /// <param name="pseudo">pseudo</param>
        /// <param name="lobbyId">loobyId</param>
        /// <returns></returns>
        public async Task SetTeam(Teams team, string pseudo, string lobbyId)
        {
            await _connection.SendAsync("SetTeam", team, pseudo, lobbyId);
        }

        /// <summary>
        /// Send if the player is ready
        /// </summary>
        /// <param name="ready">ready status</param>
        /// <param name="pseudo">pseudo</param>
        /// <param name="lobbyId">lobbyId</param>
        /// <returns></returns>
        public async Task IsReady(bool ready, string pseudo, string lobbyId)
        {
            await _connection.SendAsync("IsReady", ready, pseudo, lobbyId);
        }
    }
}
