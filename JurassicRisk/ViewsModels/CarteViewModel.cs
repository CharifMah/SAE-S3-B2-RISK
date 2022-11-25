using Models.Fabriques.FabriqueUnite;
using Models.Map;
using Models.Units;
using Stockage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace JurassicRisk.ViewsModels
{
    /// <author>Charif</author>
    public class CarteViewModel : observable.Observable
    {
        #region Attributes

        private Canvas _carteCanvas;
        private Carte _carte;
        private FabriqueUniteBase f;
        private int zi = 0;
        private JoueurViewModel _joueurVm;

        #endregion

        #region Property
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
        
        public JoueurViewModel JoueurVm { get => _joueurVm; set => _joueurVm = value; }

        #endregion

        /// <summary>
        /// Cree la carte et la dessine
        /// </summary>
        /// <author>Charif</author>
        public CarteViewModel()
        {
            //ChargerCollection c = new ChargerCollection(Environment.CurrentDirectory);
            //_decorations = c.Charger<List<TerritoireDecorator>>("Map/Cartee");
            //_carte = CreateCarte(_decorations);
            //new SaveMap(_carte);

            //Charge le fichier Cartee.json
            ChargerCollection c = new ChargerCollection(Environment.CurrentDirectory);
            _carte = c.Charger<Carte>("Map/Cartee");
            _carteCanvas = new Canvas();
            foreach (Continent continent in _carte.DicoContinents.Values)
            {
                foreach (TerritoireDecorator Territoire in continent.DicoTerritoires.Values)
                {
                    DrawRegion(Territoire);
                }
            }

            f = new FabriqueUniteBase();
            JoueurVm = new JoueurViewModel();

            NotifyPropertyChanged("CarteCanvas");
            NotifyPropertyChanged("Carte");
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
            myCanvas.ToolTipOpening += (sender, e) => MyCanvas_ToolTipOpening(sender, e, territoire, myCanvas);
            ToolTipService.SetInitialShowDelay(myCanvas, 0);
            myCanvas.MouseEnter += MyCanvas_MouseEnter;
            myCanvas.MouseLeave += MyCanvas_MouseLeave;
            myCanvas.PreviewMouseDown += (sender, e) => MyCanvas_PreviewMouseDown(sender, e, territoire);
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

            JoueurVm.PositionnerTroupe(new List<UniteBase>(JoueurVm.Joueur.Troupe), territoire);
            NotifyPropertyChanged("CarteCanvas");
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