using JurassicRisk.observable;
using Models.Graph.lieux;
using Models.Graph.parseur;
using Models.Map;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace JurassicRisk.ViewsModels
{
    /// <summary>
    /// Vue modèle des graphes (Lieux et routes)
    /// </summary>
    public class GraphesViewModel : Observable
    {
        #region Attributes
        private CarteViewModel _carteVm;
        private Canvas _gphCanvas;
        private readonly ScaleTransform layoutTransform;
        private readonly Dictionary<Route, Line> lignes;
        ///<summary>Liste des fichiers pour la combobox</summary>
        private ObservableCollection<string> listeFichiers;

        ///<summary>Liste des lieux du graphe en cours</summary>
        private Dictionary<string, Lieu> listeLieux;

        ///<summary>Liste des routes du graphe en cours</summary>
        private List<Route> listeRoutes;

        /// <summary>Fichier sélectionné dans la combobox</summary>
        private string fichierSelectionne;
        private string fichierSelectionneMem;
        /// <summary>Le fichier doit-il être chargé ?</summary>
        private bool doitCharger;

        #endregion

        #region Property

        public ObservableCollection<string> ListeFichiers => listeFichiers;

        public Dictionary<string, Lieu> ListeLieux => listeLieux;

        public List<Route> ListeRoutes => listeRoutes;

        /// <summary>Fichier sélectionné pour le graphe </summary>
        public string FichierSelectionne
        {
            get => fichierSelectionne;
            set
            {
                if (fichierSelectionne != null) fichierSelectionneMem = fichierSelectionne;
                fichierSelectionne = value;
                ChargementFichier();
                NotifyPropertyChanged();
            }
        }

        public Canvas GphCanvas { get => _gphCanvas; }


        #endregion

        /// <summary>Constructeur par défaut</summary>
        public GraphesViewModel(CarteViewModel carteVm)
        {
            _carteVm = carteVm;
            //Initialisation
            listeLieux = new Dictionary<string, Lieu>();
            listeRoutes = new List<Route>();
            listeFichiers = new ObservableCollection<string>();
            fichierSelectionneMem = "";
            doitCharger = true;

            this.layoutTransform = new ScaleTransform();
            this.lignes = new Dictionary<Route, Line>();

            //Chargement de la liste des fichiers de graphes
            UpdateListeFichiers();

            //Creation d'un watcher 
            string chemin = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Ressources\";
            FileSystemWatcher watcher = new FileSystemWatcher(chemin, "*.gph");
            watcher.Created += Watcher_Created;
            watcher.Deleted += Watcher_Deleted;
            watcher.Renamed += Watcher_Renamed;
            watcher.EnableRaisingEvents = true;


            this.FichierSelectionne = "GrapheSimple1.gph";
            _gphCanvas = CreateGphCanvas();
            NotifyPropertyChanged("GphCanvas");

        }

        #region Events

        /// <summary>Suppression d'un fichier-Graphe</summary>
        /// <param name="sender">Watcher</param>
        /// <param name="e">Le fichier supprimé</param>
        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                doitCharger = false;
                UpdateListeFichiers();
                if (fichierSelectionneMem.Equals(e.Name))
                {
                    doitCharger = true;
                    FichierSelectionne = null;
                }
                else FichierSelectionne = fichierSelectionneMem;
                doitCharger = true;
            });
        }

        /// <summary>Renommage d'un fichier-Graphe</summary>
        /// <param name="sender">Watcher</param>
        /// <param name="e">Le fichier renommé</param>
        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                doitCharger = false;
                if (e.OldName.Equals(fichierSelectionne))
                {
                    FichierSelectionne = e.Name;
                }
                UpdateListeFichiers();
                FichierSelectionne = fichierSelectionneMem;
                doitCharger = true;
            });
        }

        /// <summary>Création d'un nouveau fichier-Graphe</summary>
        /// <param name="sender">Watcher</param>
        /// <param name="e">Nouveau fichier</param>
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                doitCharger = false;
                UpdateListeFichiers();
                FichierSelectionne = fichierSelectionneMem;
                doitCharger = true;
            });
        }


        #endregion

        private Canvas CreateGphCanvas()
        {
            Canvas c = new Canvas();

            int maxX = 0;
            int maxY = 0;
            int minX = Int32.MaxValue;
            int minY = Int32.MaxValue;
            foreach (Continent continent in _carteVm.Carte.DicoContinents.Values)
            {
                foreach (TerritoireDecorator territoire in continent.DicoTerritoires.Values)
                {
                    int x = territoire.X + (territoire.Width / 2);
                    int y = territoire.Y + (territoire.Height / 2);

                    minX = Math.Min(minX, x);
                    minY = Math.Min(minY, y);

                    Ellipse ellipse = new Ellipse();
                    ellipse.Width = 30;
                    ellipse.Height = 30;
                    ellipse.Fill = Brushes.AliceBlue; 
                    ellipse.Stroke = Brushes.Black; 
                    ellipse.StrokeThickness = 2;

                    Canvas.SetLeft(ellipse, x);
                    Canvas.SetTop(ellipse, y);
                    c.Children.Add(ellipse);
                }
            }


            c.Width = maxX;
            c.Height = maxY;

            if (maxX > 0 && maxY > 0)
            {
                this.layoutTransform.ScaleX = Math.Min(1100.0 / maxX, 600.0 / maxY);
                this.layoutTransform.ScaleY = Math.Min(1100.0 / maxX, 600.0 / maxY);
            }

            return c;
        }

        /// <summary>Mise-à-jour de la liste des fichiers</summary>
        private void UpdateListeFichiers()
        {
            //Récupération des noms de fichiers
            string chemin = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Ressources\";
            string filtre = "*.gph";
            string[] fichiers = Directory.GetFiles(chemin, filtre);

            //Mise à jour de la liste
            listeFichiers.Clear();
            foreach (string fichier in fichiers)
            {
                string nomFichier = Path.GetFileName(fichier);
                listeFichiers.Add(nomFichier);
            }
        }

        /// <summary>Chargement du fichier</summary>
        private void ChargementFichier()
        {
            if (doitCharger)
            {
                ListeLieux.Clear();
                ListeRoutes.Clear();
                if (fichierSelectionne != null)
                {
                    //Parsage du fichier et mise-à-jour des listes de lieux
                    Parseur parseur = new Parseur(fichierSelectionne);
                    parseur.Parser();
                    listeLieux = parseur.ListeLieux;
                    listeRoutes = parseur.ListeRoutes;
                    NotifyPropertyChanged("GphCanvas");
                }
            }
        }
    }
}
