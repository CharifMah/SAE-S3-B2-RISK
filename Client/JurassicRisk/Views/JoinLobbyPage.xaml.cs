using JurassicRisk.ViewsModels;
using Models;
using Models.Son;
using System;
using System.Threading.Tasks;
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
                        if (JurasicRiskGameClient.Get.IsConnectedToPartie && !JurasicRiskGameClient.Get.IsConnectedToLobby)
                        {
                            await JurasicRiskGameClient.Get.DisconnectPartie();
                        }
                        await JurasicRiskGameClient.Get.ConnectLobby();


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
            (Window.GetWindow(App.Current.MainWindow) as MainWindow)?.frame.NavigationService.Navigate(new MenuPage());
        }
    }
}
