using JurassicRisk.Ressource;
using JurassicRisk.ViewsModels;
using Models.GameStatus;
using System;
using System.Threading.Tasks;
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
            if (inputLobbyName.Text != "")
            {
                if (inputPassword.Password == inputPassword2.Password)
                {
                    string connexion1 = await JurassicRiskViewModel.Get.LobbyVm.CreateLobby(new Lobby(inputLobbyName.Text, inputPassword.Password));

                    if (connexion1.Contains("Lobby Created with name"))
                    {
                        await JoinLobby(true);
                    }
                    else
                    {
                        Error.Text = connexion1;
                        Error.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    Error.Text = Strings.PasswordNotMatch;
                    Error.Visibility = Visibility.Visible;
                }
            }
            else
            {
                Error.Text = Strings.NoPseudoEnter;
                Error.Visibility = Visibility.Visible;
            }
        }

        private async Task JoinLobby(bool fromCreate = false)
        {
            try
            {
                var WaitTime = 500;

                Error.Visibility = Visibility.Hidden;
                await JurassicRiskViewModel.Get.LobbyVm.JoinLobby(inputLobbyName.Text, inputPassword.Password);
                await Task.Delay(WaitTime);

                if (JurassicRiskViewModel.Get.LobbyVm.IsConnectedToLobby && !JurassicRiskViewModel.Get.PartieVm.IsConnectedToPartie)
                {
                    if (JurassicRiskViewModel.Get.LobbyVm.Lobby != null)
                        (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new LobbyPage());
                }
                else
                {
                    if (!JurassicRiskViewModel.Get.LobbyVm.IsConnectedToLobby && JurassicRiskViewModel.Get.PartieVm.IsConnectedToPartie)
                    {
                        await JurassicRiskViewModel.Get.PartieVm.StopConnection();
                    }
                    else
                    {
                        if (JurassicRiskViewModel.Get.LobbyVm.Lobby != null && JurassicRiskViewModel.Get.LobbyVm.IsConnectedToLobby)
                        {
                            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new LobbyPage());
                        }
                        else
                        {
                            Error.Visibility = Visibility.Visible;
                            Error.Text = "Lobby don't exist";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Error.Visibility = Visibility.Visible;
                Error.Text = ex.Message;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new MenuPage());
        }
    }
}
