namespace DBStorage.ClassMetier
{
    public class Profil
    {
        public string Pseudo { get ; set; }

        public Profil(string login) 
        {
            Pseudo = login; 
        }

    }
}