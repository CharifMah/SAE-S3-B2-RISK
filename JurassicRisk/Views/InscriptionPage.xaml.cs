﻿using JurassicRisk.Ressource;
using JurassicRisk.ViewsModels;
using Models;
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
    /// Interaction logic for Inscription.xaml
    /// </summary>
    public partial class InscriptionPage : Page
    {
        public InscriptionPage()
        {
            InitializeComponent();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if(!await ProfilViewModel.Instance.VerifProfilCreation(inputPseudo.Text))
            {
                if (inputPseudo.Text != "")
                {
                    await ProfilViewModel.Instance.CreateProfil(new Models.Profil(inputPseudo.Text));
                    (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new MenuPage());
                    await ProfilViewModel.Instance.SetSelectedProfil(inputPseudo.Text);
                }
                else
                {
                    Error.Text = Strings.NoPseudoEnter;
                    Error.Visibility = Visibility.Visible;
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new HomePage());
        }
    }
}
