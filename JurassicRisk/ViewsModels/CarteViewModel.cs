using Models;
using Models.Fabriques.FabriqueUnite;
using Models.Map;
using Models.Units;
using Stockage;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
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
        private JoueurViewModel _joueur;
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

        #endregion

        /// <summary>
        /// Cree la carte et la dessine
        /// </summary>
        /// <author>Charif</author>
        public CarteViewModel(JoueurViewModel joueur)
        {
            //Charge le fichier Cartee.json
            ChargerCollection c = new ChargerCollection(Environment.CurrentDirectory);
            _carte = c.Charger<Carte>("Map/Cartee");
            _carteCanvas = new Canvas();

            InsertCarte(_carte);
            foreach (Continent continent in _carte.DicoContinents.Values)
            {
                foreach (TerritoireDecorator Territoire in continent.DicoTerritoires.Values)
                {
                    DrawRegion(Territoire);
                }
            }

            f = new FabriqueUniteBase();
            _joueur = joueur;

            NotifyPropertyChanged("CarteCanvas");
            NotifyPropertyChanged("Carte");
        }

        /// <summary>
        /// Set Value of the selected profil
        /// </summary>
        /// <param name="pseudo">string pseudo</param>
        /// <returns>awaitable Task</returns>
        /// <Author>Charif Mahmoud</Author>
        public async Task<string> InsertCarte(Carte carte)
        {
            string res = "Ok";
            try
            {
                JurasicRiskGame.Get.Client.DefaultRequestHeaders.Accept.Clear();
                JurasicRiskGame.Get.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage reponse = await JurasicRiskGame.Get.Client.PostAsJsonAsync<Carte>($"https://{JurasicRiskGame.Get.Ip}/Carte/SetCarte", carte);

                if (reponse.IsSuccessStatusCode)
                {
                    res = reponse.Content.ReadAsStringAsync().Result;
                }

            }
            catch (Exception e)
            {
                res = e.Message;
            }
            return res;
        }

        #region Event

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

            this._carte.SelectedTerritoire = null;
        }

        private void MyCanvas_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e, TerritoireDecorator territoire)
        {
            Canvas c = sender as Canvas;
            DropShadowEffect shadow = new DropShadowEffect();

            shadow.Color = Brushes.Green.Color;
            c.Effect = shadow;


            this._carte.SelectedTerritoire = territoire;
            if (_joueur.Joueur.Troupe.Count > 0 && this._carte.SelectedTerritoire != null)
            {
                _joueur.AddUnits(new List<IUnit>() { _joueur.SelectedUnit }, this._carte.SelectedTerritoire);
            }

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
    #endregion

       

}
