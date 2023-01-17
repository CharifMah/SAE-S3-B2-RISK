using JurassicRisk.Ressource;
using JurassicRisk.ViewsModels;
using System;
using System.Threading.Tasks;
using Models;
using System.Windows;
using System.Windows.Controls;
using Models.Son;
using Models.GameStatus;

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
                        JoinLobby(true);
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

        private async void JoinLobby(bool fromCreate = false)
        {
            try
            {
                //Retry Pattern Async
                var RetryTimes = 3;

                var WaitTime = 500;

                for (int i = 0; i < RetryTimes; i++)
                {
                    if (!JurasicRiskGameClient.Get.IsConnectedToPartie && JurasicRiskGameClient.Get.IsConnectedToLobby)
                    {
                        Error.Visibility = Visibility.Hidden;
                        await JurassicRiskViewModel.Get.LobbyVm.JoinLobby(inputLobbyName.Text, inputPassword.Password);
                        (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new LobbyPage());
                        i = RetryTimes;
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
            catch (Exception ex)
            {
                Error.Text = ex.Message;
                Error.Visibility = Visibility.Visible;
            }

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new MenuPage());
        }
    }
}
