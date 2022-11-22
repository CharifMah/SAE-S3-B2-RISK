using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockage
{
    public interface ISauve
    {
        /// <summary>
        /// Sauvagarde le dictionnaire
        /// </summary>
        void Sauver<T>(T data, string FileName);

    }
}
