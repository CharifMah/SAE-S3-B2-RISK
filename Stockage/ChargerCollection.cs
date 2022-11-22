using System.Collections.Specialized;

namespace Stockage
{
    public class ChargerCollection : ICharge
    {
        private string _path;

        public ChargerCollection(string path)
        {
            this._path = path;
        }

        public NameValueCollection Charger()
        {
            throw new NotImplementedException();
        }
    }
}