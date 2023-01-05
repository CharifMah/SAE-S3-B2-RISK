using JurassicRisk.ViewsModels;
using Models;
using Models.Son;
using System.Windows;
using System.Windows.Controls;

namespace JurassicRisk.Views
{
    /// <summary>
    /// Interaction logic for CreateLobbyPage.xaml
    /// </summary>
    public partial class CreateLobbyPage : Page
    {
        public CreateLobbyPage()
        {
            InitializeComponent();
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string connexion = await JurassicRiskViewModel.Get.LobbyVm.CreateLobby(new Lobby(inputLobbyName.Text,inputPassword.Password));
            SoundStore.Get("ClickButton.mp3").Play();

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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SoundStore.Get("ClickButton.mp3").Play();
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new MenuPage());
        }
    }
}
