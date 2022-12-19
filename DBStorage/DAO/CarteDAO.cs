using ModelsAPI.ClassMetier.Map;

namespace DBStorage.DAO
{
    public interface CarteDAO
    {
        public void Insert(Carte carte);
        public void Update(Carte carte);
        public string Get();
    }
}
