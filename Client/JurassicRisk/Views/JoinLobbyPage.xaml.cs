using JurassicRisk.ViewsModels;
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
            SoundStore.Get("ClickButton.mp3").Play();
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

        private async void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SoundStore.Get("ClickButton.mp3").Play();
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new MenuPage());
        }
    }
}
