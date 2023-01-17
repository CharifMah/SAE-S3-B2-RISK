using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using ModelsAPI.ClassMetier;
using ModelsAPI.ClassMetier.GameStatus;
using ModelsAPI.ClassMetier.Player;
using Redis.OM;
using System;

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

        public async Task EndTurn(string partieName, string joueurName)
        {
            Partie partie = null;
            foreach (Partie l in JurasicRiskGameServer.Get.Parties)
            {
                if (l.Id == partieName)
                {
                    partie = l;
                    break;
                }
            }
            if (partie != null && partie.Joueurs[partie.PlayerIndex].Profil.Pseudo == joueurName)
            {
                Joueur joueur = partie.Joueurs[partie.NextPlayer()];
                partie.Transition();
                await Clients.Client(joueur.Profil.ConnectionId).SendAsync("yourTurn", partie.Etat.ToString());
                Console.WriteLine($"c'est au tour de {joueur.Profil.Pseudo}");
            }
            else
            {
                Console.WriteLine($"{joueurName} essaye de finir le tour alors que c'est au tour de {partie.Joueurs[partie.PlayerIndex].Profil.Pseudo}");
            }
        }

        /// <summary>
        /// Call Action de la partie correspondant a l'état du joueur actuel 
        /// </summary>
        /// <param name="partieName">nom de la partie</param>
        /// <param name="unitlist">list index d'unité</param>
        /// <returns>Task</returns>
        public async Task Action(string partieName, List<int> unitlist)
        {
            Partie p = JurasicRiskGameServer.Get.Parties.First(l => l.Id == partieName);
            p.Action(unitlist);

            switch (p.Etat.ToString())
            {
                case "Deploiment":
                    Deploiment d = (p.Etat as Deploiment);
                    if (d.IdUniteRemove != null && d.IdTerritoireUpdate != null && p.PlayerIndex != -1)
                    {
                        foreach (Joueur j in p.Joueurs)
                        {
                            await Clients.Client(j.Profil.ConnectionId).SendAsync("deploiment", d.IdUniteRemove, d.IdTerritoireUpdate, p.PlayerIndex);
                        }

                        Console.WriteLine($"Deployement Update from {Context.ConnectionId}");
                    }
                   
                    break;
            }
        }

        public async Task SetSelectedTerritoire(string partieName, int ID)
        {
            Partie p = JurasicRiskGameServer.Get.Parties.First(partie => partie.Id == partieName);
            p.Carte.SelectedTerritoire = p.Carte.GetTerritoire(ID);
            Console.WriteLine("Selected Territoire set to " + ID);
        }

        public async Task ConnectedPartie(string partieName, string joueurName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, partieName);
            Partie partie = null;

            foreach (Partie p in JurasicRiskGameServer.Get.Parties)
            {
                if (p.Id == partieName)
                {
                    partie = p;
                    break;
                }
            }
            if (partie != null)
            {
                Joueur j = partie.Joueurs.Find(j => j.Profil.Pseudo == joueurName);
               j.Profil.ConnectionId = Context.ConnectionId;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{joueurName} connected to {partieName}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public async Task ExitPartie(string partieName, string joueurName)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Disconnected {Context.ConnectionId} {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}");
            Console.ForegroundColor = ConsoleColor.White;
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, partieName);
            try
            {
                Partie partie = null;

                foreach (Partie p in JurasicRiskGameServer.Get.Parties)
                {
                    if (p.Id == partieName)
                    {
                        partie = p;
                        break;
                    }
                }
                if (partie != null)
                {
                    Joueur j = partie.Joueurs.Find(j => j.Profil.Pseudo == joueurName);
                    partie.ExitPartie(j);
                    Groups.RemoveFromGroupAsync(Context.ConnectionId, partieName);
                    Console.WriteLine($"the player {j.Profil.Pseudo} as succeffuluy leave the party {partie.Id}");
                }         
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed to leave the party ????");
            }
        }

        #region Override
        public override async Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ConnectedPartie", Context.ConnectionId);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Connected to the game {Context.ConnectionId} {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}");
            Console.ForegroundColor = ConsoleColor.White;
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Disconnected {Context.ConnectionId} {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}");
            Console.ForegroundColor = ConsoleColor.White;
            await Clients.Client(Context.ConnectionId).SendAsync("disconnected");
            if (exception != null)
            {
                Console.WriteLine(exception.Message);
            }

            await base.OnDisconnectedAsync(exception);
        }
        #endregion
    }
}
