using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace JurassicRisk.Ressources
{
    public static class GetResource
    {
        public static List<String> GetResourceFileName(string PathContain)
        {
            List<String> filesNames = new List<String>();
            Assembly resourceAssembly = Assembly.GetExecutingAssembly();
            String[] manifests = resourceAssembly.GetManifestResourceNames();
            using (ResourceReader reader = new ResourceReader(resourceAssembly.GetManifestResourceStream(manifests[0])))
            {
                System.Collections.IDictionaryEnumerator dict = reader.GetEnumerator();

                while (dict.MoveNext())
                {
                    if (dict.Key.ToString().Contains(PathContain))
                    {
                        filesNames.Add((dict.Key as String).Remove(0, 14));
                    }
                }
            }

            filesNames.Sort();

            return filesNames;
        }
    }
}
