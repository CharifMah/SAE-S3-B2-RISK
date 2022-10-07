﻿using JurassicRisk.ViewModels;
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
            if(await MainViewModel.Get().VerifProfilCreation(inputPseudo.Text))
            {
                MessageBox.Show("Ce pseudo existe déjà !");
            }
            else
            {
                await MainViewModel.Get().CreateProfil(new Models.Profil(inputPseudo.Text));
            }
            


        }
    }
}
