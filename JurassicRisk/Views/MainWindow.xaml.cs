using DBStorage;
using Models;
using Models.Son;
using System;
using System.Globalization;
using System.Windows;

namespace JurassicRisk.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            SoundStore.LoadSounds("Sounds");
            Settings.Get().Backgroundmusic = SoundStore.Get("JungleMusic.mp3");
            Settings.Get().Backgroundmusic.Play();
            Settings.Get().Backgroundmusic.Volume = Settings.Get().Volume / 100;
            GestionDatabase connection = new GestionDatabase();
            connection.CreateDatabase();
            connection.CreateUserTable();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();

            StartupSettings();
            frame.NavigationService.Navigate(new HomePage());

        }

        /// <summary>
        /// Selectionne les settings a charger au lancement
        /// </summary>
        /// <Author>Charif</Author>
        /// <exception cref="System.Windows.Markup.XamlParseException"></exception>
        private void StartupSettings()
        {
            if (Settings.Get().PleinEcran)
            {
                this.WindowStyle = WindowStyle.None;
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                this.WindowStyle = WindowStyle.ThreeDBorderWindow;
            }

            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Settings.Get().Culturename);
        }

        /// <summary>
        /// Environment.Exit a la fermeture de la fenetre 
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private void FentrePrincipal_Closed(object sender, System.EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
