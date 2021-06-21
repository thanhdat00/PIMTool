using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace PIMTool.Client.Presentation.Commands
{
    class DeleteProjectCommand : ICommand
    {
        Action<object> executeAction;

        public event EventHandler CanExecuteChanged;
        public DeleteProjectCommand(Action<object> executeAction)
        {
            this.executeAction = executeAction;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
    }
}
