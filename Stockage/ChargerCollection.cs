
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Stockage
{
    public class ChargerCollection : ICharge
    {
        private string _path;

        public ChargerCollection(string path)
        {
            this._path = path;
        }

        public T Charger<T>(string FileName)
        {
            T d2 = default;
            if (File.Exists(Path.Combine(_path, $"{FileName}.json")))
            {
                using (FileStream stream = File.OpenRead(Path.Combine(_path, $"{FileName}.json")))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                    d2 = (T?)ser.ReadObject(stream);
                }

            }
            return d2;
        }
    }
}