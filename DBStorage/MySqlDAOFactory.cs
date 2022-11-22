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

        public static MySqlDAOFactory getInstance()
        {
            if (Instance == null)
            {
                Instance = new MySqlDAOFactory();
            }
            return Instance;
        }
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
