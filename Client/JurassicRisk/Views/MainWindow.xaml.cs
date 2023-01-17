using JurassicRisk.ViewsModels;
using Models;
using Models.Settings;
using Models.Son;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;

namespace JurassicRisk.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// MainWindow with frame navigation system (Page)
    /// </summary>
    /// <Auteur>Mahmoud Charif</Auteur>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.Closed += async (sender, e) => await FentrePrincipal_Closed(sender, e);

            StartupSettings();
        }

        /// <summary>
        /// Selectionne les settings a charger au lancement
        /// </summary>
        /// <Author>Charif</Author>
        /// <exception cref="System.Windows.Markup.XamlParseException"></exception>
        private void StartupSettings()
        {
            //Load Sounds
            SoundStore.LoadSounds("Sounds");

            //Get Settings.json if exist and set the background music
            Settings.Get().LoadSettings();
            Settings.Get().Backgroundmusic = SoundStore.Get("HubJurr.mp3");
            Settings.Get().Backgroundmusic.Play();
            Settings.Get().Backgroundmusic.Volume = Settings.Get().Volume / 100;

            //Navigate to the HomePage (with frame)
            frame.NavigationService.Navigate(new HomePage());

            //Set fullscreen on off with the settings loaded at startup 
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

            //Set the cultrure info with the settings loaded (language)
            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Settings.Get().Culturename);
        }

        /// <summary>
        /// Environment.Exit a la fermeture de la fenetre 
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Author>Charif</Author>
        private async Task FentrePrincipal_Closed(object sender, System.EventArgs e)
        {
            await JurassicRiskViewModel.Get.LobbyVm.DisconnectLobby();
            Environment.Exit(0);
        }
    }
}
