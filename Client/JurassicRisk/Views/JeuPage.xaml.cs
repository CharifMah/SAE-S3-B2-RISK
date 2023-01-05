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

        /// <summary>
        /// Page du jeux
        /// </summary>
        public JeuPage()
        {
            InitializeComponent();
            mainwindow = (Window.GetWindow(App.Current.MainWindow) as MainWindow);
            mainwindow.SizeChanged += JeuPage_SizeChanged;
            mainwindow.PreviewKeyDown += Mainwindow_PreviewKeyDown;
            ViewboxCanvas.Width = mainwindow.ActualWidth;
            ViewboxCanvas.Height = mainwindow.ActualHeight;

            DataContext = JurassicRiskViewModel.Get;
        }

        #region Events

        #region Async

        private async void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            SoundStore.Get("ClickButton.mp3").Play();
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new OptionsPage(this));
            await JurassicRiskViewModel.Get.CarteVm.SetCarte(JurassicRiskViewModel.Get.CarteVm.Carte);
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
            SoundStore.Get("ClickButton.mp3").Play();
            Resume();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            SoundStore.Get("ClickButton.mp3").Play();
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
            SoundStore.Get("ClickButton.mp3").Play();
            SoundStore.Get("MusicGameJurr.mp3").Stop();
            SoundStore.Get("HubJurr.mp3").Play(true);
            await JurasicRiskGameClient.Get.Disconnect();

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
