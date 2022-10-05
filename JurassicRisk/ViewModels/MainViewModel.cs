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

        private Compte _selectedProfil;

        private ObservableCollection<Compte> _Profils;

        #endregion

        #region Properties

        public Compte SelectedProfil
        {
            get { return _selectedProfil; }
            set
            {
                _selectedProfil = value;
                NotifyPropertyChanged("SelectedProfil");
            }
        }

        public ObservableCollection<Compte> Profils
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
            _Profils = new ObservableCollection<Compte>();

            InitializeProfil();

            DisplayProfilCommand = new RelayCommand(o => DisplayProfil());

        }

        #endregion Constructor

        #region Private methods

        private void InitializeProfil()
        {
            _Profils.Add(new Compte("John", "Doe"));

            _Profils.Add(new Compte("Alicia", "Davis"));

            _Profils.Add(new Compte("Mike", "Jones"));

            _Profils.Add(new Compte("Justine", "Anderson"));
        }

        private void DisplayProfil()
        {
            if (SelectedProfil is null)

                MessageBox.Show("Please select a user before.");

            else

                MessageBox.Show($"The selected user is {SelectedProfil.Pseudo} {SelectedProfil.Password}.");
        }

        #endregion Private methods

    }
}
