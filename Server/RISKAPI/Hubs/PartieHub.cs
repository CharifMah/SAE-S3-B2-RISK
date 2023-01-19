using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using ModelsAPI;
using ModelsAPI.ClassMetier.GameStatus;
using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Player;
using ModelsAPI.ClassMetier.Units;
using Newtonsoft.Json;
using Redis.OM;

namespace RISKAPI.Hubs
{
    [HubName("PartieHub")]
    public class PartieHub : Hub
    {
        public PartieHub()
        {

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
                int index = partie.NextPlayer();
                Joueur joueur = partie.Joueurs[index];
                partie.Transition();
                string etatJson = JsonConvert.SerializeObject(partie.Etat);
                await Clients.Client(joueur.Profil.ConnectionId).SendAsync("yourTurn", etatJson, partie.Etat.ToString(), index);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"c'est au tour de {joueur.Profil.Pseudo}");
                Console.ForegroundColor = ConsoleColor.White;

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
        public async Task Action(string partieName, List<int> unitlist, string joueurName)
        {
            Partie p = JurasicRiskGameServer.Get.Parties.First(l => l.Id == partieName);
            if (p.Joueurs[p.PlayerIndex].Profil.Pseudo == joueurName)
            {
                if (p.Action(unitlist))
                {
                    switch (p.Etat.ToString())
                    {
                        case "Deploiment":
                            Deploiment d = (p.Etat as Deploiment);
                            if (p.PlayerIndex != -1)
                            {

                                await Clients.Group(partieName).SendAsync("deploiment", d.IdUniteRemove, d.IdTerritoireUpdate, p.PlayerIndex);
                                await Clients.Group(partieName).SendAsync("endTurn", p.NextPlayer());

                                Console.WriteLine($"Deployement Update from {Context.ConnectionId}");
                            }
                            break;
                        case "Renforcement":
                            Renforcement r = (p.Etat as Renforcement);
                            if (p.PlayerIndex != -1)
                            {
                                await Clients.Group(partieName).SendAsync("renforcement", r.IdUniteRemove, r.IdTerritoireUpdate, p.PlayerIndex);

                                Console.WriteLine($"Deployement Update from {Context.ConnectionId}");
                            }

                            break;
                    }
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
            p.Carte.SelectedTerritoire = (TerritoireBase?)p.Carte.GetTerritoire(ID);
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
            Console.WriteLine(joueurName + " start connect ");
            Partie? partie = JurasicRiskGameServer.Get.Parties.FirstOrDefault(p => p.Id == partieName);
            if (partie != null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, partieName);
                Joueur joueur = partie.Joueurs.FirstOrDefault(j => j.Profil.Pseudo == joueurName);

                if (joueur != null)
                {
                    //Important
                    joueur.Profil.ConnectionId = Context.ConnectionId;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{joueurName} connected to {partieName}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Not Connected");
                Console.ForegroundColor = ConsoleColor.White;
            }



        }

        /// <summary>
        /// Lance la partie pour tout les joueurs
        /// </summary>
        /// <param name="partieName">nom de la partie</param>
        /// <param name="joueurName">nom du joueur qui fait l'action</param>
        /// <param name="carteName">nom de la Carte</param>
        /// <returns>Task</returns>
        public async Task StartPartie(string partieName, string joueurName, string carteName)
        {
            Partie partie = JurasicRiskGameServer.Get.Parties.FirstOrDefault(p => p.Id == partieName);
            Lobby lobby = JurasicRiskGameServer.Get.Lobbys.FirstOrDefault(l => l.Id == partieName);
            string joueursJson = String.Empty;
            string etatJson = "";
            string unitsPlayersJson = "";
            if (partie != null && partie.Owner == null)
            {
                partie.Owner = joueurName;
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, partieName);
            //Lance la partie si c'est le Owner qui fait l'action Play
            if (partie != null && partie.Owner == joueurName)
            {
                Console.WriteLine($"{joueurName} try to Start the game");
                Carte carte = CreateCarte1();
                partie.Carte = carte;
                Console.WriteLine("Carte Created");

                //Envoie de la partie Lancement pour tout les joueurs
                if (partie.Joueurs.Count > 0 || (partie.Joueurs.Count > 0 && lobby.Joueurs.Count > 0))
                {
                    Console.WriteLine("Serialize Object");
                    foreach (Joueur j in lobby.Joueurs)
                    {
                        partie.JoinPartie(j);
                    }

                    joueursJson = JsonConvert.SerializeObject(partie.Joueurs);
                    etatJson = JsonConvert.SerializeObject(partie.Etat);

                    List<Joueur?> list = JsonConvert.DeserializeObject<List<Joueur?>>(joueursJson);

                    int index = partie.NextPlayer();

                    await Clients.Group(partieName).SendAsync("ReceivePartie", joueursJson, partieName, etatJson, index);
                    
                    Console.WriteLine("Succeffully SendPartie to groupe " + partieName);

                    await Clients.Group(partieName).SendAsync("yourTurn", etatJson, partie.Etat.ToString(), index);
                    Console.WriteLine($"Partie avec {partie.Joueurs.Count} players Crée");
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
                    foreach (Partie p in JurasicRiskGameServer.Get.Parties)
                    {
                        if (partie.Joueurs.Count <= 0)
                        {
                            List<Partie> l = JurasicRiskGameServer.Get.Parties;
                            Console.WriteLine($"Removed empty partie {p.Id}");
                            l.Remove(p);


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
            ITerritoireBase[] territoires = new ITerritoireBase[41];
            for (int i = 0; i < territoires.Length; i++)
            {
                territoires[i] = new TerritoireBase(i, null);
            }
            IContinent[] _continents = new IContinent[6]
            {
                 //Ajout de 7 territoires au premier continent en utilisant les index 0 à 6 du dictionnaire "territoires"
                new Continent(territoires[0..7]),
                //Ajout de 7 territoires au deuxième continent en utilisant les index 7 à 13 du dictionnaire "territoires"
                new Continent(territoires[7..14]),
                //Ajout de 8 territoires au troisième continent en utilisant les index 14 à 21 du dictionnaire "territoires"
                new Continent(territoires[14..22]),
                //Ajout de 7 territoires au quatrième continent en utilisant les index 22 à 28 du dictionnaire "territoires"
                new Continent(territoires[22..29]),
                //Ajout de 5 territoires au cinquième continent en utilisant les index 29 à 33 du dictionnaire "territoires"
                new Continent(territoires[29..34]),
                //Ajout de 7 territoires au sixième continent en utilisant les index 34 à 40"territoires"
                new Continent(territoires[34..41])
            };

            return new Carte(_continents, null);
        }

        #endregion


    }
}
