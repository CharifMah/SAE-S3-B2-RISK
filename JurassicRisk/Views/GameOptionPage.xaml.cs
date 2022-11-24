using JurassicRisk.ViewsModels;
using Models;
using Stockage;
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
    /// Interaction logic for GameOptionPage.xaml
    /// </summary>
    public partial class GameOptionPage : Page
    {
        SettingsViewModel settingVm;
        SauveCollection _save;
        public GameOptionPage()
        {
            InitializeComponent();
            _save = new SauveCollection(Environment.CurrentDirectory);
            InitializeComponent();
            settingVm = new SettingsViewModel();
            this.DataContext = settingVm;
        }

        private void LangueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void slider_Son_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {

        }

        private void BackToGameButton_Click(object sender, RoutedEventArgs e)
        {
            _save.Sauver(Settings.Get(), "Settings");

            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new JeuPage());
        }
    }
}
