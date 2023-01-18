using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.SignalR;
using ModelsAPI.ClassMetier;
using ModelsAPI.ClassMetier.GameStatus;
using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Player;
using Newtonsoft.Json;
using Redis.OM;

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

        /// <summary>
        /// Termine le tour
        /// </summary>
        /// <param name="partieName">nom de la partie</param>
        /// <param name="joueurName">nom du joueur</param>
        /// <returns></returns>
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
            if (partie != null && partie.Joueurs.Count > 0 && partie.Joueurs[partie.PlayerIndex].Profil.Pseudo == joueurName)
            {
                Joueur joueur = partie.Joueurs[partie.NextPlayer()];
                partie.Transition();
                await Clients.Client(joueur.Profil.ConnectionId).SendAsync("yourTurn", JsonConvert.SerializeObject(partie.Etat));
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

            if (p.Action(unitlist))
            {
                switch (p.Etat.ToString())
                {
                    case "Deploiment":
                        Deploiment d = (p.Etat as Deploiment);
                        if (p.PlayerIndex != -1)
                        {

                            await Clients.Group(partieName).SendAsync("deploiment", d.IdUniteRemove, d.IdTerritoireUpdate, p.PlayerIndex);

                            await Clients.Client(p.Joueurs[p.PlayerIndex].Profil.ConnectionId).SendAsync("endTurn");

                            Console.WriteLine($"Deployement Update from {Context.ConnectionId}");
                        }

                        break;
                }
            }

        }

        /// <summary>
        /// Actualise le territoire selectionnée par l'utilisateur pour toute la partie
        /// </summary>
        /// <param name="partieName"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task SetSelectedTerritoire(string partieName, int ID)
        {
            Partie p = JurasicRiskGameServer.Get.Parties.First(partie => partie.Id == partieName);
            p.Carte.SelectedTerritoire = p.Carte.GetTerritoire(ID);
            Console.WriteLine("Selected Territoire set to " + ID);
        }

        /// <summary>
        /// Ajoute l'utilisateur au groupe et a la partie
        /// </summary>
        /// <param name="partieName">nom de la partie</param>
        /// <param name="joueurName">nom du joueur</param>
        /// <returns>Task</returns>
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

            if (partie != null && partie.Joueurs.Count > 0)
            {
                Joueur j = partie.Joueurs.FirstOrDefault(j => j.Profil.Pseudo == joueurName);
                if (j == null)
                {
                    Lobby lobby = null;
                    foreach (Lobby l in JurasicRiskGameServer.Get.Lobbys)
                    {
                        if (l.Id == partieName)
                        {
                            lobby = l;
                            break;
                        }
                    }
                    if (lobby != null)
                        partie.JoinPartie(lobby.Joueurs.FirstOrDefault(j=>j.Profil.Pseudo == joueurName));
                }
                else
                j.Profil.ConnectionId = Context.ConnectionId;    
                
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{joueurName} connected to {partieName}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Lance la partie si c'est l'owner du lobby qui fait l'action play
        /// </summary>
        /// <param name="partieName">nom de la partie</param>
        /// <param name="joueurName">nom du joueur qui fait l'action</param>
        /// <param name="carteName">nom de la Carte</param>
        /// <returns>Task</returns>
        public async Task StartPartie(string partieName, string joueurName, string carteName)
        {
            bool find = false;
            Lobby lobby = null;
            Joueur joueur = null;
            Partie partie = null;
            string joueursJson = "";
            foreach (Lobby l in JurasicRiskGameServer.Get.Lobbys)
            {
                if (l.Id == partieName)
                {
                    foreach (Joueur j in l.Joueurs)
                    {
                        if (j.Profil.Pseudo == joueurName)
                        {
                            joueur = j;
                            break;
                        }
                    }
                    lobby = l;
                }

                if (find)
                {
                    break;
                }
            }



            foreach (Partie p in JurasicRiskGameServer.Get.Parties)
            {
                if (p.Id == partieName)
                {
                    partie = p;
                    break;
                }
            }

            //Lance la partie si c'est le Owner qui fait l'action Play
            if (lobby != null && lobby.Owner == joueurName)
            {
                Console.WriteLine($"{joueurName} try to Start the game");

                Carte carte = CreateCarte1();
                Console.WriteLine("Carte Created");

                //Create Partie For the Server
                Partie p = new Partie(carte, lobby.Joueurs, lobby.Id);

                //Ajoute la partie if don't exist
                if (partie == null)
                {
                    Console.WriteLine("Partie Created");

                    if (lobby.Joueurs.Count > 0)
                    {

                        JurasicRiskGameServer.Get.Parties.Add(p);
                        joueursJson = JsonConvert.SerializeObject(p.Joueurs);
                        if (joueursJson == "")
                        {
                            Console.WriteLine("JsonJoueursssss VIDE");
                        }
                        await Clients.Group(partieName).SendAsync("ReceivePartie", joueursJson, partieName,p.Etat);

                        
                        await Clients.Client(lobby.Joueurs[p.NextPlayer()].Profil.ConnectionId).SendAsync("YourTurn", JsonConvert.SerializeObject(p.Etat));
                    }
                    else
                    {
                        Console.WriteLine("Errorrr 0 Players in lobby");
                    }
                }
                else if (lobby.Joueurs.Count > 0)
                {
                    partie = p;
                    if (p.Joueurs != null)
                    {
                        await Clients.Group(partieName).SendAsync("ReceivePartie", JsonConvert.SerializeObject(p.Joueurs), partieName, JsonConvert.SerializeObject(p.Etat));
                    }

                }

            }
            else
            {
                Console.WriteLine($"{joueurName} is not the owner to start a game");
            }
        }

        /// <summary>
        /// Quitte la Partie
        /// </summary>
        /// <param name="partieName">nom de la partie</param>
        /// <param name="joueurName">nom du joueur qui veut quittée</param>
        /// <returns>Task</returns>
        public async Task ExitPartie(string partieName, string joueurName)
        {
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
                    Joueur j = partie.Joueurs.FirstOrDefault(j => j.Profil.Pseudo == joueurName);
                    partie.ExitPartie(j);
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, partieName);
                    Console.WriteLine($"the player {j.Profil.Pseudo} as succeffuluy leave the party {partie.Id}");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Disconnected {Context.ConnectionId} {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}");
                    Console.ForegroundColor = ConsoleColor.White;
  

                    //Supprime les partie vide
                    if (partie.Joueurs.Count <= 0)
                    {
                        foreach (Partie p in JurasicRiskGameServer.Get.Parties)
                        {
                            if (p.Id == partie.Id)
                            {
                                JurasicRiskGameServer.Get.Parties.Remove(p);
                            }
                        }
                    }
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
            await Clients.Client(Context.ConnectionId).SendAsync("connectedgame", Context.ConnectionId);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Connected to the game {Context.ConnectionId} {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}");
            Console.ForegroundColor = ConsoleColor.White;
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Disconnected to the game {Context.ConnectionId} {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}");
            Console.ForegroundColor = ConsoleColor.White;
            await Clients.Client(Context.ConnectionId).SendAsync("disconnected");
            if (exception != null)
            {
                Console.WriteLine(exception.Message);
            }

            await base.OnDisconnectedAsync(exception);
        }
        #endregion

        #region Private

        private Carte CreateCarte1()
        {
            Dictionary<string, ITerritoireBase> territoires = new Dictionary<string, ITerritoireBase>();
            for (int i = 0; i < 41; i++)
            {
                territoires.Add(i.ToString(), new TerritoireBase(i, null));
            }
            List<IContinent> _continents = new List<IContinent>
            {
                new Continent(territoires.Take(7).ToDictionary(x => x.Key, y => y.Value)),
                new Continent(territoires.Skip(7).Take(7).ToDictionary(x => x.Key, y => y.Value)),
                new Continent(territoires.Skip(14).Take(8).ToDictionary(x => x.Key, y => y.Value)),
                new Continent(territoires.Skip(22).Take(7).ToDictionary(x => x.Key, y => y.Value)),
                new Continent(territoires.Skip(29).Take(5).ToDictionary(x => x.Key, y => y.Value)),
                new Continent(territoires.Skip(34).Take(7).ToDictionary(x => x.Key, y => y.Value))
            };
            Dictionary<string, IContinent> dic = new Dictionary<string, IContinent>();
            for (int i = 0; i < _continents.Count; i++)
            {
                dic.Add(i.ToString(), _continents[i]);
            }


            return new Carte(dic, null);
        }

        #endregion
    }
}
