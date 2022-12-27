using JurassicRisk.ViewsModels;
using Models;
using Models.Son;
using System.Windows;
using System.Windows.Controls;

namespace JurassicRisk.Views
{
    /// <summary>
    /// Interaction logic for LobbyPage.xaml
    /// </summary>
    public partial class LobbyPage : Page
    {
        private LobbyViewModel _lobbyVm;
        public LobbyPage(LobbyViewModel lobbyVm)
        {
            InitializeComponent();
            _lobbyVm = lobbyVm;
            DataContext= _lobbyVm;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            SoundStore.Get("JungleMusic.mp3").Stop();
            Settings.Get().Backgroundmusic = SoundStore.Get("DarkJungleMusic.mp3");
            Settings.Get().Backgroundmusic.Volume = Settings.Get().Volume / 100;
            SoundStore.Get("DarkJungleMusic.mp3").Play(true);
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new JeuPage());
        }

        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new MenuPage());
        }

        private void SelectTeamButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = (sender as Button);
            switch (b.Name)
            {
                case "Rbutton":

                    if (b.Content == Ressource.Strings.Red)
                    {
                        b.Content = Ressource.Strings.Red + '\n' + JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo;
                        Gbutton.IsEnabled = false;
                        Bbutton.IsEnabled = false;
                        Ybutton.IsEnabled = false;
                    }
                    else
                    {
                        b.Content = Ressource.Strings.Red;
                        Gbutton.IsEnabled = true;
                        Bbutton.IsEnabled = true;
                        Ybutton.IsEnabled = true;
                    }

                    break;
                case "Gbutton":
                    if (b.Content == Ressource.Strings.Green)
                    {
                        b.Content = Ressource.Strings.Green + '\n' + JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo;
                        Rbutton.IsEnabled = false;
                        Bbutton.IsEnabled = false;
                        Ybutton.IsEnabled = false;
                    }
                    else
                    {
                        b.Content = Ressource.Strings.Green;
                        Rbutton.IsEnabled = true;
                        Bbutton.IsEnabled = true;
                        Ybutton.IsEnabled = true;
                    }
                    break;
                case "Bbutton":
                    if (b.Content == Ressource.Strings.Blue)
                    {
                        b.Content = Ressource.Strings.Blue + '\n' + JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo;
                        Rbutton.IsEnabled = false;
                        Gbutton.IsEnabled = false;
                        Ybutton.IsEnabled = false;
                    }
                    else
                    {
                        b.Content = Ressource.Strings.Blue;
                        Rbutton.IsEnabled = true;
                        Gbutton.IsEnabled = true;
                        Ybutton.IsEnabled = true;
                    }
                    break;
                case "Ybutton":
                    if (b.Content == Ressource.Strings.Yellow)
                    {
                        b.Content = Ressource.Strings.Yellow + '\n' + JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo;
                        Rbutton.IsEnabled = false;
                        Gbutton.IsEnabled = false;
                        Bbutton.IsEnabled = false;
                    }
                    else
                    {
                        b.Content = Ressource.Strings.Yellow;
                        Rbutton.IsEnabled = true;
                        Gbutton.IsEnabled = true;
                        Bbutton.IsEnabled = true;
                    }
                    break;
            }
        }
    }
}
