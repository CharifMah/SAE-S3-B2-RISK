using Models.Map;
using Stockage;
using System;
using System.Collections.Generic;
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
        private ITerritoireBase[] _decorations;
        private int i = 0;

        /// <summary>
        /// Save the carte
        /// </summary>
        /// <param name="carte">null if we don't have map</param>
        public SaveMap(Carte? carte = null)
        {
            SaveCarte(carte);
        }

        private Carte CreateCarte1(ITerritoireBase[] _decorations)
        {
            IContinent[] _continents = new IContinent[6]
            {
                new Continent(_decorations[0..7]),
                new Continent(_decorations[7..14]),
                new Continent(_decorations[14..22]),
                new Continent(_decorations[22..29]),
                new Continent(_decorations[29..34]),
                new Continent(_decorations[34..41])
            };

            return new Carte(_continents, null);
        }

        /// <summary>
        /// Save the map into list of territoire decorator
        /// </summary>
        private void SaveCarte(Carte? carte)
        {
            if (carte == null)
            {
                _decorations = new ITerritoireBase[41];
                _fileEntries = GetResource.GetResourceFileName("carte2/");
                foreach (string fileName in _fileEntries)
                {
                    SerializeConf($"pack://application:,,,/Sprites/Carte2/{fileName}");
                }

                SauveCollection s = new SauveCollection(Environment.CurrentDirectory);

                s.Sauver(CreateCarte1(_decorations), "Map/Cartee");

            }
            else
            {
                SauveCollection s = new SauveCollection(Environment.CurrentDirectory);
                s.Sauver(carte, "Cartee");
            }
        }

        public void SerializeConf(string Urisource)
        {

            _decorations[i] = new TerritoireDecorator(new Models.Map.TerritoireBase(i), Urisource);
            i++;
        }
    }
}
