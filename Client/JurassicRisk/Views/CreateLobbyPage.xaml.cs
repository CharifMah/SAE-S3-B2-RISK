using JurassicRisk.Ressource;
using JurassicRisk.ViewsModels;
using System;
using System.Threading.Tasks;
using Models;
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
            string connexion = await JurassicRiskViewModel.Get.LobbyVm.CreateLobby(new Lobby(inputLobbyName.Text,inputPassword.Password));
            SoundStore.Get("ClickButton.mp3").Play();

            if (connexion == "lobby rejoint et refresh")
            {
                if (inputPassword.Password == inputPassword2.Password)
                {
                    string connexion = await JurassicRiskViewModel.Get.LobbyVm.CreateLobby(new Lobby(inputLobbyName.Text, inputPassword.Password));

                    if (connexion.Contains("Lobby Created with name"))
                    {
                        JoinLobby();
                    }
                    else
                    {
                        Error.Text = connexion;
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

        private async void JoinLobby()
        {
            try
            {
                await JurassicRiskViewModel.Get.LobbyVm.JoinLobby(inputLobbyName.Text, inputPassword.Password);

                //Retry Pattern Async
                var RetryTimes = 3;

                var WaitTime = 500;

                for (int i = 0; i < RetryTimes; i++)
                {
                    if (JurassicRiskViewModel.Get.LobbyVm.IsConnectedToLobby)
                    {
                        (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new LobbyPage());
                        break;
                    }
                    else
                    {
                        Error.Text = Ressource.Strings.NoExistLobby;
                        Error.Visibility = Visibility.Visible;
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
            SoundStore.Get("ClickButton.mp3").Play();
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new MenuPage());
        }
    }
}
