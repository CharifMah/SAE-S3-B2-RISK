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
        public event Action StartGame;


        /// <summary>
        /// SignalRLobbyService
        /// </summary>
        /// <param name="connection">HubConnection</param>
        public SignalRLobbyService(HubConnection connection)
        {
            _connection = connection;

            _connection.On<string>("ReceiveLobby", (lobbyJson) => LobbyReceived?.Invoke(lobbyJson));
            _connection.On<string>("JoinLobby", (lobbyJson) => LobbyJoined?.Invoke(lobbyJson));
            _connection.On("startgame", () => StartGame?.Invoke());
            _connection.On<string,string>("connected", (connexionId, connected) => Connected?.Invoke(connexionId, connected));
            _connection.On("disconnected", () => Disconnected?.Invoke());
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

        public async Task ExitLobby(string joueurName,string id)
        {
            await _connection.SendAsync("ExitLobby", joueurName, id); 
        }

        public async Task ForceExitLobby(string joueurName)
        {
            await _connection.SendAsync("ForceExitLobby", joueurName);
        }

        public async Task StartGameOtherPlayer(string lobbyName)
        {
            await _connection.SendAsync("StartGameOtherPlayer", lobbyName);
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
