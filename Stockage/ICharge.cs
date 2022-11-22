using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockage
{
    /// <summary>
    /// Interface sur un chargeur de dictionnaire
    /// </summary>
    public interface ICharge
    {
        /// <summary>
        /// Charge le dictionnaire
        /// </summary>
        /// <returns>le dictionnaire chargé</returns>
        NameValueCollection Charger();
    }
}
