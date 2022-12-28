using JurassicRisk.ViewsModels;
using System.Windows;
using System.Windows.Controls;

namespace JurassicRisk.Views
{
    /// <summary>
    /// Interaction logic for CreateLobbyPage.xaml
    /// </summary>
    public partial class CreateLobbyPage : Page
    {
        private LobbyViewModel _lobbyVm;
        public CreateLobbyPage()
        {
            InitializeComponent();
            _lobbyVm = new LobbyViewModel();
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string connexion = await _lobbyVm.CreateLobby(new Lobby(inputLobbyName.Text,inputPassword.Password));
            if (connexion == "lobby rejoint et refresh")
            {

                (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new LobbyPage(_lobbyVm));
            }
            else
            {
                Error.Text = connexion;
                Error.Visibility = Visibility.Visible;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new MenuPage());
        }
    }
}
