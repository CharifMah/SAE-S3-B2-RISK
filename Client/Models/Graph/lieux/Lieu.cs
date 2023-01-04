using Models.Graph.distances;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Models.Graph.lieux
{
    /// <summary> Lieu (magasin ou usine) </summary>
    public class Lieu
    {
        /// <summary>Type du lieu (Magasin ou usine)</summary>
        private TypeLieu type;
        public TypeLieu Type => type;

        /// <summary>Nom du lieu (clé primaire)</summary>
        private string nom;
        public string Nom => nom;

        /// <summary>Abscisse du lieu</summary>
        private int x;
        public int X => x;

        /// <summary>Ordonnée du lieu</summary>
        private int y;
        public int Y => y;

        /// <summary>Test si le lieu est visiter</summary>
        private bool visited = false;
        public bool IsVisited
        {
            get => visited;
            set => visited = value;
        }


        /// <summary>Constructeur par défaut</summary>
        /// <param name="type">Type du lieu (Magasin ou usine)</param>
        /// <param name="nom">Nom du lieu</param>
        /// <param name="x">Absisse du lieu</param>
        /// <param name="y">Ordonnée du lieu</param>
        public Lieu(TypeLieu type, string nom, int x, int y)
        {
            this.type = type;
            this.nom = nom;
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Distance d’un lieu L à un couple de lieu (A,B)
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public int DistLieuCouple(Lieu A, Lieu B)
        {
            int dist = 0;

            dist = FloydWarshall.Distance(A, this) + FloydWarshall.Distance(this, B) - FloydWarshall.Distance(A, B);

            return dist;
        }

        /// <summary>
        /// Distance d’un lieu L à une tournée T
        /// </summary>
        /// <param name="listlieux"></param>
        /// <returns></returns>
        public void DistLieuTournee(Tournee T, ref int dist, ref int IndexInsertion)
        {
            dist = -1;
            IndexInsertion = -1;
            for (int i = 0; i < T.ListeLieux.Count; i++)
            {
                if (dist == -1 && IndexInsertion == -1 || DistLieuCouple(T.ListeLieux[i], T.ListeLieux[(i + 1) % T.ListeLieux.Count]) <= dist)
                {
                    dist = DistLieuCouple(T.ListeLieux[i], T.ListeLieux[(i + 1) % T.ListeLieux.Count]);
                    IndexInsertion = i;
                }
            }
        }

        /// <summary>
        /// Trouve le lieux le plus proche dans une liste de lieux depuis une position de depart
        /// </summary>
        public Lieu LieuxProche(List<Lieu> listelieux)
        {
            Lieu lieuxleplusproche = this;
            int mindist = -1; //Initialise la distance minimum
            int dist = -1;


            foreach (Lieu lieu in listelieux.Where(l => !l.IsVisited))
            {
                dist = FloydWarshall.Distance(this, lieu);

                if (mindist == -1 || dist <= mindist)
                {
                    mindist = dist;
                    lieuxleplusproche = lieu;
                }
            }
            return lieuxleplusproche;
        }

        public override string ToString()
        {
            return Nom;
        }
    }
}
