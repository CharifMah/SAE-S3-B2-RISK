using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Fight
{
    public class Des
    {
        private List<int> faces = new List<int>();

        public List<int> Faces { get => faces; set => faces = value; }

        public Des(int nbFaces)
        {
            for (int i = 1; i < nbFaces; i++)
            {
                faces.Add(i);
            }
        }

        public int Roll()
        {
            Random random = new Random();
            return Faces[random.Next(5)];
        }
    }
}
