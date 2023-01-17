using Microsoft.AspNet.SignalR.Hubs;
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
                    if (p.PlayerIndex != -1)
                    {
  
                        await Clients.Group(partieName).SendAsync("deploiment", d.IdUniteRemove, d.IdTerritoireUpdate, p.PlayerIndex);

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
                Joueur j = partie.Joueurs.FirstOrDefault(j => j.Profil.Pseudo == joueurName);
                j.Profil.ConnectionId = Context.ConnectionId;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{joueurName} connected to {partieName}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public async Task StartPartie(string partieName, string joueurName, string carteName)
        {
            bool find = false;
            Lobby lobby = null;
            Joueur joueur = null;
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

            if (lobby.Owner == joueurName)
            {
                Console.WriteLine($"{joueurName} try to Start the game");

                Carte carte = CreateCarte1();
                Console.WriteLine("Carte Created");

                //Create Partie For the Server
                Partie p = new Partie(carte, lobby.Joueurs, lobby.Id);
                Console.WriteLine("Partie Created");

                List<Partie> partieList = JurasicRiskGameServer.Get.Parties;
                if (partieList.FirstOrDefault(partie => partie.Id == p.Id) == null)
                {
                    partieList.Add(p);
                }
                else
                {
                    Partie OldPartie = partieList.FirstOrDefault(partie => partie.Id == p.Id);
                    OldPartie = p;
                }
                if (lobby.Joueurs.Count > 0)
                {
                    string joueursJson = JsonConvert.SerializeObject(p.Joueurs);

                    await Clients.Group(partieName).SendAsync("ReceivePartie", joueursJson,partieName);

                    await Clients.Client(lobby.Joueurs[p.NextPlayer()].Profil.ConnectionId).SendAsync("YourTurn", p.Etat.ToString());
                }
                else
                {
                    Console.WriteLine("Errorrr 0 Players in lobby");
                }
            }
            else
            {
                Console.WriteLine($"{joueurName} is not the owner to start a game");
            }
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
                    if (partie.Joueurs.Count <= 0)
                    {
                        foreach (Partie p in JurasicRiskGameServer.Get.Parties)
                        {
                            if (p.Id == partie.Id)
                            {
                                JurasicRiskGameServer.Get.Parties.Remove(p);
                                break;
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
