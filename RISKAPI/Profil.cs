namespace RISKAPI
{
    public class Profil
    {
        public string Pseudo { get ; set; }

        public Profil()
        {
        }

        public Profil(string login) 
        {
            Pseudo = login; 
        }

    }
}