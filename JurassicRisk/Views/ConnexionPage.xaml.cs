﻿using JurassicRisk.ViewsModels;
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
        public ConnexionPage()
        {
            InitializeComponent();
            DataContext = ProfilViewModel.Instance;
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            await ProfilViewModel.Instance.SetSelectedProfil(inputPseudo.Text);

            if (ProfilViewModel.Instance.SelectedProfil != null)
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
