﻿using JurassicRisk.ViewsModels;
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
        public OptionsPage()
        {
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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _save.Sauver(Settings.Get(), "Settings");

            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new MenuPage());     
        }
          
    }
}
