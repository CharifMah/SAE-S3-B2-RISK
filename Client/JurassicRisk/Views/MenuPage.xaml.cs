using JurassicRisk.ViewsModels;
using Models;
using Models.Son;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JurassicRisk.Views
{
    /// <summary>
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void ProfilButton_Click(object sender, RoutedEventArgs e)
        {
            
            (Window.GetWindow(App.Current.MainWindow) as MainWindow)?.frame.NavigationService.Navigate(new UserProfil());
        }

        private void DeconnectionButton_Click(object sender, RoutedEventArgs e)
        {
            
            ProfilViewModel.Get.SelectedProfil = null;
            JurassicRiskViewModel.Get.DestroyVm();
            JurasicRiskGameClient.Get.DestroyClient();
            (Window.GetWindow(App.Current.MainWindow) as MainWindow)?.frame.NavigationService.Navigate(new HomePage());
        }

        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            
            (Window.GetWindow(App.Current.MainWindow) as MainWindow)?.frame.NavigationService.Navigate(new OptionsPage(this));
        }

        private void JoinLobbyButton_Click(object sender, RoutedEventArgs e)
        {
            
            (Window.GetWindow(App.Current.MainWindow) as MainWindow)?.frame.NavigationService.Navigate(new JoinLobbyPage());
        }

        private void CreateLobbyButton_Click(object sender, RoutedEventArgs e)
        {
            
            (Window.GetWindow(App.Current.MainWindow) as MainWindow)?.frame.NavigationService.Navigate(new CreateLobbyPage());
        }
    }
}
