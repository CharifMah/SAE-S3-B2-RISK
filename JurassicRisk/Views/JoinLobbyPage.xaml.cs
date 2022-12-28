using JurassicRisk.ViewsModels;
using System.Windows;
using System.Windows.Controls;

namespace JurassicRisk.Views
{
    /// <summary>
    /// Interaction logic for JoinLobbyPage.xaml
    /// </summary>
    public partial class JoinLobbyPage : Page
    {
        public JoinLobbyPage()
        {
            InitializeComponent();
        }

        private async void JoinButton_Click(object sender, RoutedEventArgs e)
        {

            string connexion = await JurassicRiskViewModel.Get.LobbyVm.JoinLobby(inputLobbyName.Text);
            if (connexion == "lobby rejoint et refresh")
            {

                (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new LobbyPage());
            }
            else
            {
                Error.Text = connexion;
                Error.Visibility = Visibility.Visible;
            }

        }

        private async void BackButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new MenuPage());
        }
    }
}
