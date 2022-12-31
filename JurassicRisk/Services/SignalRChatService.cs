using Microsoft.AspNetCore.SignalR.Client;
using Models.Player;
using System;
using System.Threading.Tasks;

namespace JurassicRisk.Services
{
    public class SignalRChatService
    {
        private readonly HubConnection _connection;

        public event Action<Lobby> LobbyReceived;
        public event Action<Lobby> LobbyJoined;

        public SignalRChatService(HubConnection connection)
        {
            _connection = connection;

            _connection.On<Lobby>("ReceiveLobby", (lobby) => LobbyReceived?.Invoke(lobby));
            //_connection.On<Lobby>("JoinLobby", (lobby) => LobbyJoined?.Invoke(lobby));
        }


        public async Task Connect()
        {
            await _connection.StartAsync();
        }

        public async Task JoinLobby(Joueur joueur,string lobbyName)
        {
            await _connection.SendAsync("JoinLobby", joueur, lobbyName);
        }

        public async Task SendLobby(Lobby lobby)
        {
            await _connection.SendAsync("SendLobby", lobby);
        }


    }
}
