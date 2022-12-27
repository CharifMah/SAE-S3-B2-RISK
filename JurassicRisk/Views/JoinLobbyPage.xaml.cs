using JurassicRisk.ViewsModels;
using Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JurassicRisk.Views
{
    /// <summary>
    /// Interaction logic for JoinLobbyPage.xaml
    /// </summary>
    public partial class JoinLobbyPage : Page
    {
        private LobbyViewModel _lobbyVm;
        public JoinLobbyPage()
        {
            InitializeComponent();
            _lobbyVm = new LobbyViewModel();
        }

        private async void JoinButton_Click(object sender, RoutedEventArgs e)
        {

            string connexion = await _lobbyVm.JoinLobby(inputLobbyName.Text);
            if (connexion == "lobby rejoint et refresh")
            {

                (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new LobbyPage(_lobbyVm));
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
