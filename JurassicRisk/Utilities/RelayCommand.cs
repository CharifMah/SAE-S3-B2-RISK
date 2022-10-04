using System;
using System.Windows.Input;

namespace JurassicRisk.Utilities
{
    internal class RelayCommand : ICommand
    {
        #region Fields

        private readonly Action<object> _execute;

        private readonly Predicate<object> _canExecute;

        #endregion Fields

        #region Constructor

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null) throw new ArgumentNullException("execute");

            _execute = execute;

            _canExecute = canExecute;
        }

        #endregion Constructor

        #region Public methods

        public bool CanExecute(object parameter)

        {

            return _canExecute == null || _canExecute(parameter);

        }

        public event EventHandler CanExecuteChanged

        {

            add { CommandManager.RequerySuggested += value; }

            remove { CommandManager.RequerySuggested -= value; }

        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion Public methods
    }
}
