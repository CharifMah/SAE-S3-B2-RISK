using JurassicRisk.Utilities;
using Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace JurassicRisk.ViewModels
{
    public class MainViewModel : observable.Observable
    {
        #region Attributs

        private Profil _selectedProfil;

        private ObservableCollection<Profil> _Profils;

        #endregion

        #region Properties

        public Profil SelectedProfil
        {
            get { return _selectedProfil; }
            set
            {
                _selectedProfil = value;
                NotifyPropertyChanged("SelectedProfil");
            }
        }

        public ObservableCollection<Profil> Profils
        {
            get { return _Profils; }

            set
            {
                _Profils = value;
                NotifyPropertyChanged("Profils");
            }
        }

        public ICommand DisplayProfilCommand { get; }

        #endregion Properties

        #region Constructor

        public MainViewModel()
        {
            _Profils = new ObservableCollection<Profil>();

            InitializeProfil();

            DisplayProfilCommand = new RelayCommand(o => DisplayProfil());

        }

        #endregion Constructor

        #region Private methods

        private void InitializeProfil()
        {
            _Profils.Add(new Profil("John"));

            _Profils.Add(new Profil("Alicia"));

            _Profils.Add(new Profil("Mike"));

            _Profils.Add(new Profil("Justine"));
        }

        private void DisplayProfil()
        {
            if (SelectedProfil is null)

                MessageBox.Show("Please select a user before.");

            else

                MessageBox.Show($"The selected user is {SelectedProfil.Pseudo}.");
        }

        #endregion Private methods

    }
}
