using JurassicRisk.ViewsModels;
using Models.Son;
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
    /// Logique d'interaction pour UserProfil.xaml
    /// </summary>
    public partial class UserProfil : Page
    {
        public UserProfil()
        {
            InitializeComponent();
            this.DataContext = ProfilViewModel.Instance;
            DataGrid1.DataContext = new StatsViewModel();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new MenuPage());
        }
    }
}
