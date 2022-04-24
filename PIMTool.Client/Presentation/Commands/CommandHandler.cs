using System;
using System.Windows.Input;

namespace PIMTool.Client.Presentation.Commands
{
    class CommandHandler : ICommand
    {
        private Action<object> _action;
        private Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public CommandHandler(Action<object> action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            if (parameter != null) _action(parameter);
            else _action(null);
        }
    }
}
