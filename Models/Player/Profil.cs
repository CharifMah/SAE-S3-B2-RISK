using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Player
{
    public class Profil
    {
        /// <summary>
        /// Crée un nouveau profil
        /// </summary>
        /// <param name="pseudo"></param>
        public Profil(string pseudo, string password)
        {
            Pseudo = pseudo;
            Password = password;
        }

        public int Id { get; set; }
        public string Pseudo { get; set; }
        public string Password { get; set; }
    }
}
