using JurassicRisk.Views;
using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace JurassicRisk.ViewsModels
{
    public class SettingsViewModel : observable.Observable
    {
        
        /// <summary>
        /// True if full screen else False
        /// </summary>
        public bool PleinEcran
        {
            get { return Settings.Get().PleinEcran; }
            set
            {
                Settings.Get().PleinEcran = value;
                MainWindow m = (Window.GetWindow(App.Current.MainWindow) as MainWindow);
                if (Settings.Get().PleinEcran)
                {
                    m.WindowStyle = WindowStyle.None;
                    m.WindowState = WindowState.Maximized;
                }
                else
                {
                    m.WindowState = WindowState.Normal;
                    m.WindowStyle = WindowStyle.ThreeDBorderWindow;
                }
                this.NotifyPropertyChanged("PLeinEcran");
            }
        }

        public string Culturename
        {
            get { return Settings.Get().Culturename; }
            set
            {

                Settings.Get().Culturename = value;
                ChangeLanguage(value);
                this.NotifyPropertyChanged("Culturename");

            }
        }

        public List<string> Culturenames
        {
            get
            {
                return Settings.Get().AvailableCutlures;
            }
        }

        public SettingsViewModel()
        {
            this.NotifyPropertyChanged("PLeinEcran");
            this.NotifyPropertyChanged("Culturename");
        }

        /// <summary>
        /// Changes the language according to the given culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        public void ChangeLanguage(string culture)
        {
            Ressource.Strings.Culture = new CultureInfo(culture);
            Page currentPage = (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.Content as Page;

            if (currentPage.Name == "Options")
            {
                (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.Navigate(new OptionsPage());
            }
            else if (currentPage.Name == "OptionInGame")
            {
                (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.Navigate(new GameOptionPage());
            }
            
        }
    }
}
