using JurassicRisk.ViewsModels;
using Models;
using Models.Player;
using Models.Settings;
using Models.Son;
using System.Threading.Tasks;
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
        public LobbyPage()
        {
            InitializeComponent();
            _lobbyVm = JurassicRiskViewModel.Get.LobbyVm;
            DataContext = _lobbyVm;
        }

        private async void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            
            SoundStore.Get("HubJurr.mp3").Stop();
            Settings.Get().Backgroundmusic = SoundStore.Get("MusicGameJurr.mp3");
            Settings.Get().Backgroundmusic.Volume = Settings.Get().Volume / 100;
            SoundStore.Get("MusicGameJurr.mp3").Play(true);
            if (_lobbyVm.Lobby.PlayersReady && _lobbyVm.Lobby.Owner == JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo)
            {


                //Retry Pattern Async
                var RetryTimes = 3;

                var WaitTime = 500;

                for (int i = 0; i < RetryTimes; i++)
                {
                    if (JurassicRiskViewModel.Get.IsConnected)
                    {
                        Error.Visibility = Visibility.Hidden;
                        await JurassicRiskViewModel.Get.LobbyVm.StartPartie(_lobbyVm.Lobby.Id, ProfilViewModel.Get.SelectedProfil.Pseudo, "carte");

                        break;
                    }
                    else
                    {
                        await JurasicRiskGameClient.Get.ConnectPartie();

                        if (i >= 2)
                        {
                            Error.Text = "is not connected";
                            Error.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            Error.Text = "Loading...";
                            Error.Visibility = Visibility.Visible;
                        }

                    }
                    //Wait for 500 milliseconds
                    await Task.Delay(WaitTime);
                }
            }
            else
            {
                Error.Text = "tous les joueur ne sont pas pret";
                Error.Visibility = Visibility.Visible;
            }
        }

        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (JurassicRiskViewModel.Get.JoueurVm.Joueur.Team != Teams.NEUTRE)
            {
                if (!JurassicRiskViewModel.Get.JoueurVm.Joueur.IsReady)
                    JurassicRiskViewModel.Get.JoueurVm.IsReady = "✅";
                else
                    JurassicRiskViewModel.Get.JoueurVm.IsReady = "❌";
            }
            else
            {
                Error.Text = " choisissez une equipe avant de vous mettre pret ";
                Error.Visibility = Visibility.Visible;
            }
        }

        private async void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            
            await _lobbyVm.ExitLobby();
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new MenuPage());
        }

        private async void SelectTeamButton_Click(object sender, RoutedEventArgs e)
        {
            
            Teams team = Teams.NEUTRE;
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
                        team = Teams.ROUGE;
                    }
                    else
                    {
                        b.Content = Ressource.Strings.Red;
                        Gbutton.IsEnabled = true;
                        Bbutton.IsEnabled = true;
                        Ybutton.IsEnabled = true;
                        team = Teams.NEUTRE;
                    }

                    break;
                case "Gbutton":
                    if (b.Content == Ressource.Strings.Green)
                    {
                        b.Content = Ressource.Strings.Green + '\n' + JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo;
                        Rbutton.IsEnabled = false;
                        Bbutton.IsEnabled = false;
                        Ybutton.IsEnabled = false;
                        team = Teams.VERT;
                    }
                    else
                    {
                        b.Content = Ressource.Strings.Green;
                        Rbutton.IsEnabled = true;
                        Bbutton.IsEnabled = true;
                        Ybutton.IsEnabled = true;
                        team = Teams.NEUTRE;
                    }
                    break;
                case "Bbutton":
                    if (b.Content == Ressource.Strings.Blue)
                    {
                        b.Content = Ressource.Strings.Blue + '\n' + JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo;
                        Rbutton.IsEnabled = false;
                        Gbutton.IsEnabled = false;
                        Ybutton.IsEnabled = false;
                        team = Teams.BLEU;
                    }
                    else
                    {
                        b.Content = Ressource.Strings.Blue;
                        Rbutton.IsEnabled = true;
                        Gbutton.IsEnabled = true;
                        Ybutton.IsEnabled = true;
                        team = Teams.NEUTRE;
                    }
                    break;
                case "Ybutton":
                    if (b.Content == Ressource.Strings.Yellow)
                    {
                        b.Content = Ressource.Strings.Yellow + '\n' + JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo;
                        Rbutton.IsEnabled = false;
                        Gbutton.IsEnabled = false;
                        Bbutton.IsEnabled = false;
                        team = Teams.JAUNE;
                    }
                    else
                    {
                        b.Content = Ressource.Strings.Yellow;
                        Rbutton.IsEnabled = true;
                        Gbutton.IsEnabled = true;
                        Bbutton.IsEnabled = true;
                        team = Teams.NEUTRE;
                    }
                    break;
            }
            JurassicRiskViewModel.Get.JoueurVm.Joueur.Team = team;
            await this._lobbyVm.SetTeam(team);
        }
    }
}
