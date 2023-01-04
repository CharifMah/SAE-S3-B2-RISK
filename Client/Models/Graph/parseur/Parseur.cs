using Models.Graph.lieux;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Models.Graph.parseur
{
    /// <summary>Parseur de fichier de graphe</summary>
    public class Parseur
    {
        /// <summary>Propriétés nécessaires</summary>
        private string adresseFichier;
        private Dictionary<string, Lieu> listeLieux;
        public Dictionary<string, Lieu> ListeLieux => listeLieux;
        private List<Route> listeRoutes;
        public List<Route> ListeRoutes => listeRoutes;

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="nomDuFichier">Nom du fichier à parser</param>
        public Parseur(string nomDuFichier)
        {
            listeLieux = new Dictionary<string, Lieu>();
            listeRoutes = new List<Route>();
            adresseFichier = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\ressources\" + nomDuFichier;
        }

        /// <summary>
        /// Parsage du fichier
        /// </summary>
        public void Parser()
        {
            //Ouverture du fichier
            using (StreamReader stream = new StreamReader(adresseFichier))
            {
                string ligne;
                while ((ligne = stream.ReadLine()) != null)
                {
                    //Lecture d'une ligne
                    string[] morceaux = ligne.Split(' ');
                    if (morceaux[0] == "ROUTE")
                    {
                        Route route = MonteurRoute.Creer(morceaux, listeLieux);
                        listeRoutes.Add(route);
                    }
                    else
                    {
                        Lieu lieu = MonteurLieu.Creer(morceaux);
                        listeLieux.Add(morceaux[1], lieu);
                    }
                }
            }
        }
    }
}
