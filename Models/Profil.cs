using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Profil 
    {
        /// <summary>
        /// Crée un nouveau profil
        /// </summary>
        /// <param name="pseudo"></param>
        public Profil(string pseudo)
        {
            Pseudo = pseudo;
            Pseudo1 = pseudo + "1";
            Pseudo2 = pseudo + "2";
            Pseudo3 = pseudo +"3";
        }

        public string Pseudo { get; set; }
        public string Pseudo1 { get; set; }
        public string Pseudo2 { get; set; }
        public string Pseudo3 { get; set; }
    }
}
