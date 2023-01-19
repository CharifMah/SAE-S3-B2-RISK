using JurassicRisk.Ressource;
using JurassicRisk.ViewsModels;
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

                Error.Visibility = Visibility.Hidden;
                await JurassicRiskViewModel.Get.LobbyVm.JoinLobby(inputLobbyName.Text, inputPassword.Password);

                for (int i = 0; i < RetryTimes; i++)
                {

                    if (!JurassicRiskViewModel.Get.PartieVm.IsConnectedToPartie && JurassicRiskViewModel.Get.LobbyVm.IsConnectedToLobby)
                    {
                        if (JurassicRiskViewModel.Get.LobbyVm.Lobby != null)
                            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new LobbyPage());
                        i = RetryTimes;
                        break;
                    }
                    else
                    {
                        if (JurassicRiskViewModel.Get.PartieVm.IsConnectedToPartie && !JurassicRiskViewModel.Get.LobbyVm.IsConnectedToLobby)
                        {
                            await JurassicRiskViewModel.Get.PartieVm.DisconnectPartie();
                        }

                        if (i >= 2)
                        {
                            Error.Text = Strings.NotConnect;
                            Error.Visibility = Visibility.Visible;
                            if (JurassicRiskViewModel.Get.LobbyVm.Lobby != null)
                                (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new LobbyPage());
                        }
                        else
                        {
                            Error.Text = Strings.Loading;
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
