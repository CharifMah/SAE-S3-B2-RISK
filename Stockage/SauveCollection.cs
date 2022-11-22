using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
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

        /// <summary>
        /// Crée un fichier Json avec les Settings
        /// </summary>
        /// <param name="NameValueCollection">data a sauvgarde</param>
        /// <Author>Charif</Author>
        public void Sauver(NameValueCollection data,string FileName)
        {
            if (Directory.Exists(_path))
            {
                if (File.Exists(Path.Combine(_path, $"{FileName}.json")))
                {
                    File.Delete(Path.Combine(_path, $"{FileName}.json"));
                }
                using (FileStream stream = File.OpenWrite(Path.Combine(_path,  $"{FileName}.json")))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(NameValueCollection));
                    ser.WriteObject(stream, data);
                }
            }
        }
    }
}
