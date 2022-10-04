using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Compte
    {
        public Compte(string pseudo, string password)
        {
            Pseudo = pseudo;
            Password = password;
        }

        public string Pseudo { get; set; }
        public string Password { get; set; }
    }
}
