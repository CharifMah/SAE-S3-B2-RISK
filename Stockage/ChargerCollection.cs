using System.Collections.Specialized;
using static System.Formats.Asn1.AsnWriter;
using System.Runtime.Serialization;

namespace Stockage
{
    public class ChargerCollection : ICharge
    {
        private string _path;

        public ChargerCollection(string path)
        {
            this._path = path;
        }

        public NameValueCollection Charger(string FileName)
        {
            NameValueCollection d2 = null;
            if (File.Exists(Path.Combine(_path, $"{FileName}.json")))
            {
                using (FileStream stream = File.OpenRead(Path.Combine(_path, $"{FileName}.json")))
                {
                    DataContractSerializer ser = new DataContractSerializer(typeof(NameValueCollection));
                    d2 = ser.ReadObject(stream) as NameValueCollection;
                }

            }
            return d2;
        }
    }
}