using System;
using System.Windows.Input;

namespace PIMTool.Client.Presentation.Commands
{
    class SearchCommand : ICommand
    {
        Action executeAction;

        public event EventHandler CanExecuteChanged;
        public SearchCommand(Action executeAction)
        {
            this.executeAction = executeAction;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            executeAction();
        }
    }
}
