using Models.Graph.distances;

namespace Models.Graph.lieux
{
    ///<summary>Tour (ensemble ordonné de lieu parcouru dans l'ordre avec retour au point de départ)</summary>
    public class Tournee
    {
        /// <summary>Liste des lieux dans l'ordre de visite</summary>
        private List<Lieu> listeLieux;
        public List<Lieu> ListeLieux
        {
            get => listeLieux;
            set => listeLieux = value;
        }

        /// <summary>Constructeur par défaut</summary>
        public Tournee()
        {
            listeLieux = new List<Lieu>();
        }

        /// <summary>Constructeur par copie</summary>
        public Tournee(Tournee modele)
        {
            listeLieux = new List<Lieu>(modele.listeLieux);
        }

        /// <summary>
        /// Ajoute un lieu à la tournée (fin)
        /// </summary>
        /// <param name="lieu">Le lieu à ajouter</param>
        public void Add(Lieu lieu)
        {
            ListeLieux.Add(lieu);
        }

        /// <summary>Distance totale de la tournée</summary>
        public int Distance
        {
            get
            {
                int res = 0;
                for (int i = 0; i < listeLieux.Count; i++)
                {
                    res += FloydWarshall.Distance(listeLieux[i], listeLieux[(i + 1) % listeLieux.Count]);
                }
                return res;
            }
        }

        public override string ToString()
        {
            string res = listeLieux[0].Nom;

            for (int i = 1; i < listeLieux.Count; i++)
            {
                res += " => " + listeLieux[i].Nom;
            }
            res += " => " + listeLieux[0].Nom;
            return res;
        }
    }
}
