using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBStorage.DAO;

namespace DBStorage.DAOFactory
{
    public interface DAOFactory
    {
        /// <summary>
        /// create the good factory
        /// </summary>
        /// <returns>factory of type asked</returns>
        ProfilDAO CreerProfil();

        /// <summary>
        /// create the good factory
        /// </summary>
        /// <returns>factory of type asked</returns>
        CarteDAO CreateCarteDAO();
    }
}
