using EnumsNET;
using PIMTool.Client.Dictionary;
using PIMTool.Client.Extension;
using PIMTool.Client.Models;
using PIMTool.Client.Presentation.Commands;
using PIMTool.Client.WebApiClient.Services;
using PIMTool.Services.Resource;
using PIMTool.Services.Service.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private int _totalItems;
        private ProjectListModel _selectedItem;
        private List<ProjectListModel> ProjectList;     
        private ICommand _searchCommand;
        private ICommand _resetSearchCommand;
        
        private ObservableCollection<PageModel> _pageCollection;
        private ICollectionView _projectCollectionView;
        private string _currentSelectedStatus;
        public int NumberOfPage { get; set; }
        public List<ProjectDto> ListOfProject { get; set; }      
        public EStatusType Status { get; set; }
        public ICollectionView ProjectCollectionView
        {
            get { return _projectCollectionView; }
            set
            {
                _projectCollectionView = value;
                OnPropertyChanged(nameof(ProjectCollectionView));
            }
        }
        public ObservableCollection<PageModel> PageCollection
        {
            get { return _pageCollection; }
            set
            {
                _pageCollection = value;
                OnPropertyChanged(nameof(PageCollection));
            }
        }
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

            ListOfProject = _projectWebApiClient.GetSearchProject(new SearchProjectQuery()).ProjectItems;
            _totalItems = _projectWebApiClient.GetSearchProject(new SearchProjectQuery()).TotalItems;
            LoadProjectToCollectionView();

            _currentSelectedStatus = "none";
            NumberOfPage = PageCount(_totalItems);
            InitPageButton();

        }
        
        private void InitPageButton()
        {
            PageCollection = new ObservableCollection<PageModel>();
            for (int i=1; i <= NumberOfPage; i++)
            {
                PageCollection.Add(new PageModel(i.ToString(), 
                    new CommandHandler(HandlePageSelect, () => { return true; })));
            }
        }
        private int PageCount(int totalItems)
        {
            if (totalItems % 10 == 0)
                return totalItems / 10;
            else
                return totalItems / 10 + 1;
        }
        private void LoadProjectToCollectionView()
        {
            ProjectList = new List<ProjectListModel>();
            foreach (var item in ListOfProject)
            {
                ProjectList.Add(new ProjectListModel(item, new CommandHandler(HandleDeleteProject, () => { return true; })));
            }
 
            ProjectCollectionView = CollectionViewSource.GetDefaultView(ProjectList);
            ProjectCollectionView.Filter = FilterProject;
        }

        private void HandlePageSelect(object pageNumber)
        {
            var searchQuery = new SearchProjectQuery
            {
                SearchCriteria = _currentSelectedStatus,
                PageSize = 5,
                SelectedPage = int.Parse(pageNumber.ToString()),
            };
            InitialSelectPage(searchQuery);
        }

        private void InitialSelectPage(SearchProjectQuery searchQuery)
        {
            ListOfProject = _projectWebApiClient.GetSearchProject(searchQuery).ProjectItems;
            NumberOfPage = PageCount(_projectWebApiClient.GetSearchProject(searchQuery).TotalItems);
            LoadProjectToCollectionView();
            ProjectCollectionView.Refresh();
            InitPageButton();
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
            var searchQuery = new SearchProjectQuery();
            InitialSelectPage(searchQuery);
        }

        //Handle event when click to search project
        private void HandleSearchProject(object obj)
        {
            if (StatusFilter > -1 )
            {
                var searchQuery = new SearchProjectQuery
                {
                    SearchCriteria = ((EStatusType)StatusFilter).AsString(EnumFormat.Description),
                    SelectedPage =1
                };

                _currentSelectedStatus = searchQuery.SearchCriteria;
                ListOfProject = _projectWebApiClient.GetSearchProject(searchQuery).ProjectItems;
                NumberOfPage = PageCount(_projectWebApiClient.GetSearchProject(searchQuery).TotalItems);
                LoadProjectToCollectionView();
                ProjectCollectionView.Refresh();
                InitPageButton();
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
