﻿using JurassicRisk.ViewsModels;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using Models.Player;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace JurassicRisk.Services
{
    ///<Author>Mahmoud Charif</Author>
    public class SignalRLobbyService
    {
        private readonly HubConnection _connection;

        public event Action<string> LobbyReceived;
        public event Action<string> LobbyJoined;
        public event Action<string> Connected;

        /// <summary>
        /// SignalRLobbyService
        /// </summary>
        /// <param name="connection">HubConnection</param>
        public SignalRLobbyService(HubConnection connection)
        {
            _connection = connection;

            _connection.On<string>("ReceiveLobby", (lobbyJson) => LobbyReceived?.Invoke(lobbyJson));
            _connection.On<string>("JoinLobby", (lobbyJson) => LobbyJoined?.Invoke(lobbyJson));
            _connection.On<string>("connected", (lobbyJson) => Connected?.Invoke(lobbyJson));
        }

        /// <summary>
        /// Starts a connection to the servers
        /// </summary>
        /// <returns></returns>
        public async Task Connect()
        {
            await _connection.StartAsync();
        }

        /// <summary>
        /// Join a Lobby
        /// </summary>
        /// <param name="joueur">player qui rejoint</param>
        /// <param name="lobbyName">lobby to join</param>
        /// <returns>Task</returns>
        public async Task JoinLobby(Joueur joueur, string lobbyName)
        {
            string joueurJson = JsonConvert.SerializeObject(joueur);
            await _connection.SendAsync("JoinLobby", joueurJson, lobbyName);
        }

        public async Task SetTeam(Teams teams)
        {
            await _connection.SendAsync("SetTeam", teams, JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo, JurassicRiskViewModel.Get.LobbyVm.Lobby.Id);
        }

        /// <summary>
        /// Send lobby to server
        /// </summary>
        /// <param name="lobby">lobby to send</param>
        /// <returns>Task</returns>
        public async Task SendLobby(Lobby lobby)
        {
            string lobbyJson = JsonConvert.SerializeObject(lobby);
            await _connection.SendAsync("SendLobby", lobbyJson);
        }
    }
}
