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
                        filesNames.Add((dict.Key as string).Remove(0, 14));
                    }
                }
            }

            filesNames.Sort();

            return filesNames;
        }
    }
}
