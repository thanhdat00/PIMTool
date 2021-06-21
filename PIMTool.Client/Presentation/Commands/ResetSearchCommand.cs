using System;
using System.Windows.Input;

namespace PIMTool.Client.Presentation.Commands
{
    class ResetSearchCommand : ICommand
    {
        Action executeAction;

        public event EventHandler CanExecuteChanged;
        public ResetSearchCommand(Action executeAction)
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
