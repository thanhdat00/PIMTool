using PIMTool.Client.Dictionary;
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
        private int _selectedProjectItemIndex = -1;
        private int _statusFilter = -1;
        private List<ProjectModel> ProjectList = new List<ProjectModel>();     
        public ICollectionView ProjectCollectionView { get; }
        public StatusDictionary StatusDic = new StatusDictionary();         
        public ICommand SearchCommand { get; set; }
        public ICommand ResetSearchCommand { get; set; }
        public int SelectedProjectItemIndex
        {
            get { return _selectedProjectItemIndex; }
            set
            {
                _selectedProjectItemIndex = value;
                HandleSelectProject();
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
            SearchCommand = new SearchCommand(HandleSearchProject);
            ResetSearchCommand = new ResetSearchCommand(HandleResetSearch);
            foreach (var item in _mainViewModel.Projects)
            {
                ProjectList.Add(new ProjectModel(item, new DeleteProjectCommand(HandleDeleteProject)));
            }

            ProjectCollectionView = CollectionViewSource.GetDefaultView(ProjectList);
            ProjectCollectionView.Filter = FilterProject;
            ProjectCollectionView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(ProjectResource.Status)));
            ProjectCollectionView.SortDescriptions.Add(new SortDescription(nameof(ProjectResource.ProjectNumber),
                                                        ListSortDirection.Ascending));
        }
        
        //TODO: Handle event when click the delete button
        private void HandleDeleteProject(object obj)
        {
            int projectNumber = (int)obj;
            MessageBox.Show("Deleted");
            foreach (var item in ProjectList)
                if (item.Project.ProjectNumber == projectNumber)
                {
                    ProjectList.Remove(item);
                    ProjectCollectionView.Refresh();
                    break;
                }
            _projectWebApiClient.DeleteProject(projectNumber);           
        }

        //TODO: Handle event when click the reset button
        private void HandleResetSearch()
        {
            StatusFilter = -1;
            ProjectCollectionView.Filter = FilterProject;
        }

        //TODO: Handle event when click to search project
        private void HandleSearchProject()
        {
            if (StatusFilter > -1 )
            {
                ProjectCollectionView.Filter = FilterProjectByStatus;
            }
        }

        //TODO: Filter engine for searching by status
        private bool FilterProjectByStatus(object obj)
        {
            if (obj is ProjectModel project)
            {
                return project.Project.Status.Contains(StatusDic.Status[StatusFilter].ToString()) && 
                    ( project.Customer.Contains(ProjectFilter) 
                    || project.ProjectNumber.ToString().Contains(ProjectFilter)
                    || project.Name.Contains(ProjectFilter) ) ;
            }
            return false;
        }

        //TODO: Handle evenet when click to any item in projectlist
        private void HandleSelectProject()
        {            
            if (SelectedProjectItemIndex == -1) return;
            var tmpProjectList = ProjectCollectionView;
            tmpProjectList.MoveCurrentToFirst();
            ProjectModel project = null;
            while (tmpProjectList.MoveCurrentToNext())
            {
                if (tmpProjectList.CurrentPosition == SelectedProjectItemIndex)
                {
                    project = tmpProjectList.CurrentItem as ProjectModel;
                    break;
                }
            }
            EditProjectViewModel viewModel = new EditProjectViewModel(_mainViewModel, project.Project);
            _mainViewModel.EditProjectSelected = true;
            _mainViewModel.SelectedViewModel = viewModel;
            SelectedProjectItemIndex = -1;
        }

        //TODO: Fiter engine
        private bool FilterProject(object obj)
        {
            if (obj is ProjectModel project)
            {
                return project.Customer.Contains(ProjectFilter) || project.Project.ProjectNumber.ToString().Contains(ProjectFilter)
                    || project.Name.Contains(ProjectFilter);
            }
            return false;
        }
    }
}
