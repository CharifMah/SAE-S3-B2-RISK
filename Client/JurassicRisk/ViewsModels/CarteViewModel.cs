using Models;
using Models.Fabriques.FabriqueUnite;
using Models.Player;
using Models.Son;
using Models.Tours;
using Stockage;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using Carte = Models.Map.Carte;
using Continent = Models.Map.Continent;
using IUnit = Models.Units.IUnit;
using TerritoireBase = Models.Map.TerritoireBase;
using TerritoireDecorator = Models.Map.TerritoireDecorator;

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
        private ITour tour = new TourAttente();
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

        public ITour Tour { get => tour; set => tour = value; }

        #endregion

        /// <summary>
        /// Cree la carte et la dessine
        /// </summary>
        /// <author>Charif</author>
        public CarteViewModel(JoueurViewModel joueur)
        {
            //Charge le fichier Cartee.json
            ChargerCollection c = new ChargerCollection(Environment.CurrentDirectory);
            //new SaveMap(null);
            _carte = c.Charger<Carte>("Map/Cartee");
            _carteCanvas = new Canvas();
            TerritoireBase t = new TerritoireBase(999999);
            t.Team = Teams.BLEU;
            _carte.SelectedTerritoire = t;
            SetCarte(_carte);
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
        public async Task<string> SetCarte(Carte carte)
        {
            string res = "Ok";
            try
            {
                JurasicRiskGameClient.Get.Client.DefaultRequestHeaders.Accept.Clear();
                JurasicRiskGameClient.Get.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json-patch+json"));

                HttpResponseMessage reponse = await JurasicRiskGameClient.Get.Client.PostAsJsonAsync($"https://{JurasicRiskGameClient.Get.Ip}/Carte/SetCarte", carte);

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
            Canvas.SetLeft(myCanvas, territoire.X);
            Canvas.SetTop(myCanvas, territoire.Y);
            myCanvas.ToolTip = $"Units: {territoire.TerritoireBase.Units.Count} ID : {territoire.ID} team : {territoire.Team}";
            myCanvas.ToolTipOpening += (sender, e) => MyCanvas_ToolTipOpening(sender, e, territoire, myCanvas);
            ToolTipService.SetInitialShowDelay(myCanvas, 0);
            myCanvas.MouseEnter += (sender, e) => MyCanvas_MouseEnter(sender, e);
            myCanvas.MouseLeave += async (sender, e) => await MyCanvas_MouseLeave(sender, e);
            myCanvas.PreviewMouseDown += async (sender, e) => await MyCanvas_PreviewMouseDown(sender, e, territoire);
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

        private async Task MyCanvas_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e, TerritoireDecorator territoire)
        {
            Canvas c = sender as Canvas;
            DropShadowEffect shadow = new DropShadowEffect();

            shadow.Color = Brushes.Green.Color;
            c.Effect = shadow;

            switch (Tour.GetType().Name)
            {
                case "TourPlacement":
                    this.tour.PlaceUnits(_joueur.SelectedUnit,_joueur.Joueur);
                    break;
            }

            await SetCarte(_carte);
            NotifyPropertyChanged("CarteCanvas");
        }

        private async Task MyCanvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Canvas c = (sender as Canvas);
            c.Width -= 15;
            c.Height -= 15;
            DropShadowEffect shadow = new DropShadowEffect();
            shadow.Color = Brushes.Black.Color;
            c.Effect = shadow;

            await SetCarte(_carte);
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
            SoundStore.Get("PassageMap.mp3").Play();
            NotifyPropertyChanged("Carte");
            NotifyPropertyChanged("CarteCanvas");
        }
    }
    #endregion



}
