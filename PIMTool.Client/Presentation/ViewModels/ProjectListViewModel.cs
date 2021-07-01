using EnumsNET;
using PIMTool.Client.Dictionary;
using PIMTool.Client.Extension;
using PIMTool.Client.Models;
using PIMTool.Client.Presentation.Commands;
using PIMTool.Client.WebApiClient.Services;
using PIMTool.Services.Resource;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PIMTool.Client.Presentation.ViewModels
{
    class ProjectListViewModel : BaseViewModel
    {        
        private string _projectFilter = string.Empty;
        private readonly IProjectWebApiClient _projectWebApiClient;
        private MainViewModel _mainViewModel;
        private int _statusFilter;
        private ProjectListModel _selectedItem;
        private List<ProjectListModel> ProjectList = new List<ProjectListModel>();     
        private ICommand _searchCommand;
        private ICommand _resetSearchCommand;
        public ICollectionView ProjectCollectionView { get; }
        public EStatusType Status { get; set; }

        public ProjectListModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                HandleSelectProject();
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new CommandHandler(HandleSearchProject, () => { return true; }));
            }
        }

        public ICommand ResetSearchCommand
        {
            get
            {
                return _resetSearchCommand ?? (_resetSearchCommand = new CommandHandler(HandleResetSearch, () => { return true; }));
            }
        }
        public string ProjectFilter
        {
            get { return _projectFilter; }
            set 
            {
                _projectFilter = value;
                OnPropertyChanged(nameof(ProjectFilter));
                ProjectCollectionView.Refresh();
            }
        }
        public int StatusFilter
        {
            get { return _statusFilter; }
            set
            {
                _statusFilter = value;
                OnPropertyChanged(nameof(StatusFilter));
            }
        }
        public ProjectListViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _projectWebApiClient = _mainViewModel.ProjectWebApiClient;
            foreach (var item in _mainViewModel.Projects)
            {
                ProjectList.Add(new ProjectListModel(item, new CommandHandler(HandleDeleteProject, () => { return true; })));
            }

            ProjectCollectionView = CollectionViewSource.GetDefaultView(ProjectList);
            ProjectCollectionView.Filter = FilterProject;
            ProjectCollectionView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(ProjectDto.Status)));
            ProjectCollectionView.SortDescriptions.Add(new SortDescription(nameof(ProjectDto.ProjectNumber),
                                                        ListSortDirection.Ascending));
        }
        
        //Handle event when click the delete button
        private void HandleDeleteProject(object prjNumber)
        {
            int projectNumber = (int)prjNumber;
            foreach (var item in ProjectList)
                if (item.Project.ProjectNumber == projectNumber)
                {
                    ProjectList.Remove(item);
                    ProjectCollectionView.Refresh();
                    break;
                }
            _projectWebApiClient.DeleteProject(projectNumber);
            MessageBox.Show("Deleted");
        }

        //Handle event when click the reset button
        private void HandleResetSearch(object obj)
        {
            StatusFilter = -1;
            ProjectCollectionView.Filter = FilterProject;
        }

        //Handle event when click to search project
        private void HandleSearchProject(object obj)
        {
            if (StatusFilter > -1 )
            {
                ProjectCollectionView.Filter = FilterProjectByStatus;
            }
        }

        //Filter engine for searching by status
        private bool FilterProjectByStatus(object obj)
        {
            if (obj is ProjectListModel project)
            {
                string description = ((EStatusType)StatusFilter).AsString(EnumFormat.Description);
                return EnumHelper.Description(project.Project.Status).Contains(description) && 
                    ( project.Customer.Contains(ProjectFilter) 
                    || project.ProjectNumber.ToString().Contains(ProjectFilter)
                    || project.Name.Contains(ProjectFilter) ) ;
            }
            return false;
        }

        //Handle evenet when click to any item in projectlist
        private void HandleSelectProject()
        {
            DetailProjectViewModel viewModel = new DetailProjectViewModel(_mainViewModel, true, SelectedItem.Project);
            _mainViewModel.EditProjectSelected = true;
            _mainViewModel.SelectedViewModel = viewModel;
        }

        //Fiter engine
        private bool FilterProject(object obj)
        {
            if (obj is ProjectListModel project)
            {
                return project.Customer.Contains(ProjectFilter) || project.Project.ProjectNumber.ToString().Contains(ProjectFilter)
                    || project.Name.Contains(ProjectFilter);
            }
            return false;
        }
    }
}
