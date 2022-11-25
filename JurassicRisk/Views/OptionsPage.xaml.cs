using JurassicRisk.ViewsModels;
using Models;
using Models.Son;
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
        public OptionsPage()
        {
            SoundStore.Get("Click.mp3").Stop();
            _save = new SauveCollection(Environment.CurrentDirectory);
            settingVm = new SettingsViewModel();
            this.DataContext = settingVm;
            InitializeComponent();
        }

        private void LangueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void slider_Son_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            Settings.Get().SliderMusic.Volume = slider_Son.Value;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _save.Sauver(settingVm.Settings, "Settings");
            
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new MenuPage());
        }
    }
}
