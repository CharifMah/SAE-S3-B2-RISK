
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