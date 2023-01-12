using JurassicRisk.ViewsModels;
using Models;
using Models.Son;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using IUnit = Models.Units.IUnit;

namespace JurassicRisk.Views
{
    /// <summary>
    /// Logique d'interaction pour Jeu.xaml
    /// </summary>
    public partial class JeuPage : Page
    {
        private Window mainwindow;
        private static ScrollViewer _scrollviewer;

        public static ScrollViewer ScrollViewer
        {
            get { return _scrollviewer; }
        }

        /// <summary>
        /// Page du jeux
        /// </summary>
        public JeuPage()
        {
            InitializeComponent();
            mainwindow = (Window.GetWindow(App.Current.MainWindow) as MainWindow);
            mainwindow.SizeChanged += JeuPage_SizeChanged;
            mainwindow.PreviewKeyDown += Mainwindow_PreviewKeyDown;

            _scrollviewer = ScrollViewerView;
            ViewboxCanvas.Width = mainwindow.ActualWidth;
            ViewboxCanvas.Height = mainwindow.ActualHeight;


            DataContext = JurassicRiskViewModel.Get;
        }


        /// <summary>
        /// move the camera to point x,y
        /// </summary>
        /// <param name="x">axis x</param>
        /// <param name="y">axis y</param>
        /// <Author>Charif</Author>
        public static void MoveCamera(double x, double y)
        {
            JeuPage.ScrollViewer.ScrollToVerticalOffset(y - Application.Current.MainWindow.ActualHeight / 2);
            JeuPage.ScrollViewer.ScrollToHorizontalOffset(x - Application.Current.MainWindow.ActualWidth / 2);
        }

        #region Events

        #region Async

        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {     
            (Window.GetWindow(App.Current.MainWindow) as MainWindow)?.frame.NavigationService.Navigate(new OptionsPage(this));
        }

        #endregion

        private void Mainwindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                bool Pressed = false;
                //Pause
                if (GroupBoxPause.Visibility == Visibility.Hidden && !Pressed)
                {
                    Pause();
                    Pressed = true;
                }
                //Resume
                if (GroupBoxPause.Visibility == Visibility.Visible && !Pressed)
                {
                    Resume();
                    Pressed = true;
                }
            }
        }

        private void JeuPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ViewboxCanvas.Width = mainwindow.ActualWidth;
            ViewboxCanvas.Height = mainwindow.ActualHeight;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            
            Resume();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            
            bool Pressed = false;
            //Pause
            if (GroupBoxPause.Visibility == Visibility.Hidden && !Pressed)
            {
                Pause();
                Pressed = true;
            }
            //Resume
            if (GroupBoxPause.Visibility == Visibility.Visible && !Pressed)
            {
                Resume();
                Pressed = true;
            }
        }

        private async void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            
            SoundStore.Get("MusicGameJurr.mp3").Stop();
            SoundStore.Get("HubJurr.mp3").Play(true);
            await JurassicRiskViewModel.Get.LobbyVm.ExitLobby();
            JurassicRiskViewModel.Get.DestroyVm();

            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new MenuPage());
        }

        private void Pause()
        {
            GroupBoxPause.Visibility = Visibility.Visible;
            ViewboxCanvas.IsEnabled = false;
        }

        private void Resume()
        {
            GroupBoxPause.Visibility = Visibility.Hidden;
            ViewboxCanvas.IsEnabled = true;
        }

        private void ListBoxUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            JurassicRiskViewModel.Get.JoueurVm.SelectedUnit = (sender as ListBox).SelectedItem as IUnit;
        }

        #endregion

    }
}
