using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Profil 
    {
        public Profil(string pseudo)
        {
            Pseudo = pseudo;
        }

        public string Pseudo { get; set; }
    }
}
