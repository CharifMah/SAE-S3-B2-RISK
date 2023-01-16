using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace JurassicRisk.Ressources
{
    public static class GetResource
    {
        public static List<string> GetResourceFileName(string PathContain)
        {
            List<string> filesNames = new List<string>();
            Assembly resourceAssembly = Assembly.GetExecutingAssembly();
            string[] manifests = resourceAssembly.GetManifestResourceNames();
            using (ResourceReader reader = new ResourceReader(resourceAssembly.GetManifestResourceStream(manifests[0])))
            {
                System.Collections.IDictionaryEnumerator dict = reader.GetEnumerator();

                while (dict.MoveNext())
                {
                    if (dict.Key.ToString().Contains(PathContain))
                    {
                        string key = (dict.Key as string);
                        filesNames.Add(key.Substring(key.Length - 8));
                    }
                }
            }

            filesNames.Sort();

            return filesNames;
        }
    }
}
