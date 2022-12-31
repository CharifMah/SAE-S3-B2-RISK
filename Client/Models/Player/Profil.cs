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
        /// pseudo of the profil
        /// </summary>
        public string Pseudo { get; set; }

        /// <summary>
        /// Password of the profil
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// ConnectionId SignalR
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// create a profil
        /// </summary>
        /// <param name="login">of the profil</param>
        public Profil(string Pseudo, string Password)
        {
            this.Pseudo = Pseudo;
            this.Password = Password;
        }
    }
}
