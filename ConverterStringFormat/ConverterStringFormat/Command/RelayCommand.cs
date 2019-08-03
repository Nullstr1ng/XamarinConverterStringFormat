using System;
using System.Windows.Input;

namespace ConverterStringFormat.Command
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        
        Action _executeDelegate;

        public RelayCommand(Action executeDelegate)
        {
            _executeDelegate = executeDelegate;
        }

        public bool CanExecute(object parameter) { return true; }

        public void Execute(object parameter)
        {
            _executeDelegate();
        }
    }
}
