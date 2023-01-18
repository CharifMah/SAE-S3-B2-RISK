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
            (Window.GetWindow(App.Current.MainWindow) as MainWindow)?.frame.NavigationService.Navigate(new MenuPage());
        }
    }
}
