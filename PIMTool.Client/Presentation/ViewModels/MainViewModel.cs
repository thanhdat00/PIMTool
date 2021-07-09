using PIMTool.Client.Presentation.Commands;
using PIMTool.Client.WebApiClient.Services;
using PIMTool.Services.Resource;
using PIMTool.Services.Service.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace PIMTool.Client.Presentation.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel;
        private bool _projectListSelected = true;
        private bool _createProjectSelected;
        private bool _editProjectSelected;
        private bool _errorOccur;
        private ICommand _updateViewCommand;

        public readonly IProjectWebApiClient ProjectWebApiClient;
        public readonly IEmployeeWebApiClient EmployeeWebApiClient;
        public List<ProjectDto> Projects { get; private set; }
        public List<EmployeeDto> Employees { get; set; }
        public ProjectDto SelectedProject { get; set; }

        #region Getter Setter
        public bool ErrorOccur
        {
            get { return _errorOccur; }
            set
            {
                _errorOccur = value;
                if (_errorOccur)
                {
                    _projectListSelected = false;
                    _createProjectSelected = false;
                    _editProjectSelected = false;
                }
            }
        }
        public bool EditProjectSelected
        {
            get { return _editProjectSelected; }
            set
            {
                _editProjectSelected = value;
                if (_editProjectSelected)
                {
                    _projectListSelected = false;
                    _createProjectSelected = false;
                }
            }
        }
        public bool ProjectListSelected
        {
            get { return _projectListSelected; }
            set
            {
                _projectListSelected = value;              
                OnPropertyChanged(nameof(ProjectListSelected));               
            }
        }
        public bool CreateProjectSelected
        {
            get { return _createProjectSelected; }
            set
            {
                _createProjectSelected = value;
                OnPropertyChanged(nameof(CreateProjectSelected));                
            }
        }
        public BaseViewModel SelectedViewModel
        { 
            get { return _selectedViewModel; }
            set 
            { 
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }
        #endregion
        public ICommand UpdateViewCommand
        {
            get
            {
                return _updateViewCommand ?? (_updateViewCommand = new CommandHandler(HandleUpdateView, () => { return true; }));
            }
        }
        public void HandleUpdateView(object obj)
        {
            if (ProjectListSelected)
                SelectedViewModel = new ProjectListViewModel(this);
            else if (CreateProjectSelected)
                SelectedViewModel = new DetailProjectViewModel(this, false);
            else if (EditProjectSelected)
                SelectedViewModel = new DetailProjectViewModel(this, true, this.SelectedProject);
        }

        public MainViewModel(IProjectWebApiClient projectWebApiClient, IEmployeeWebApiClient employeeWebApiClient)
        {
            ProjectWebApiClient = projectWebApiClient;
            EmployeeWebApiClient = employeeWebApiClient;

            Projects = ProjectWebApiClient.GetAllProjects();
            Employees = EmployeeWebApiClient.GetAllEmployees(); 
            SelectedViewModel = new ProjectListViewModel(this);
        }

        //Reset Project List when something update in DB
        public void ResetAllProject()
        {
            Projects = ProjectWebApiClient.GetAllProjects();
        }
    }
}
