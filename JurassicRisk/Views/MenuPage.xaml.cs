using Models;
using Models.Son;
using System.Windows;
using System.Windows.Controls;

namespace JurassicRisk.Views
{
    /// <summary>
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();

        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            SoundStore.Get("JungleMusic.mp3").Stop();
            Settings.Get().Backgroundmusic = SoundStore.Get("DarkJungleMusic.mp3");
            Settings.Get().Backgroundmusic.Volume = Settings.Get().Volume / 100;
            SoundStore.Get("DarkJungleMusic.mp3").Play(true);
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new JeuPage());
        }

        private void ProfilButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new UserProfil());
        }

        private void DeconnectionButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new HomePage());
        }

        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new OptionsPage("_MenuPage"));
        }
    }
}
