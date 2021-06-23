using PIMTool.Client.Presentation.Commands;
using PIMTool.Client.WebApiClient.Services;
using PIMTool.Services.Resource;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace PIMTool.Client.Presentation.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel;
        private bool _projectListSelected = true;
        private bool _createProjectSelected = false;
        private bool _editProjectSelected = false;
        private ICommand _updateViewCommand;

        public readonly IProjectWebApiClient ProjectWebApiClient;
        public List<ProjectResource> Projects { get; private set; }
        public ProjectResource SelectedProject { get; set; }

        #region Getter Setter
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
                return _updateViewCommand ?? (_updateViewCommand = new CommandHandler(() => HandleUpdateView(), () => CanUpdateView()));
            }
        }

        public bool CanUpdateView()
        {
            return true;
        }

        public void HandleUpdateView()
        {
            if (ProjectListSelected)
                SelectedViewModel = new ProjectListViewModel(this);
            else if (CreateProjectSelected)
                SelectedViewModel = new DetailProjectViewModel(this,false);
            else if (EditProjectSelected)
                SelectedViewModel = new DetailProjectViewModel(this,true,this.SelectedProject);
        }

        public MainViewModel(IProjectWebApiClient projectWebApiClient)
        {
            ProjectWebApiClient = projectWebApiClient;
            Projects = projectWebApiClient.GetAllProjects();
            SelectedViewModel = new ProjectListViewModel(this);
        }

        //Reset Project List when something update in DB
        public void ResetAllProject()
        {
            Projects = ProjectWebApiClient.GetAllProjects();
        }
    }
}
