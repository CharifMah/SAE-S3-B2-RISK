using JurassicRisk.ViewsModels;
using Models;
using Stockage;
using System;
using System.Windows;
using System.Windows.Controls;

namespace JurassicRisk.Views
{
    /// <summary>
    /// Interaction logic for OptionsPage.xaml
    /// </summary>
    public partial class OptionsPage : Page
    {
        SettingsViewModel settingVm;
        SauveCollection _save;

        public OptionsPage(string OldPageName)
        {
            _save = new SauveCollection(Environment.CurrentDirectory);
            InitializeComponent();
            settingVm = new SettingsViewModel();
            this.DataContext = settingVm;
            Settings.Get().ActualPageName = OldPageName;
        }

        private void LangueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void slider_Son_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _save.Sauver(Settings.Get(), "Settings");

            switch (Settings.Get().ActualPageName)
            {
                case "_MenuPage":
                    (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new MenuPage());
                    this.LangueComboBox.Visibility = Visibility.Visible;
                    break;
                case "_HomePage":
                    (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new HomePage());
                    this.LangueComboBox.Visibility = Visibility.Visible;
                    break;
                case "_JeuPage":
                    (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new JeuPage());
                    this.LangueComboBox.Visibility = Visibility.Hidden;
                    break;
            }
           
        }
          
    }
}
