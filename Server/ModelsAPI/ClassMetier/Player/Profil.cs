using System.Text.Json.Serialization;
using Redis.OM.Modeling;

namespace ModelsAPI.ClassMetier.Player
{
    [Document(StorageType = StorageType.Json, Prefixes = new[] { "Profil" })]
    public class Profil
    {
        /// <summary>
        /// pseudo of the profil
        /// </summary>
        [RedisIdField]
        public string Pseudo { get; set; }

        /// <summary>
        /// Password of the profil
        /// </summary>
        [Indexed]
        public string Password { get; set; }

        /// <summary>
        /// ConnectionId SignalR
        /// </summary>
        [Indexed]
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