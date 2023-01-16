using JurassicRisk.Views;
using Models;
using Models.Fabriques.FabriqueUnite;
using Models.Graph;
using Models.Map;
using Models.Son;
using Models.Tours;
using Stockage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using Carte = Models.Map.Carte;
using Color = System.Windows.Media.Color;
using Continent = Models.Map.Continent;
using IUnit = Models.Units.IUnit;
using TerritoireDecorator = Models.Map.TerritoireDecorator;

namespace JurassicRisk.ViewsModels
{
    /// <author>Charif</author>
    public class CarteViewModel : observable.Observable
    {
        #region Attributes
        private Stopwatch _timer;
        private AdjacencySetGraph _graph;
        private Canvas _carteCanvas;
        private Carte _carte;
        private FabriqueUniteBase f;
        private int zi = 0;
        private JoueurViewModel _joueur;
        private ITour tour = new TourAttente();
        private List<ITerritoireBase> _territoires;

        public delegate void DrawEnd();
        public delegate void Progression(double rate);
        private DrawEnd toDoWhenFinished;
        private Progression progress;
        private long currentPosition;
        private bool drawing;

        private Point previousPositionZoom;
        #endregion

        #region Property

        public Canvas CarteCanvas
        {
            get
            {
                return _carteCanvas;
            }
            set 
            {
                _carteCanvas = value;
                NotifyPropertyChanged();
            }
        }

        public Carte Carte
        {
            get
            {
                return _carte;
            }
            set
            {
                _carte = value;
                NotifyPropertyChanged();
            }
        }

        public ITour Tour { get => tour; set => tour = value; }

        #endregion

        #region Constructor
        /// <summary>
        /// Cree la carte et la dessine
        /// </summary>
        /// <author>Charif</author>
        public CarteViewModel(JoueurViewModel joueur, DrawEnd drawEnd, Progression progression)
        {
            currentPosition = 0;
            drawing = false;
            toDoWhenFinished = drawEnd;
            progress = progression;

            //new SaveMap(_carte);
            InitCarte();

            f = new FabriqueUniteBase();
            _joueur = joueur;
            previousPositionZoom = new Point();

        }
        #endregion

        /// <summary>
        /// Initialize the map
        /// </summary>
        private void InitCarte()
        {
            //Charge le fichier Cartee.json
            ChargerCollection c = new ChargerCollection(Environment.CurrentDirectory);
            _carte = c.Charger<Carte>("Map/Cartee");
            _carteCanvas = new Canvas();

            _territoires = new List<ITerritoireBase>();
            drawing = true;
            currentPosition = 0;
            progress(currentPosition);
            int i = 0;
            foreach (Continent continent in _carte.DicoContinents.Values)
            {
                foreach (TerritoireDecorator Territoire in continent.DicoTerritoires.Values)
                {
                    Territoire.ID = i;
                    i++;
                    _territoires.Add(Territoire);
                    DrawRegion(Territoire);
                    currentPosition += (100 / continent.DicoTerritoires.Values.Count);
                    progress(currentPosition);
                }
            }

            InitGraph();

            NotifyPropertyChanged("CarteCanvas");
            NotifyPropertyChanged("Carte");
        }

