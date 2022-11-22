namespace DBStorage.ClassMetier
{
    public class Profil
    {
        public int Id { get; set; }

        public string Pseudo { get ; set; }

        public string Mdp { get; set; }

        public Profil(string login) 
        {
            Pseudo = login; 
        }

    }
}