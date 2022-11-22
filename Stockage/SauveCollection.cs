using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockage
{
    public class SauveCollection : ISauve
    {
        private string _path;

        public SauveCollection(string path)
        {
            this._path = path;
        }

        public void Sauver(NameValueCollection collection)
        {
            throw new NotImplementedException();
        }
    }
}
