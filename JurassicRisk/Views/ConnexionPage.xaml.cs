
using JurassicRisk.ViewsModels;
using Models.Player;
using System.Windows;
using System.Windows.Controls;

namespace JurassicRisk.Views
{
    /// <summary>
    /// Interaction logic for Connexion.xaml
    /// </summary>
    public partial class ConnexionPage : Page
    {
        public ConnexionPage()
        {
            InitializeComponent();
            DataContext = ProfilViewModel.Instance;
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            Profil profil = new Profil(inputPseudo.Text, inputPassword.Password);
            string connexion = await ProfilViewModel.Instance.SetSelectedProfil(profil);

            if (connexion == "Ok")
            {
                (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new MenuPage());
            }
            else
            {
                Error.Text = connexion;
                Error.Visibility = Visibility.Visible;
            }

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new HomePage());
        }
    }
}