        /// <summary>
        /// Initialize the graph of the map
        /// </summary>
        private void InitGraph()
        {
            _graph = new AdjacencySetGraph(_territoires);

            currentPosition = 1;
            progress(currentPosition);
            #region Territoire 1
            _graph.AddEdge(_territoires[0], _territoires[1], 1);
            _graph.AddEdge(_territoires[0], _territoires[2], 1);
            _graph.AddEdge(_territoires[0], _territoires[32], 1);

            _graph.AddEdge(_territoires[1], _territoires[2], 1);
            _graph.AddEdge(_territoires[1], _territoires[5], 1);

            _graph.AddEdge(_territoires[2], _territoires[3], 1);
            _graph.AddEdge(_territoires[2], _territoires[4], 1);
            _graph.AddEdge(_territoires[2], _territoires[5], 1);

            _graph.AddEdge(_territoires[3], _territoires[4], 1);
            _graph.AddEdge(_territoires[3], _territoires[5], 1);

            _graph.AddEdge(_territoires[4], _territoires[6], 1);
            _graph.AddEdge(_territoires[4], _territoires[5], 1);

            _graph.AddEdge(_territoires[5], _territoires[6], 1);

            _graph.AddEdge(_territoires[6], _territoires[18], 1);

            #endregion
            currentPosition = 33;
            progress(currentPosition);
            #region Territoire 2

            _graph.AddEdge(_territoires[7], _territoires[10], 1);
            _graph.AddEdge(_territoires[7], _territoires[8], 1);

            _graph.AddEdge(_territoires[8], _territoires[10], 1);
            _graph.AddEdge(_territoires[8], _territoires[9], 1);

            _graph.AddEdge(_territoires[9], _territoires[10], 1);
            _graph.AddEdge(_territoires[9], _territoires[12], 1);

            _graph.AddEdge(_territoires[10], _territoires[11], 1);
            _graph.AddEdge(_territoires[10], _territoires[12], 1);

            _graph.AddEdge(_territoires[11], _territoires[12], 1);
            _graph.AddEdge(_territoires[11], _territoires[13], 1);
            _graph.AddEdge(_territoires[11], _territoires[14], 1);

            _graph.AddEdge(_territoires[12], _territoires[13], 1);

            #endregion
            currentPosition = 66;
            progress(currentPosition);
            #region Territoire 3

            _graph.AddEdge(_territoires[14], _territoires[15], 1);
            _graph.AddEdge(_territoires[14], _territoires[17], 1);

            _graph.AddEdge(_territoires[15], _territoires[16], 1);
            _graph.AddEdge(_territoires[15], _territoires[17], 1);
            _graph.AddEdge(_territoires[15], _territoires[26], 1);

            _graph.AddEdge(_territoires[16], _territoires[17], 1);
            _graph.AddEdge(_territoires[16], _territoires[19], 1);
            _graph.AddEdge(_territoires[16], _territoires[22], 1);

            _graph.AddEdge(_territoires[17], _territoires[18], 1);
            _graph.AddEdge(_territoires[17], _territoires[19], 1);

            _graph.AddEdge(_territoires[18], _territoires[19], 1);
            _graph.AddEdge(_territoires[18], _territoires[21], 1);

            _graph.AddEdge(_territoires[19], _territoires[20], 1);
            _graph.AddEdge(_territoires[19], _territoires[21], 1);
            _graph.AddEdge(_territoires[19], _territoires[22], 1);


            _graph.AddEdge(_territoires[20], _territoires[21], 1);
            _graph.AddEdge(_territoires[20], _territoires[22], 1);
            _graph.AddEdge(_territoires[20], _territoires[23], 1);

            #region Region 4
            _graph.AddEdge(_territoires[22], _territoires[23], 1);
            _graph.AddEdge(_territoires[22], _territoires[25], 1);

            _graph.AddEdge(_territoires[23], _territoires[24], 1);
            _graph.AddEdge(_territoires[23], _territoires[25], 1);
            _graph.AddEdge(_territoires[23], _territoires[28], 1);
            _graph.AddEdge(_territoires[23], _territoires[29], 1);

            _graph.AddEdge(_territoires[24], _territoires[25], 1);
            _graph.AddEdge(_territoires[24], _territoires[28], 1);
            _graph.AddEdge(_territoires[24], _territoires[36], 1);

            _graph.AddEdge(_territoires[25], _territoires[26], 1);
            _graph.AddEdge(_territoires[25], _territoires[27], 1);
            _graph.AddEdge(_territoires[25], _territoires[28], 1);

            _graph.AddEdge(_territoires[26], _territoires[27], 1);

            _graph.AddEdge(_territoires[27], _territoires[28], 1);

            _graph.AddEdge(_territoires[28], _territoires[39], 1);
            #endregion

            #region Region 5
            _graph.AddEdge(_territoires[29], _territoires[30], 1);
            _graph.AddEdge(_territoires[29], _territoires[31], 1);

            _graph.AddEdge(_territoires[30], _territoires[31], 1);
            _graph.AddEdge(_territoires[30], _territoires[33], 1);
            _graph.AddEdge(_territoires[30], _territoires[34], 1);

            _graph.AddEdge(_territoires[31], _territoires[32], 1);
            _graph.AddEdge(_territoires[31], _territoires[33], 1);

            _graph.AddEdge(_territoires[32], _territoires[33], 1);
            _graph.AddEdge(_territoires[32], _territoires[34], 1);
            _graph.AddEdge(_territoires[32], _territoires[35], 1);

            _graph.AddEdge(_territoires[33], _territoires[34], 1);

            _graph.AddEdge(_territoires[34], _territoires[35], 1);
            _graph.AddEdge(_territoires[34], _territoires[36], 1);
            _graph.AddEdge(_territoires[34], _territoires[37], 1);

            _graph.AddEdge(_territoires[35], _territoires[37], 1);
            _graph.AddEdge(_territoires[35], _territoires[38], 1);

            _graph.AddEdge(_territoires[36], _territoires[37], 1);
            _graph.AddEdge(_territoires[36], _territoires[39], 1);

            _graph.AddEdge(_territoires[37], _territoires[38], 1);
            _graph.AddEdge(_territoires[37], _territoires[39], 1);

            _graph.AddEdge(_territoires[38], _territoires[39], 1);
            _graph.AddEdge(_territoires[38], _territoires[40], 1);

            _graph.AddEdge(_territoires[39], _territoires[40], 1);
            #endregion

            #endregion
            currentPosition = 100;
            progress(currentPosition);
        }

