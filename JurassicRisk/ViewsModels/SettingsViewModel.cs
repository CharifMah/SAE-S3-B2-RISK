using JurassicRisk.Views;
using Models;
using Models.Son;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
                System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(value);
                (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.Navigate(new OptionsPage());
                this.NotifyPropertyChanged("Culturename");
            }
        }

        public Settings Settings
        {
            get { return Settings.Get(); }

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
            this.NotifyPropertyChanged("Musique");
        }

        public bool Musique
        {
            get { return Settings.Get().Musique; }
            set
            {
                Settings.Get().Musique = value;
                if (!Settings.Get().Musique)
                {
                    Settings.Get().BackgroundVolume = 0;
                }
                else
                {
                    Settings.Get().BackgroundVolume = Settings.Get().Backgroundmusic.Volume / 100;

                }
                this.NotifyPropertyChanged("Musique");
            }
        }
    }
}
