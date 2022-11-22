namespace DBStorage.ClassMetier
{
    public class Profil
    {
        /// <summary>
        /// id of the profil
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// pseudo of the profil
        /// </summary>
        public string Pseudo { get ; set; }

        /// <summary>
        /// Password of the profil
        /// </summary>
        public string Mdp { get; set; }

        /// <summary>
        /// create a profil
        /// </summary>
        /// <param name="login">of the profil</param>
        public Profil(string login) 
        {
            Pseudo = login; 
        }

    }
}