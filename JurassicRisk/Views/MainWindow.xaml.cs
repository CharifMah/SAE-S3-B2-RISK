using DBStorage.Mysql;
using System.Windows;

namespace JurassicRisk.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            GestionDatabase connection = new GestionDatabase();
            connection.CreateDatabase();
            connection.CreateUserTable();
            InitializeComponent();
            frame.NavigationService.Navigate(new HomePage());
        }
    }
}
