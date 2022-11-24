using Models.Map;
using Stockage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace JurassicRisk.ViewsModels
{
    /// <summary>
    /// Save the Map in Json file
    /// </summary>
    /// <Author>Charif</Author>
    public class SaveMap
    {
        private List<string> _fileEntries;
        private List<ITerritoireBase> _decorations;
        private int i = 0;
        public SaveMap(Carte carte)
        {
            SaveCarte(carte);
        }

        /// <summary>
        /// Get image Territoire from ressource 
        /// </summary>
        /// <returns>List<BitmapImage></returns>
        /// <Author>Charif</Author>
        private List<BitmapImage> GetImagesTerritoires()
        {
            List<BitmapImage> _sprites = new List<BitmapImage>();
            _fileEntries = GetResource.GetResourceFileName("carte/");
            foreach (string fileName in _fileEntries)
            {
                BitmapImage theImage = new BitmapImage(new Uri($"pack://application:,,,/Sprites/Carte/{fileName}"));
                _sprites.Add(theImage);
            }
            return _sprites;
        }

        private Carte CreateCarte(List<TerritoireDecorator> _decorations)
        {
            List<Continent> _continents = new List<Continent>();

            _continents.Add(new Continent(_decorations.ToList<ITerritoireBase>().Take(7).ToList()));
            _continents.Add(new Continent(_decorations.ToList<ITerritoireBase>().Skip(7).Take(7).ToList()));
            _continents.Add(new Continent(_decorations.ToList<ITerritoireBase>().Skip(14).Take(8).ToList()));
            _continents.Add(new Continent(_decorations.ToList<ITerritoireBase>().Skip(22).Take(7).ToList()));
            _continents.Add(new Continent(_decorations.ToList<ITerritoireBase>().Skip(29).Take(5).ToList()));
            _continents.Add(new Continent(_decorations.ToList<ITerritoireBase>().Skip(34).Take(7).ToList()));

            return new Carte(_continents);
        }

        /// <summary>
        /// Save the map into list of territoire decorator
        /// </summary>
        private void SaveCarte(Carte carte)
        {
            if (carte == null)
            {
                _decorations = new List<ITerritoireBase>();

                SerializeConf(GetImagesTerritoires()[0].UriSource.ToString(), 94, 51, 152, 70);
                SerializeConf(GetImagesTerritoires()[1].UriSource.ToString(), 114, 149, 97, 130);
                SerializeConf(GetImagesTerritoires()[2].UriSource.ToString(), 148, 1, 160, 194);
                SerializeConf(GetImagesTerritoires()[3].UriSource.ToString(), 268, 32, 90, 146);
                SerializeConf(GetImagesTerritoires()[4].UriSource.ToString(), 332, 38, 182, 150);
                SerializeConf(GetImagesTerritoires()[5].UriSource.ToString(), 222, 114, 146, 138);
                SerializeConf(GetImagesTerritoires()[6].UriSource.ToString(), 290, 157, 151, 220);
                SerializeConf(GetImagesTerritoires()[7].UriSource.ToString(), 112, 580, 188, 217);
                SerializeConf(GetImagesTerritoires()[8].UriSource.ToString(), 107, 735, 226, 158);
                SerializeConf(GetImagesTerritoires()[9].UriSource.ToString(), 163, 807, 165, 188);
                SerializeConf(GetImagesTerritoires()[10].UriSource.ToString(), 249, 637, 182, 161);
                SerializeConf(GetImagesTerritoires()[11].UriSource.ToString(), 388, 620, 215, 161);
                SerializeConf(GetImagesTerritoires()[12].UriSource.ToString(), 296, 735, 302, 146);
                SerializeConf(GetImagesTerritoires()[13].UriSource.ToString(), 400, 751, 250, 215);
                SerializeConf(GetImagesTerritoires()[14].UriSource.ToString(), 510, 424, 177, 188);
                SerializeConf(GetImagesTerritoires()[15].UriSource.ToString(), 641, 484, 168, 266);
                SerializeConf(GetImagesTerritoires()[16].UriSource.ToString(), 745, 332, 184, 162);
                SerializeConf(GetImagesTerritoires()[17].UriSource.ToString(), 635, 302, 193, 139);
                SerializeConf(GetImagesTerritoires()[18].UriSource.ToString(), 617, 127, 201, 238);
                SerializeConf(GetImagesTerritoires()[19].UriSource.ToString(), 728, 184, 214, 244);
                SerializeConf(GetImagesTerritoires()[20].UriSource.ToString(), 921, 202, 125, 272);
                SerializeConf(GetImagesTerritoires()[21].UriSource.ToString(), 837, 120, 132, 357);
                SerializeConf(GetImagesTerritoires()[22].UriSource.ToString(), 895, 321, 205, 145);
                SerializeConf(GetImagesTerritoires()[23].UriSource.ToString(), 998, 287, 154, 274);
                SerializeConf(GetImagesTerritoires()[24].UriSource.ToString(), 1123, 327, 245, 199);
                SerializeConf(GetImagesTerritoires()[25].UriSource.ToString(), 911, 389, 201, 244);
                SerializeConf(GetImagesTerritoires()[26].UriSource.ToString(), 746, 516, 142, 212);
                SerializeConf(GetImagesTerritoires()[27].UriSource.ToString(), 893, 571, 231, 232);
                SerializeConf(GetImagesTerritoires()[28].UriSource.ToString(), 1049, 502, 300, 186);
                SerializeConf(GetImagesTerritoires()[29].UriSource.ToString(), 1415, 130, 149, 134);
                SerializeConf(GetImagesTerritoires()[30].UriSource.ToString(), 1440, 194, 146, 194);
                SerializeConf(GetImagesTerritoires()[31].UriSource.ToString(), 1519, 40, 170, 267);
                SerializeConf(GetImagesTerritoires()[32].UriSource.ToString(), 1709, 49, 319, 191);
                SerializeConf(GetImagesTerritoires()[33].UriSource.ToString(), 1587, 182, 146, 182);
                SerializeConf(GetImagesTerritoires()[34].UriSource.ToString(), 1549, 296, 136, 286);
                SerializeConf(GetImagesTerritoires()[35].UriSource.ToString(), 1740, 326, 225, 148);
                SerializeConf(GetImagesTerritoires()[36].UriSource.ToString(), 1416, 349, 224, 245);
                SerializeConf(GetImagesTerritoires()[37].UriSource.ToString(), 1493, 422, 191, 309);
                SerializeConf(GetImagesTerritoires()[38].UriSource.ToString(), 1592, 523, 262, 260);
                SerializeConf(GetImagesTerritoires()[39].UriSource.ToString(), 1413, 550, 212, 194);
                SerializeConf(GetImagesTerritoires()[40].UriSource.ToString(), 1475, 668, 218, 213);

                SauveCollection s = new SauveCollection(Environment.CurrentDirectory);
                s.Sauver(_decorations.Cast<TerritoireDecorator>().ToList(), "Cartee");
            }
            else
            {
                SauveCollection s = new SauveCollection(Environment.CurrentDirectory);
                s.Sauver(carte, "Cartee");
            }
            
        }

        public void SerializeConf(string Urisource, int x, int y, int width, int height)
        {
            _decorations.Add(new TerritoireDecorator(new TerritoireBase(i), x, y, width, height, Urisource));
            i++;
        }
    }
}
