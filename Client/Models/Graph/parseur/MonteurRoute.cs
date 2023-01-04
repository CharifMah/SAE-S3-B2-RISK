using Models.Graph.lieux;
using System;
using System.Collections.Generic;

namespace Models.Graph.parseur
{
    /// <summary>Monteur des routes</summary>
    public class MonteurRoute
    {
        /// <summary>
        /// Crée une route à partir du tableau de string obtenu en lisant le fichier et de la liste des lieux
        /// </summary>
        /// <param name="morceaux">Les 4 morceaux de la ligne correspondant à la ligne</param>
        /// <param name="listLieux">Liste des lieux indexés par leur nom</param>
        /// <returns>La route créé</returns>
        public static Route Creer(string[] morceaux, Dictionary<string, Lieu> listLieux)
        {
            Route res = null;

            Lieu depart = listLieux[morceaux[1]];
            Lieu arriver = listLieux[morceaux[2]];

            res = new Route(depart, arriver, int.Parse(morceaux[3]));

            return res;
        }
    }
}
