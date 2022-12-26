using System.Text.Json.Serialization;
using Redis.OM.Modeling;

namespace ModelsAPI.ClassMetier
{
    [Document(StorageType = StorageType.Json, Prefixes = new[] { "Profil" })]
    public class Profil
    {
        /// <summary>
        /// id of the profil
        /// </summary>
        [RedisIdField]
        [Indexed]
        public string Id { get; set; }

        /// <summary>
        /// pseudo of the profil
        /// </summary>
        [Indexed]
        public string Pseudo { get; set; }

        /// <summary>
        /// Password of the profil
        /// </summary>
        [Indexed]
        public string Password { get; set; }

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