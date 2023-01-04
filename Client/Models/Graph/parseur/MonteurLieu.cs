using Models.Graph.lieux;
using System;

namespace Models.Graph.parseur
{
    /// <summary>Monteur de lieu </summary>
    public class MonteurLieu
    {
        /// <summary>
        /// Crée un lieu à partir du tableau de string obtenu en lisant le fichier 
        /// </summary>
        /// <param name="morceaux">Les 4 morceaux de la ligne correspondant à la ligne</param>
        /// <returns>Le lieu créé</returns>
        public static Lieu Creer(string[] morceaux)
        {
            Lieu res = null;

            switch (morceaux[0])
            {
                case "MAGASIN":
                    res = new Lieu(TypeLieu.MAGASIN, morceaux[1], int.Parse(morceaux[2]), int.Parse(morceaux[3]));
                    break;
                case "USINE":
                    res = new Lieu(TypeLieu.USINE, morceaux[1], int.Parse(morceaux[2]), int.Parse(morceaux[3]));
                    break;
            }
            return res;
        }
    }
}
