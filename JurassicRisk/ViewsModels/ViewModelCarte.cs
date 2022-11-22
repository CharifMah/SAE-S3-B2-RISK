using JurassicRisk.Ressources;
using Models.Map;
using Stockage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace JurassicRisk.ViewsModels
{
    /// <author>Charif</author>
    public class ViewModelCarte : observable.Observable
    {

        private List<Continent> _continents;
        private List<String> _fileEntries;
        private List<TerritoireDecorator> _decorations;
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
            //SaveCarte();
            _decorations = new List<TerritoireDecorator>();
            _carte = DrawCarte();
            NotifyPropertyChanged("Carte");
        }

        /// <summary>
        /// Add all region to the Canvas (CarteCanvas) with DrawRegion
        /// </summary>
        /// <Author>Charif</Author>
        private Carte DrawCarte()
        {
            _carteCanvas = new Canvas();
            _continents = new List<Continent>();

            //Charge le fichier Cartee.json
            ChargerCollection c = new ChargerCollection(Environment.CurrentDirectory);
            _decorations = c.Charger<List<TerritoireDecorator>>("Map/Cartee");

            List<ITerritoireBase> _territoiresBase = new List<ITerritoireBase>();

            foreach (TerritoireDecorator territoireDecorator in _decorations)
            {
                _territoiresBase.Add(territoireDecorator.TerritoireBase);
                DrawRegion(territoireDecorator);
            }

            _continents.Add(new Continent(_territoiresBase.Take(7).ToList()));
            _continents.Add(new Continent(_territoiresBase.Skip(7).Take(7).ToList()));
            _continents.Add(new Continent(_territoiresBase.Skip(14).Take(8).ToList()));
            _continents.Add(new Continent(_territoiresBase.Skip(22).Take(7).ToList()));
            _continents.Add(new Continent(_territoiresBase.Skip(29).Take(5).ToList()));
            _continents.Add(new Continent(_territoiresBase.Skip(34).Take(7).ToList()));

            NotifyPropertyChanged("Carte");

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
        private void DrawRegion(TerritoireDecorator territoire)
        {
            ImageBrush myImageBrush = new ImageBrush(new BitmapImage(new Uri(territoire.UriSource)));
            Canvas myCanvas = new Canvas();

            myCanvas.Background = myImageBrush;
            myCanvas.Height = territoire.Width;
            myCanvas.Width = territoire.Height;
            Canvas.SetLeft(myCanvas, territoire.x);
            Canvas.SetTop(myCanvas, territoire.y);
            myCanvas.ToolTip = $"X: {territoire.x} Y: {territoire.y} t : {territoire.Team}";
            ToolTipService.SetInitialShowDelay(myCanvas, 0);
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
