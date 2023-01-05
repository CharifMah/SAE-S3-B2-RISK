
using JurassicRisk.ViewsModels;
using Models;
using Models.Player;
using Models.Son;
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
            DataContext = ProfilViewModel.Get;
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            Profil profil = new Profil(inputPseudo.Text, inputPassword.Password);
            string connexion = await ProfilViewModel.Get.Connexion(profil);


            SoundStore.Get("ClickButton.mp3").Play();

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
            SoundStore.Get("ClickButton.mp3").Play();
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new HomePage());
        }
    }
}
