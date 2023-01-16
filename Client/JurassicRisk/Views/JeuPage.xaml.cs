using JurassicRisk.ViewsModels;
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
        private Window _mainwindow;
        private static JeuPage _instance;
        public static JeuPage GetInstance() { return _instance; }

        /// <summary>
        /// Page du jeux
        /// </summary>
        public JeuPage()
        {
            InitializeComponent();
            _mainwindow = (Window.GetWindow(App.Current.MainWindow) as MainWindow);
            _mainwindow.SizeChanged += JeuPage_SizeChanged;
            _mainwindow.PreviewKeyDown += Mainwindow_PreviewKeyDown;
            DataContext = JurassicRiskViewModel.Get;
            _instance = this;
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
            ViewboxCanvas.Width = _mainwindow.ActualWidth;
            ViewboxCanvas.Height = _mainwindow.ActualHeight;
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

        
        private void FinTourButton_Click(object sender, RoutedEventArgs e)
        {
            JurassicRiskViewModel.Get.SendEndTurn();
        }

        #endregion

        /// <summary>
        /// ZoomIn Canvas from x and y position on canvas
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        public void ZoomIn(int x, int y, double scale = 1.1)
        {
            // set the center point for the zoom
            transform.CenterX = x;
            transform.CenterY = y;

            if (transform.ScaleX != scale)
            {
                // Calculate the new offset, so that the point (x, y) is at the center of the viewport after the zoom
                double newHorizontalOffset = x - ScrollViewerView.HorizontalOffset;
                double newVerticalOffset = y - ScrollViewerView.VerticalOffset;

                // Set the new offset
                ScrollViewerView.ScrollToHorizontalOffset(newHorizontalOffset);
                ScrollViewerView.ScrollToVerticalOffset(newVerticalOffset);
            }


            // set the new scale
            transform.ScaleX = scale;
            transform.ScaleY = scale;


  

        }

        /// <summary>
        /// ZoomOut Canvas from x and y position on canvas
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        public void ZoomOut(int x, int y, double scale = 1.1)
        {
            double sc = 1 / scale;
            transform.CenterX = x;
            transform.CenterY = y;

            transform.ScaleX = sc;
            transform.ScaleY = sc;
        }

        public void ResetZoom()
        {
            transform.CenterX = 0.5;
            transform.CenterY = 0.5;
            transform.ScaleX = 1;
            transform.ScaleY = 1;
        }
    }
}
