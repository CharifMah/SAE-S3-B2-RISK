using Models.Map;
using Stockage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using Carte = Models.Map.Carte;
using Continent = Models.Map.Continent;
using GetResource = JurassicRisk.Ressources.GetResource;
using ITerritoireBase = Models.Map.ITerritoireBase;

namespace JurassicRisk
{
    /// <summary>
    /// Save the Map in Json file
    /// </summary>
    /// <Author>Charif</Author>
    public class SaveMap
    {
        private List<string> _fileEntries;
        private Dictionary<string, ITerritoireBase> _decorations;
        private int i = 0;
        /// <summary>
        /// Save the carte
        /// </summary>
        /// <param name="carte">null if we don't have map</param>
        public SaveMap(Carte carte)
        {
            SaveCarte(carte);
        }

        private Carte CreateCarte(Dictionary<string, ITerritoireBase> _decorations)
        {
            List<IContinent> _continents = new List<IContinent>
            {
                new Continent(_decorations.Take(7).ToDictionary(x => x.Key, y => y.Value)),
                new Continent(_decorations.Skip(7).Take(7).ToDictionary(x => x.Key, y => y.Value)),
                new Continent(_decorations.Skip(14).Take(8).ToDictionary(x => x.Key, y => y.Value)),
                new Continent(_decorations.Skip(22).Take(7).ToDictionary(x => x.Key, y => y.Value)),
                new Continent(_decorations.Skip(29).Take(5).ToDictionary(x => x.Key, y => y.Value)),
                new Continent(_decorations.Skip(34).Take(7).ToDictionary(x => x.Key, y => y.Value))
            };
            Dictionary<string, IContinent> dic = new Dictionary<string, IContinent>();
            for (int i = 0; i < _continents.Count; i++)
            {
                dic.Add(i.ToString(), _continents[i]);
            }


            return new Carte(dic,null);
        }

        /// <summary>
        /// Save the map into list of territoire decorator
        /// </summary>
        private void SaveCarte(Carte carte)
        {
            if (carte == null)
            {
                _decorations = new Dictionary<string, ITerritoireBase>();
                _fileEntries = GetResource.GetResourceFileName("carte2/");
                foreach (string fileName in _fileEntries)
                {
                    SerializeConf($"pack://application:,,,/Sprites/Carte2/{fileName.Substring(fileName.Length - 8)}"); 
                }

                SauveCollection s = new SauveCollection(Environment.CurrentDirectory);

                s.Sauver(CreateCarte(_decorations), "Map/Cartee");

            }
            else
            {
                SauveCollection s = new SauveCollection(Environment.CurrentDirectory);
                s.Sauver(carte, "Cartee");
            }
        }

        public void SerializeConf(string Urisource)
        {
            _decorations.Add(i.ToString(), new TerritoireDecorator(new Models.Map.TerritoireBase(i),Urisource));
            i++;
        }
    }
}
