using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stockage
{
    public static class SoundStore
    {
        private static Dictionary<string, Sound> sounds = new Dictionary<string, Sound>();

        public static void LoadSounds(string folder)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), folder);
            if (!Directory.Exists(path))
                return;
            foreach (string file in Directory.GetFiles(path))
            {
                string fileName = Path.GetFileName(file);
                sounds[fileName] = new Sound(file);
            }
        }

        public static Sound Get(string name) => sounds.ContainsKey(name) ? sounds[name] : throw new ArgumentException();
    }
}
