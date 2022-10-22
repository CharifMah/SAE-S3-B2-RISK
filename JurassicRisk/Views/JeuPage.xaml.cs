﻿using JurassicRisk.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JurassicRisk.Views
{
    /// <summary>
    /// Logique d'interaction pour Jeu.xaml
    /// </summary>
    public partial class JeuPage : Page
    {
        private Window mainwindow;

        public JeuPage()
        {
            InitializeComponent();
            mainwindow = (Window.GetWindow(App.Current.MainWindow) as MainWindow);
            mainwindow.SizeChanged += JeuPage_SizeChanged;
            ViewboxCanvas.Width = mainwindow.ActualWidth;
            ViewboxCanvas.Height = mainwindow.ActualHeight;
            CarteCanvas.DataContext = new ViewModelCarte();
        }

        private void JeuPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ViewboxCanvas.Width = mainwindow.ActualWidth;
            ViewboxCanvas.Height = mainwindow.ActualHeight;
        }
    }
}
