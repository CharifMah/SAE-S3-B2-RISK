using DBStorage.ClassMetier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBStorage
{
    public class MySqlDAOFactory : DAOFactory
    {
        private static MySqlDAOFactory Instance;
        private MySqlDAOFactory() { }

        /// <summary>
        /// get the instance of the factory
        /// </summary>
        /// <returns>the instance</returns>
        public static MySqlDAOFactory GetInstance()
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
            catch(IOException e) 
            {
                return null;
            }
        }
    }
}