        #region Drawing

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
            //TerritoireDecorator
            Canvas myCanvas = new Canvas();
            MyImage myImageBrush = new MyImage(new BitmapImage(new Uri(territoire.UriSource)));
            territoire.X = (int)((myImageBrush.Points[0].X + myImageBrush.Points[1].X) / 2);
            territoire.Y = (int)((myImageBrush.Points[0].Y + myImageBrush.Points[2].Y) / 2);
            territoire.Points = myImageBrush.Points;
            territoire.Width = (int)myImageBrush.Size.Width;
            territoire.Height = (int)myImageBrush.Size.Height;
            myCanvas.Children.Add(myImageBrush);
            //Add All ElementUI to Carte Canvas
            myCanvas.ToolTip = new ToolTip() { Content = $"Units: {territoire.TerritoireBase.Units.Count} ID : {territoire.ID} team : {territoire.Team}" };
            myCanvas.ToolTipOpening += (sender, e) => MyCanvas_ToolTipOpening(sender, e, territoire, myCanvas);
            myCanvas.MouseEnter += (sender, e) => MyCanvas_MouseEnter(sender, e);
            myCanvas.MouseLeave += (sender, e) => MyCanvas_MouseLeave(sender, e);
            myCanvas.PreviewMouseDown += (sender, e) => MyCanvas_PreviewMouseDown(sender, e, territoire);
            myCanvas.PreviewMouseUp += (sender, e) => MyCanvas_PreviewMouseUp(sender, e, territoire);
            ToolTipService.SetInitialShowDelay(myCanvas, 1);

            _carteCanvas.Children.Add(myCanvas);
        }

