using JurassicRisk.Ressources;
using Models.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JurassicRisk.ViewsModels
{
    /// <author>Charif</author>
    public class ViewModelCarte : observable.Observable
    {
        private List<BitmapImage> _sprites;
        private List<Continent> _continents;
        private List<String> _fileEntries;
        private Canvas _carteCanvas;
        private Carte _carte;
        private int zi = 0;

        public Canvas CarteCanvas
        {
            get
            {
                return _carteCanvas;
            }
        }

        public Carte Carte
        {
            get
            {
                return _carte;
            }
        }

        /// <summary>
        /// Cree la carte et la dessine
        /// </summary>
        /// <author>Charif</author>
        public ViewModelCarte()
        {
            _sprites = GetImagesTerritoires();
            _continents = CreerContinent();
            _carte = DrawCarte();

            NotifyPropertyChanged("Carte");
        }

        /// <summary>
        /// Dessine les territoire
        /// </summary>
        /// <returns>List de Sprites</returns>
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

        /// <summary>
        /// Cree les contient avec la liste des territoires grace aux noms des image
        /// </summary>
        /// <returns>List de continent</returns>
        /// <Author>Charif</Author>
        private List<Continent> CreerContinent()
        {
            List<Continent> continents = new List<Continent>();

            for (int i = 0; i < 6; i++)
            {
                Continent continent = new Continent();

                for (int ii = 0; ii < _fileEntries.Where(c => c[0] == (char)i).Count(); i++)
                {
                    continent.Territoires.Add(new TerritoireBase());
                }
                continents.Add(continent);
            }

            return continents;
        }

        /// <summary>
        /// Add all region to the Canvas (CarteCanvas) with DrawRegion
        /// </summary>
        /// <Author>Charif</Author>
        private Carte DrawCarte()
        {
            _carteCanvas = new Canvas();

            DrawRegion(_sprites[0], 94, 51, 152, 70);
            DrawRegion(_sprites[1], 114, 149, 97, 130);
            DrawRegion(_sprites[2], 148, 1, 160, 194);
            DrawRegion(_sprites[3], 268, 32, 90, 146);
            DrawRegion(_sprites[4], 332, 38, 182, 150);
            DrawRegion(_sprites[5], 222, 114, 146, 138);
            DrawRegion(_sprites[6], 290, 157, 151, 220);
            DrawRegion(_sprites[7], 112, 580, 188, 217);
            DrawRegion(_sprites[8], 107, 735, 226, 158);
            DrawRegion(_sprites[9], 163, 807, 165, 188);
            DrawRegion(_sprites[10], 249, 637, 182, 161);
            DrawRegion(_sprites[11], 388, 620, 215, 161);
            DrawRegion(_sprites[12], 296, 735, 302, 146);
            DrawRegion(_sprites[13], 400, 751, 250, 215);
            DrawRegion(_sprites[14], 510, 424, 177, 188);
            DrawRegion(_sprites[15], 641, 484, 168, 266);
            DrawRegion(_sprites[16], 745, 332, 184, 162);
            DrawRegion(_sprites[17], 635, 302, 193, 139);
            DrawRegion(_sprites[18], 617, 127, 201, 238);
            DrawRegion(_sprites[19], 728, 184, 214, 244);
            DrawRegion(_sprites[20], 921, 202, 125, 272);
            DrawRegion(_sprites[21], 837, 120, 132, 357);
            DrawRegion(_sprites[22], 895, 321, 205, 145);
            DrawRegion(_sprites[23], 998, 287, 154, 274);
            DrawRegion(_sprites[24], 1123, 327, 245, 199);
            DrawRegion(_sprites[25], 911, 389, 201, 244);
            DrawRegion(_sprites[26], 746, 516, 142, 212);
            DrawRegion(_sprites[27], 893, 571, 231, 232);
            DrawRegion(_sprites[28], 1049, 502, 300, 186);
            DrawRegion(_sprites[29], 1415, 130, 149, 134);
            DrawRegion(_sprites[30], 1440, 194, 146, 194);
            DrawRegion(_sprites[31], 1519, 40, 170, 267);
            DrawRegion(_sprites[32], 1709, 49, 319, 191);
            DrawRegion(_sprites[33], 1587, 182, 146, 182);
            DrawRegion(_sprites[34], 1549, 296, 136, 286);
            DrawRegion(_sprites[35], 1740, 326, 225, 148);
            DrawRegion(_sprites[36], 1416, 349, 224, 245);
            DrawRegion(_sprites[37], 1493, 422, 191, 309);
            DrawRegion(_sprites[38], 1592, 523, 262, 260);
            DrawRegion(_sprites[39], 1413, 550, 212, 194);
            DrawRegion(_sprites[40], 1475, 668, 218, 213);
         
            return new Carte(_continents);
        }

        /// <summary>
        /// Dessine les regions et les ajoute a la carte
        /// </summary>
        /// <param name="territoire">Territoire</param>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="height">hauteur</param>
        /// <param name="width">largeur</param>
        /// <Author>Charif</Author>
        private void DrawRegion(BitmapImage sprite, int x, int y, int height, int width)
        {
            ImageBrush myImageBrush = new ImageBrush(sprite);
            Canvas myCanvas = new Canvas();

            myCanvas.Background = myImageBrush;
            myCanvas.Height = height;
            myCanvas.Width = width;
            Canvas.SetLeft(myCanvas, x);
            Canvas.SetTop(myCanvas, y);
            myCanvas.MouseEnter += MyCanvas_MouseEnter; ;
            myCanvas.MouseLeave += MyCanvas_MouseLeave; ;
            _carteCanvas.Children.Add(myCanvas);
        }

        private void MyCanvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Canvas c = (sender as Canvas);
            c.Width -= 15;
            c.Height -= 15;
            NotifyPropertyChanged("Carte");
        }

        private void MyCanvas_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Canvas c = (sender as Canvas);
            Canvas.SetZIndex(c, zi);
            c.Width += 15;
            c.Height += 15;
            zi++;
            NotifyPropertyChanged("Carte");
        }
    }
}
