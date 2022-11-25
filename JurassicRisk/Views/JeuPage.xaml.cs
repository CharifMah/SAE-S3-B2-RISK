using JurassicRisk.ViewsModels;
using Models;
using Models.Son;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JurassicRisk.Views
{
    /// <summary>
    /// Logique d'interaction pour Jeu.xaml
    /// </summary>
    public partial class JeuPage : Page
    {
        private Window mainwindow;

        public JeuPage()
        {
            InitializeComponent();
            mainwindow = (Window.GetWindow(App.Current.MainWindow) as MainWindow);
            mainwindow.SizeChanged += JeuPage_SizeChanged;
            mainwindow.PreviewKeyDown += Mainwindow_PreviewKeyDown;
            ViewboxCanvas.Width = mainwindow.ActualWidth;
            ViewboxCanvas.Height = mainwindow.ActualHeight;
            DataContext = new JurassicRiskViewModel();
        }

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

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            SoundStore.Get("DarkJungleMusic.mp3").Stop();
            SoundStore.Get("JungleMusic.mp3").Play(true);
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

        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new OptionsPage("_JeuPage"));
        }
    }
}
