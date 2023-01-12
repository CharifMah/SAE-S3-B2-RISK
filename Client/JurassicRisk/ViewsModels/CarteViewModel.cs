using Models;
using Models.Fabriques.FabriqueUnite;
using Models.Graph;
using Models.Map;
using Models.Son;
using Stockage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Brushes = System.Windows.Media.Brushes;
using Carte = Models.Map.Carte;
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
        private List<ITerritoireBase> _territoires;

        public delegate void DrawEnd();
        public delegate void Progression(double rate);
        private DrawEnd toDoWhenFinished;
        private Progression progress;
        private long currentPosition;
        private bool drawing;
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

        public List<ITerritoireBase> Territoires { get => _territoires; set => _territoires = value; }
        #endregion

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

            //new SaveMap(null);
            InitCarte();
            f = new FabriqueUniteBase();
            _joueur = joueur;

        }

        /// <summary>
        /// Initialize the map
        /// </summary>
        private void InitCarte()
        {
            //Charge le fichier Cartee.json
            ChargerCollection c = new ChargerCollection(Environment.CurrentDirectory);
            _carte = c.Charger<Carte>("Map/Cartee");
            _carteCanvas = new Canvas();

            Territoires = new List<ITerritoireBase>();
            int i = 0;
            foreach (Continent continent in _carte.DicoContinents.Values)
            {
                foreach (TerritoireDecorator Territoire in continent.DicoTerritoires.Values)
                {
                    Territoire.ID = i;
                    i++;
                    Territoires.Add(Territoire);
                }
            }

            InitGraph();

            StartDrawRegion();
        }

        /// <summary>
        /// Initialize the graph of the map
        /// </summary>
        private void InitGraph()
        {
            _graph = new AdjacencySetGraph(Territoires);

            currentPosition = 1;
            progress(currentPosition);
            #region Territoire 1
            _graph.AddEdge(Territoires[0], Territoires[1], 1);
            _graph.AddEdge(Territoires[0], Territoires[2], 1);
            _graph.AddEdge(Territoires[0], Territoires[32], 1);

            _graph.AddEdge(Territoires[1], Territoires[2], 1);
            _graph.AddEdge(Territoires[1], Territoires[5], 1);

            _graph.AddEdge(Territoires[2], Territoires[3], 1);
            _graph.AddEdge(Territoires[2], Territoires[4], 1);

            _graph.AddEdge(Territoires[3], Territoires[4], 1);
            _graph.AddEdge(Territoires[3], Territoires[5], 1);

            _graph.AddEdge(Territoires[4], Territoires[6], 1);
            _graph.AddEdge(Territoires[4], Territoires[5], 1);

            _graph.AddEdge(Territoires[5], Territoires[6], 1);

            _graph.AddEdge(Territoires[6], Territoires[18], 1);

            #endregion
            currentPosition = 33;
            progress(currentPosition);
            #region Territoire 2

            _graph.AddEdge(Territoires[7], Territoires[10], 1);
            _graph.AddEdge(Territoires[7], Territoires[8], 1);

            _graph.AddEdge(Territoires[8], Territoires[10], 1);
            _graph.AddEdge(Territoires[8], Territoires[9], 1);

            _graph.AddEdge(Territoires[9], Territoires[10], 1);
            _graph.AddEdge(Territoires[9], Territoires[12], 1);

            _graph.AddEdge(Territoires[10], Territoires[11], 1);

            _graph.AddEdge(Territoires[11], Territoires[13], 1);
            _graph.AddEdge(Territoires[11], Territoires[14], 1);

            _graph.AddEdge(Territoires[12], Territoires[13], 1);

            #endregion
            currentPosition = 66;
            progress(currentPosition);
            #region Territoire 3

            _graph.AddEdge(Territoires[14], Territoires[15], 1);
            _graph.AddEdge(Territoires[14], Territoires[17], 1);

            _graph.AddEdge(Territoires[15], Territoires[16], 1);
            _graph.AddEdge(Territoires[15], Territoires[17], 1);

            _graph.AddEdge(Territoires[16], Territoires[17], 1);
            _graph.AddEdge(Territoires[16], Territoires[19], 1);
            _graph.AddEdge(Territoires[16], Territoires[22], 1);

            _graph.AddEdge(Territoires[17], Territoires[18], 1);
            _graph.AddEdge(Territoires[17], Territoires[19], 1);

            _graph.AddEdge(Territoires[18], Territoires[19], 1);
            _graph.AddEdge(Territoires[18], Territoires[21], 1);

            _graph.AddEdge(Territoires[19], Territoires[20], 1);
            _graph.AddEdge(Territoires[19], Territoires[21], 1);
            _graph.AddEdge(Territoires[19], Territoires[22], 1);


            _graph.AddEdge(Territoires[20], Territoires[21], 1);
            _graph.AddEdge(Territoires[20], Territoires[22], 1);
            _graph.AddEdge(Territoires[20], Territoires[23], 1);

            #region Region 4
            _graph.AddEdge(Territoires[22], Territoires[23], 1);
            _graph.AddEdge(Territoires[22], Territoires[25], 1);

            _graph.AddEdge(Territoires[23], Territoires[24], 1);
            _graph.AddEdge(Territoires[23], Territoires[25], 1);
            _graph.AddEdge(Territoires[23], Territoires[28], 1);
            _graph.AddEdge(Territoires[23], Territoires[29], 1);

            _graph.AddEdge(Territoires[24], Territoires[25], 1);
            _graph.AddEdge(Territoires[24], Territoires[28], 1);
            _graph.AddEdge(Territoires[24], Territoires[36], 1);

            _graph.AddEdge(Territoires[25], Territoires[26], 1);
            _graph.AddEdge(Territoires[25], Territoires[27], 1);

            _graph.AddEdge(Territoires[26], Territoires[27], 1);

            _graph.AddEdge(Territoires[27], Territoires[28], 1);

            _graph.AddEdge(Territoires[28], Territoires[39], 1);
            #endregion

            #region Region 5
            _graph.AddEdge(Territoires[29], Territoires[30], 1);
            _graph.AddEdge(Territoires[29], Territoires[31], 1);

            _graph.AddEdge(Territoires[30], Territoires[31], 1);
            _graph.AddEdge(Territoires[30], Territoires[33], 1);
            _graph.AddEdge(Territoires[30], Territoires[34], 1);

            _graph.AddEdge(Territoires[31], Territoires[32], 1);
            _graph.AddEdge(Territoires[31], Territoires[33], 1);

            _graph.AddEdge(Territoires[32], Territoires[33], 1);
            _graph.AddEdge(Territoires[32], Territoires[34], 1);
            _graph.AddEdge(Territoires[32], Territoires[35], 1);

            _graph.AddEdge(Territoires[33], Territoires[34], 1);

            _graph.AddEdge(Territoires[34], Territoires[35], 1);
            _graph.AddEdge(Territoires[34], Territoires[36], 1);
            _graph.AddEdge(Territoires[34], Territoires[37], 1);

            _graph.AddEdge(Territoires[35], Territoires[37], 1);
            _graph.AddEdge(Territoires[35], Territoires[38], 1);

            _graph.AddEdge(Territoires[36], Territoires[37], 1);
            _graph.AddEdge(Territoires[36], Territoires[39], 1);

            _graph.AddEdge(Territoires[37], Territoires[38], 1);
            _graph.AddEdge(Territoires[37], Territoires[39], 1);

            _graph.AddEdge(Territoires[38], Territoires[39], 1);
            _graph.AddEdge(Territoires[38], Territoires[40], 1);

            _graph.AddEdge(Territoires[39], Territoires[40], 1);
            #endregion

            #endregion
            currentPosition = 100;
            progress(currentPosition);

        }

        /// <summary>
        /// Start drawing regions
        /// </summary>
        public void StartDrawRegion()
        {
            drawing = true;
            currentPosition = 0;
            progress(currentPosition);
            int count = Territoires.Count;
            foreach (TerritoireDecorator Territoire in Territoires)
            {
                DrawRegion(Territoire);
                currentPosition += (100 / count);
                progress(currentPosition);
            }

            NotifyPropertyChanged("CarteCanvas");
            NotifyPropertyChanged("Carte");
        }

        /// <summary>
        /// Cancel the drawing
        /// </summary>
        public void CancelDrawRegion()
        {
            drawing = false;
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

            DrawNode(myImageBrush.Points, territoire);

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
        /// Draw node (Ellipse on each territoire)
        /// </summary>
        /// <param name="CarteCanvas">carte canvas</param>
        /// <param name="territoire">territoire</param>
        private void DrawNode(List<System.Windows.Point> points, ITerritoireBase territoire)
        {
            //Node Eclipse
            Ellipse eclipse = new Ellipse();
            eclipse.Width = 30;
            eclipse.Height = 30;
            eclipse.Fill = Brushes.White;
            eclipse.Stroke = Brushes.Blue;
            eclipse.StrokeThickness = 2;
            eclipse.IsHitTestVisible = true;
            Canvas.SetZIndex(eclipse, 3);
            Canvas.SetLeft(eclipse, ((points[0].X + points[1].X) / 2) - 15);
            Canvas.SetTop(eclipse, ((points[0].Y + points[2].Y) / 2) - 15);

            eclipse.ToolTip = new ToolTip() { Content = $"Name : {territoire.ID} x: {points[0].X} y: {points[0].Y}" };
            eclipse.MouseEnter += Eclipse_MouseEnter;
            eclipse.MouseLeave += Eclipse_MouseLeave;
            _carteCanvas.Children.Add(eclipse);
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

        private void Eclipse_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ToolTip el = ((ToolTip)(sender as Ellipse).ToolTip);
            el.StaysOpen = false;
            el.IsOpen = false;
        }

        private void Eclipse_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ToolTip el = ((ToolTip)(sender as Ellipse).ToolTip);
            el.StaysOpen = true;
            el.IsOpen = true;
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
            Canvas? c = sender as Canvas;
            DropShadowEffect shadow = new DropShadowEffect();

            shadow.Color = Brushes.Green.Color;
            c.Effect = shadow;


            this._carte.SelectedTerritoire = territoire;
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
