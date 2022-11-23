using JurassicRisk.ViewsModels;
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
    /// Interaction logic for Connexion.xaml
    /// </summary>
    public partial class ConnexionPage : Page
    {
        private ProfilViewModel p;
        public ConnexionPage()
        {
            InitializeComponent();
            p = new ProfilViewModel();
            DataContext = p;
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            await p.SetSelectedProfil(inputPseudo.Text);

            if (p.SelectedProfil != null)
            {
                (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new MenuPage());
            }
            else
            {
                Error.Visibility = Visibility.Visible;
            }
           
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new HomePage());
        }
    }
}
