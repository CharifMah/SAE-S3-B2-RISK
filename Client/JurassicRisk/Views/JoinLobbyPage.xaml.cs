using JurassicRisk.ViewsModels;
using Microsoft.AspNetCore.SignalR.Client;
using System;
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
                bool connection = await JurassicRiskViewModel.Get.LobbyVm.JoinLobby(inputLobbyName.Text);
                if (connection)
                {
                    (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new LobbyPage());
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
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new MenuPage());
        }
    }
}
