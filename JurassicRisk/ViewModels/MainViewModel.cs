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

        private Compte _selectedAccount;

        private ObservableCollection<Compte> _accounts;

        #endregion

        #region Properties

        public Compte SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                NotifyPropertyChanged("SelectedAccount");
            }
        }

        public ObservableCollection<Compte> Accounts
        {
            get { return _accounts; }

            set
            {
                _accounts = value;
                NotifyPropertyChanged("Accounts");
            }
        }

        public ICommand DisplayAccountCommand { get; }

        #endregion Properties

        #region Constructor

        public MainViewModel()
        {
            _accounts = new ObservableCollection<Compte>();

            InitializeAccount();

            DisplayAccountCommand = new RelayCommand(o => DisplayAccount());

        }

        #endregion Constructor

        #region Private methods

        private void InitializeAccount()
        {
            _accounts.Add(new Compte("John", "Doe"));

            _accounts.Add(new Compte("Alicia", "Davis"));

            _accounts.Add(new Compte("Mike", "Jones"));

            _accounts.Add(new Compte("Justine", "Anderson"));
        }

        private void DisplayAccount()
        {
            if (SelectedAccount is null)

                MessageBox.Show("Please select a user before.");

            else

                MessageBox.Show($"The selected user is {SelectedAccount.Pseudo} {SelectedAccount.Password}.");
        }

        #endregion Private methods

    }
}
