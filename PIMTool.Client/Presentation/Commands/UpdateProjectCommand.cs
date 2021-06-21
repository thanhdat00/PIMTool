using PIMTool.Client.Presentation.ViewModels;
using System;
using System.Windows.Input;

namespace PIMTool.Client.Presentation.Commands
{
    class UpdateProjectCommand : ICommand
    {
        Action executeAction;
        private MainViewModel _viewmodel;

        public event EventHandler CanExecuteChanged;

        public UpdateProjectCommand(Action executeAction, MainViewModel mainViewModel)
        {
            this.executeAction = executeAction;
            this._viewmodel = mainViewModel;
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
