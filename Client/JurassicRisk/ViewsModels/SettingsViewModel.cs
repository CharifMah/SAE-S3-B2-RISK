using JurassicRisk.Views;
using Models.Settings;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

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

        public bool MusiqueOnOff
        {
            get { return Settings.Get().MusiqueOnOff; }
            set
            {
                Settings.Get().MusiqueOnOff = value;
                if (!Settings.Get().MusiqueOnOff)
                {
                    Settings.Get().Backgroundmusic.Volume = 0;
                }
                else
                {
                    Settings.Get().Backgroundmusic.Volume = Settings.Get().Volume / 100;

                }
                this.NotifyPropertyChanged("MusiqueOnOff");

            }
        }

        public double Volume
        {
            get { return Settings.Get().Volume; }
            set
            {
                Settings.Get().Volume = value;
                Settings.Get().Backgroundmusic.Volume = value / 100;
                if (Settings.Get().Volume > 1)
                {
                    Settings.Get().MusiqueOnOff = true;
                }
                if (Settings.Get().Volume < 1)
                {
                    Settings.Get().MusiqueOnOff = false;
                }
                this.NotifyPropertyChanged("Volume");
                this.NotifyPropertyChanged("MusiqueOnOff");
            }
        }

        public SettingsViewModel()
        {
            this.NotifyPropertyChanged("PLeinEcran");
            this.NotifyPropertyChanged("Culturename");
            this.NotifyPropertyChanged("MusiqueOnOff");
            this.NotifyPropertyChanged("Volume");
        }

        /// <summary>
        /// Changes the language according to the given culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        public void ChangeLanguage(string culture)
        {
            Ressource.Strings.Culture = new CultureInfo(culture);

            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.Navigate(new OptionsPage(Settings.Get().ActualPage));
        }
    }
}
