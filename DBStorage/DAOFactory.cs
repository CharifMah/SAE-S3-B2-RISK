using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBStorage
{
    public interface DAOFactory
    {
        /// <summary>
        /// create the good factory
        /// </summary>
        /// <returns>factory of type asked</returns>
        ProfilDAO CreerProfil();
    }
}
