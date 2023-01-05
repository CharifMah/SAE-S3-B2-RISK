using JurassicRisk.ViewsModels;
using Models.Settings;
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

        public OptionsPage(Page OldPageName)
        {
            _save = new SauveCollection(Environment.CurrentDirectory);
            InitializeComponent();
            settingVm = new SettingsViewModel();
            this.DataContext = settingVm;
            Settings.Get().ActualPage = OldPageName;
            InitializeComponent();
            if (OldPageName.Name == "_JeuPage")
            {
                this.LangueComboBox.Visibility = Visibility.Hidden;
            }
            slider_Son.Value = settingVm.Volume;
            checkBoxSound.IsChecked = settingVm.MusiqueOnOff;
        }

        private void slider_Son_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            SoundStore.Get("checkbox.mp3").Play();
            settingVm.Volume = slider_Son.Value;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SoundStore.Get("ClickButton.mp3").Play();
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(Settings.Get().ActualPage);

            switch (Settings.Get().ActualPage.Name)
            {
                case "_MenuPage":
                    (Window.GetWindow(App.Current.MainWindow) as MainWindow)?.frame.NavigationService.Navigate(new MenuPage());
                    break;
                case "_HomePage":
                    (Window.GetWindow(App.Current.MainWindow) as MainWindow)?.frame.NavigationService.Navigate(new HomePage());
                    break;
                case "_JeuPage":
                    (Window.GetWindow(App.Current.MainWindow) as MainWindow)?.frame.NavigationService.GoBack();
                    break;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SoundStore.Get("ClickButton.mp3").Play();
            Settings.Get().SaveSettings();
        }
    }
}
