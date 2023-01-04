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
        #region Attributes
        private string _pseudo;
        private string _password;
        private string _connectionId;
        #endregion

        #region Property

        /// <summary>
        /// pseudo of the profil
        /// </summary>
        public string Pseudo { get => _pseudo; set => _pseudo = value; }

        /// <summary>
        /// Password of the profil
        /// </summary>
        public string Password { get => _password; set => _password = value; }

        /// <summary>
        /// ConnectionId SignalR
        /// </summary>
        public string ConnectionId { get => _connectionId; set => _connectionId = value; }

        #endregion

        /// <summary>
        /// create a profil
        /// </summary>
        /// <param name="login">of the profil</param>
        public Profil(string Pseudo, string Password)
        {
            this._pseudo = Pseudo;
            this._password = Password;
        }
    }
}
