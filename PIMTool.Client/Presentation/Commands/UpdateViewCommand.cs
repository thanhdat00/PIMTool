using PIMTool.Client.Presentation.ViewModels;
using System;
using System.Windows.Input;

namespace PIMTool.Client.Presentation.Commands
{
    class UpdateViewCommand : ICommand
    {     
        private MainViewModel _viewModel;

        public event EventHandler CanExecuteChanged;
        public UpdateViewCommand(MainViewModel viewModel)
        {
            this._viewModel = viewModel;
        }
        public void Execute(object parameter)
        {
            if (_viewModel.ProjectListSelected)
                _viewModel.SelectedViewModel = new ProjectListViewModel(_viewModel);
            else if (_viewModel.CreateProjectSelected)
                _viewModel.SelectedViewModel = new CreateProjectViewModel(_viewModel);
            else if (_viewModel.EditProjectSelected)
                _viewModel.SelectedViewModel = new EditProjectViewModel(_viewModel, _viewModel.SelectedProject);
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