        /// <summary>
        /// DrawLines Graph need to be initialized
        /// </summary>
        /// <param name="territoire"></param>
        private void DrawLines(TerritoireDecorator territoire)
        {
            IEnumerable<ITerritoireBase> AdjacentT = _graph.GetAdjacentVertices(territoire);
            Random rand = new Random();
            Brush brush = new SolidColorBrush(Color.FromRgb((byte)rand.Next(0, 256), (byte)rand.Next(0, 256), (byte)rand.Next(0, 256)));
            foreach (TerritoireDecorator territoire1 in AdjacentT)
            {
                Line l = new Line();

                l.X1 = territoire.X; l.Y1 = territoire.Y;
                l.X2 = territoire1.X; l.Y2 = territoire1.Y;
                l.Stroke = brush;
                l.StrokeThickness = 5;
                l.IsHitTestVisible = false;
                Canvas.SetZIndex(l, 4);
                territoire.Lines.Add(l);

                _carteCanvas.Children.Add(l);
            }

        }

        private void EraseLine(TerritoireDecorator territoire)
        {
            foreach (Line l in territoire.Lines)
            {
                _carteCanvas.Children.Remove(l);
            }
        }

        /// <summary>
        /// Cancel the drawing
        /// </summary>
        public void CancelDrawRegion()
        {
            drawing = false;
        }
        #endregion

        #region Request

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
        #endregion

        #region Event
        private void MyCanvas_ToolTipOpening(object sender, ToolTipEventArgs e, TerritoireDecorator territoire, Canvas canvas)
        {
            canvas.ToolTip = $"Units: {territoire.Units.Count} ID : {territoire.ID} team : {territoire.Team}";
        }

        private async void MyCanvas_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e, TerritoireDecorator territoire)
        {
            Canvas c = sender as Canvas;
            DropShadowEffect shadow = new DropShadowEffect();

            if (e.ChangedButton == MouseButton.Right)
            {
                this._carte.SelectedTerritoire = territoire;
                await JurasicRiskGameClient.Get.PartieChatService.SetSelectedTerritoire(JurassicRiskViewModel.Get.LobbyVm.Lobby.Id, territoire.ID);
                await JurasicRiskGameClient.Get.PartieChatService.Action(JurassicRiskViewModel.Get.LobbyVm.Lobby.Id, new List<int>() { 0 });
                JeuPage.GetInstance().ZoomIn(territoire.X, territoire.Y,2);
            }

            if (e.ChangedButton == MouseButton.Left)
            {
                DrawLines(territoire);
                JeuPage.GetInstance().ZoomOut(territoire.X, territoire.Y,1);
            }

            if (e.ChangedButton == MouseButton.Middle)
            {
                JeuPage.GetInstance().ResetZoom();
                EraseLine(territoire);
            }

            shadow.Color = Brushes.Black.Color;
            c.Effect = shadow;
            this._carte.SelectedTerritoire = null;
            NotifyPropertyChanged("Carte");
            NotifyPropertyChanged("CarteCanvas");
        }

        private void MyCanvas_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e, TerritoireDecorator territoire)
        {
            Canvas? c = sender as Canvas;
            DropShadowEffect shadow = new DropShadowEffect();

            shadow.Color = Brushes.Green.Color;
            c.Effect = shadow;

            if (_joueur.Joueur.Units.Count > 0 && this._carte.SelectedTerritoire != null)
            {
                _joueur.AddUnits(new List<IUnit>() { _joueur.SelectedUnit }, this._carte.SelectedTerritoire);
            }

            NotifyPropertyChanged("CarteCanvas");
        }

        private void MyCanvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Canvas c = (sender as Canvas);
            c.Width -= 35;
            c.Height -= 35;
            DropShadowEffect shadow = new DropShadowEffect();
            shadow.Color = Brushes.Black.Color;
            c.Effect = shadow;

            if (zi > 2)
            {
                zi = 0;
            }

            Canvas.SetZIndex(c, zi);
            NotifyPropertyChanged("Carte");
            NotifyPropertyChanged("CarteCanvas");
        }

        private void MyCanvas_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Canvas c = (sender as Canvas);
            Canvas.SetZIndex(c, zi);
            c.Width += 35;
            c.Height += 35;
            zi++;

            SoundStore.Get("PassageMap.mp3").Play();
            NotifyPropertyChanged("Carte");
            NotifyPropertyChanged("CarteCanvas");
        }
        #endregion


    }
}
