using JurassicRisk.Ressource;
using JurassicRisk.ViewsModels;
using Models.Player;
using Models.Settings;
using Models.Son;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static JurassicRisk.ViewsModels.CarteViewModel;

namespace JurassicRisk.Views
{
    /// <summary>
    /// Interaction logic for LobbyPage.xaml
    /// </summary>
    public partial class LobbyPage : Page
    {
        private int _partieConnect = -1;
        private LobbyViewModel _lobbyVm;

        public LobbyPage()
        {
            InitializeComponent();
            _lobbyVm = JurassicRiskViewModel.Get.LobbyVm;
            DataContext = _lobbyVm;
            progression.DataContext = JurassicRiskViewModel.Get.PartieVm;
            _partieConnect = -1;
        }

        private async void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            SoundStore.Get("HubJurr.mp3").Stop();
            Settings.Get().Backgroundmusic = SoundStore.Get("MusicGameJurr.mp3");
            Settings.Get().Backgroundmusic.Volume = Settings.Get().Volume / 100;
            SoundStore.Get("MusicGameJurr.mp3").Play(true);
            SoundStore.Get("MusicGameJurr.mp3");

            if (_partieConnect == -1)
            {
                await JurassicRiskViewModel.Get.PartieVm.ConnectPartie();
                await Task.Delay(1000);
                _partieConnect = 0;
            }

            try
            {
                if (_partieConnect == 0 && ProfilViewModel.Get.SelectedProfil.Pseudo == _lobbyVm.Lobby.Owner)
                {
                    await JurassicRiskViewModel.Get.PartieVm.StartPartie(_lobbyVm.Lobby.Id, ProfilViewModel.Get.SelectedProfil.Pseudo, "carte");
                    _partieConnect = 1;
                }

                //Retry Pattern Async
            }
            catch (Exception ex)
            {
                Error.Text = ex.Message;
                Error.Visibility = Visibility.Visible;
            }
        }

        private async void ReadyButton_Click(object sender, RoutedEventArgs e)
        {

            if (JurassicRiskViewModel.Get.JoueurVm.Joueur.Team != Teams.NEUTRE)
            {
                if (!JurassicRiskViewModel.Get.JoueurVm.Joueur.IsReady)
                {
                    JurassicRiskViewModel.Get.JoueurVm.IsReady = "✅";
                    Error.Visibility = Visibility.Visible;
                    Error.Text = Strings.PlayerReady;

                    if (_partieConnect != 0  && ProfilViewModel.Get.SelectedProfil.Pseudo != _lobbyVm.Lobby.Owner)
                    {
                        await JurassicRiskViewModel.Get.PartieVm.ConnectPartie();
                        _partieConnect = 1;
                    }

                }
                else
                {
                    Error.Visibility = Visibility.Hidden;
                    JurassicRiskViewModel.Get.JoueurVm.IsReady = "❌";
                }

            }
            else
            {
                Error.Visibility = Visibility.Visible;
                Error.Text = Strings.ErrorTeamsForReady;
            }
        }

        private async void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            await _lobbyVm.StopConnection();
            _partieConnect = -1;
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new MenuPage());
        }

        private async void SelectTeamButton_Click(object sender, RoutedEventArgs e)
        {
            Teams team = Teams.NEUTRE;
            Button b = (sender as Button);
            switch (b.Name)
            {
                case "Rbutton":

                    if (b.Content == Strings.Red)
                    {
                        b.Content = Strings.Red + '\n' + JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo;
                        Gbutton.IsEnabled = false;
                        Bbutton.IsEnabled = false;
                        Ybutton.IsEnabled = false;
                        team = Teams.ROUGE;
                    }
                    else
                    {
                        b.Content = Strings.Red;
                        Gbutton.IsEnabled = true;
                        Bbutton.IsEnabled = true;
                        Ybutton.IsEnabled = true;
                        team = Teams.NEUTRE;
                    }

                    break;
                case "Gbutton":
                    if (b.Content == Strings.Green)
                    {
                        b.Content = Ressource.Strings.Green + '\n' + JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo;
                        Rbutton.IsEnabled = false;
                        Bbutton.IsEnabled = false;
                        Ybutton.IsEnabled = false;
                        team = Teams.VERT;
                    }
                    else
                    {
                        b.Content = Strings.Green;
                        Rbutton.IsEnabled = true;
                        Bbutton.IsEnabled = true;
                        Ybutton.IsEnabled = true;
                        team = Teams.NEUTRE;
                    }
                    break;
                case "Bbutton":
                    if (b.Content == Strings.Blue)
                    {
                        b.Content = Ressource.Strings.Blue + '\n' + JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo;
                        Rbutton.IsEnabled = false;
                        Gbutton.IsEnabled = false;
                        Ybutton.IsEnabled = false;
                        team = Teams.BLEU;
                    }
                    else
                    {
                        b.Content = Strings.Blue;
                        Rbutton.IsEnabled = true;
                        Gbutton.IsEnabled = true;
                        Ybutton.IsEnabled = true;
                        team = Teams.NEUTRE;
                    }
                    break;
                case "Ybutton":
                    if (b.Content == Strings.Yellow)
                    {
                        b.Content = Ressource.Strings.Yellow + '\n' + JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo;
                        Rbutton.IsEnabled = false;
                        Gbutton.IsEnabled = false;
                        Bbutton.IsEnabled = false;
                        team = Teams.JAUNE;
                    }
                    else
                    {
                        b.Content = Strings.Yellow;
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
