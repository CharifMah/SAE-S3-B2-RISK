
using Newtonsoft.Json;

namespace Stockage
{
    public class SauveCollection : ISauve
    {
        private string _path;

        /// <summary>
        /// Sauvgarde
        /// </summary>
        /// <param name="path">Directory Path</param>
        public SauveCollection(string path)
        {
            this._path = path;
        }
        private static readonly JsonSerializerSettings _options = new() { TypeNameHandling = TypeNameHandling.Auto, NullValueHandling = NullValueHandling.Ignore };
        /// <summary>
        /// Crée un fichier Json avec les Settings
        /// </summary>
        /// <param name="T">data a sauvgarde</param>
        /// <Author>Charif</Author>
        public void Sauver<T>(T data, string FileName)
        {
            if (Directory.Exists(_path))
            {
                if (File.Exists(Path.Combine(_path, $"{FileName}.json")))
                {
                    File.Delete(Path.Combine(_path, $"{FileName}.json"));
                }

                var jsonString = JsonConvert.SerializeObject(data, _options);
                File.WriteAllText(Path.Combine(_path, $"{FileName}.json"), jsonString);
            }
        }
    }
}
