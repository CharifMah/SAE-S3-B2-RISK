
using Models;
using Models.Map;
using Models.Units;
using Stockage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace JurassicRisk.ViewsModels
{
    /// <author>Charif</author>
    public class ViewModelCarte : observable.Observable
    {

        private List<Continent> _continents;
        private List<TerritoireDecorator> _decorations;
        List<ITerritoireBase> _territoiresBase;
        private Canvas _carteCanvas;
        private Carte _carte;
        private FabriqueUnite f;
        private int zi = 0;
        private Joueur j;

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
            //Charge le fichier Cartee.json
            ChargerCollection c = new ChargerCollection(Environment.CurrentDirectory);
            _decorations = c.Charger<List<TerritoireDecorator>>("Map/Cartee");
            f = new FabriqueUnite();
            j = new Joueur(new Profil("s",""),new List<Unite>() { new Unite()},Teams.NEUTRE);
            _continents = new List<Continent>();
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
            _territoiresBase = new List<ITerritoireBase>();
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
            myCanvas.ToolTip = $"Units: {territoire.TerritoireBase.Units.Count} ID : {territoire.ID} team : {territoire.Team}";
            myCanvas.ToolTipOpening += (sender, e) => MyCanvas_ToolTipOpening(sender, e, territoire,myCanvas);
            ToolTipService.SetInitialShowDelay(myCanvas, 0);
            myCanvas.MouseEnter += MyCanvas_MouseEnter;
            myCanvas.MouseLeave += MyCanvas_MouseLeave;
            myCanvas.PreviewMouseDown += (sender, e) => MyCanvas_PreviewMouseDown(sender,e,territoire);
            myCanvas.PreviewMouseUp += (sender, e) => MyCanvas_PreviewMouseUp(sender, e, territoire);
            _carteCanvas.Children.Add(myCanvas);
        }

        private void MyCanvas_ToolTipOpening(object sender, ToolTipEventArgs e, TerritoireDecorator territoire, Canvas canvas)
        {
            canvas.ToolTip = $"Units: {territoire.Units.Count} ID : {territoire.ID} team : {territoire.Team}";
        }

        private void MyCanvas_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e, TerritoireDecorator territoire)
        {
            Canvas c = sender as Canvas;
            DropShadowEffect shadow = new DropShadowEffect();
            
            shadow.Color = Brushes.Black.Color;
            c.Effect = shadow;
        }

        private void MyCanvas_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e, TerritoireDecorator territoire)
        {
            Canvas c = sender as Canvas;
            
            if(territoire.Team == Teams.NEUTRE)
            {
                territoire.Team = (Teams.ROUGE);
                DropShadowEffect shadow = new DropShadowEffect();
                shadow.Color = Brushes.Red.Color;
                c.Effect = shadow;
            }
            else
            {
                List<Unite> renforts = new List<Unite>();
                var unit = f.Create("Brachiosaure");
                renforts.Add(unit);
                j.Troupe.Add(unit);
                j.PositionnerTroupe(renforts, territoire);
                MessageBox.Show("Troupes ajoutées");
            }
        }

        private void MyCanvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Canvas c = (sender as Canvas);
            c.Width -= 15;
            c.Height -= 15;
            DropShadowEffect shadow = new DropShadowEffect();
            shadow.Color = Brushes.Black.Color;
            c.Effect = shadow;
            NotifyPropertyChanged("Carte");
            NotifyPropertyChanged("CarteCanvas");
        }

        private void MyCanvas_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Canvas c = (sender as Canvas);
            Canvas.SetZIndex(c, zi);
            c.Width += 15;
            c.Height += 15;
            zi++;
            NotifyPropertyChanged("Carte");
            NotifyPropertyChanged("CarteCanvas");
        }
    }
}
