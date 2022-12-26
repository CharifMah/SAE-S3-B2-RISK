using DBStorage.DAO;

namespace DBStorage.DAOFactory
{
    public class MySqlDAOFactory : DAOFactory
    {
        private static MySqlDAOFactory Instance;
        private MySqlDAOFactory() { }

        /// <summary>
        /// get the instance of the factory
        /// </summary>
        /// <returns>the instance</returns>
        public static MySqlDAOFactory Get()
        {
            if (Instance == null)
            {
                Instance = new MySqlDAOFactory();
            }
            return Instance;
        }

        /// <summary>
        /// create a new ProfilMySqlDAO
        /// </summary>
        /// <returns>the new ProfilMySqlDAO</returns>
        public ProfilDAO CreerProfil()
        {
            try
            {
                return new ProfilMySqlDAO();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// create a new CarteMySqlDAO
        /// </summary>
        /// <returns>the new CarteMySqlDAO</returns>
        public CarteDAO CreateCarteDAO()
        {
            try
            {
                return new CarteMySqlDAO();
            }
            catch (IOException e)
            {
                return null;
            }
        }
    }
}
