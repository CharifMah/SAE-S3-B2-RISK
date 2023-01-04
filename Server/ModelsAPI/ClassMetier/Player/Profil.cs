using System.Text.Json.Serialization;
using Redis.OM.Modeling;

namespace ModelsAPI.ClassMetier.Player
{
    [Document(StorageType = StorageType.Json, Prefixes = new[] { "Profil" })]
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
        [RedisIdField]
        public string Pseudo { get => _pseudo; set => _pseudo = value; }

        /// <summary>
        /// Password of the profil
        /// </summary>
        [Indexed]
        public string Password { get => _password; set => _password = value; }

        /// <summary>
        /// ConnectionId SignalR
        /// </summary>
        [Indexed]
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