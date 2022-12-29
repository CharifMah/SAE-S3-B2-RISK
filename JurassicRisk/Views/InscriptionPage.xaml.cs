using JurassicRisk.Ressource;
using JurassicRisk.ViewsModels;
using Models.Player;
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

        #region Event

        #region Async

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (!await ProfilViewModel.Get.VerifProfilCreation(inputPseudo.Text))
            {
                if (inputPseudo.Text != "")
                {
                    if (inputConfirmPassword.Password == inputPassword.Password)
                    {
                        inscription();
                    }
                    else
                    {
                        Error.Text = Strings.PasswordNotMatch;
                        Error.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    Error.Text = Strings.NoPseudoEnter;
                    Error.Visibility = Visibility.Visible;
                }
            }
            else
            {
                Error.Text = "this pseudo already exist";
                Error.Visibility = Visibility.Visible;
            }
        }

        private async void inscription()
        {
            Profil profil = new Profil(inputPseudo.Text, inputPassword.Password);
            string inscription = await ProfilViewModel.Get.Inscription(profil);
            if (inscription == "Ok")
            {
                (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new MenuPage());
                await ProfilViewModel.Get.Connexion(profil);
            }
            else
            {
                Error.Text = inscription;
                Error.Visibility = Visibility.Visible;
            }
        }

        #endregion

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(App.Current.MainWindow) as MainWindow).frame.NavigationService.Navigate(new HomePage());
        }

        #endregion
    }
}
