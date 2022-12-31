
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Stockage
{
    public class ChargerCollection : ICharge
    {
        private string _path;

        /// <summary>
        /// Charger un object 
        /// </summary>
        /// <param name="path">Chemin du fichier</param>
        /// <Author>Charif</Author>
        public ChargerCollection(string path)
        {
            this._path = path;
        }

        private static readonly JsonSerializerSettings _options = new() { TypeNameHandling = TypeNameHandling.Auto, NullValueHandling = NullValueHandling.Ignore };

        /// <summary>
        /// Charger un fichier
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="FileName">nom du fichier .json</param>
        /// <returns>Data Cast in Generic Type</returns>
        /// <Author>Charif</Author>
        public T Charger<T>(string FileName)
        {


            T d2 = default;
            if (File.Exists(Path.Combine(_path, $"{FileName}.json")))
            {
                string path = Path.Combine(_path, $"{FileName}.json");

                // read file into a string and deserialize JSON to a type
                d2 = JsonConvert.DeserializeObject<T>(File.ReadAllText(path), _options);

            }
            return d2;
        }
    }
}