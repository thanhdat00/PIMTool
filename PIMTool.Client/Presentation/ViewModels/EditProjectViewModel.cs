using PIMTool.Client.Dictionary;
using PIMTool.Client.Presentation.Commands;
using PIMTool.Client.Validation;
using PIMTool.Client.WebApiClient.Services;
using PIMTool.Services.Resource;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace PIMTool.Client.Presentation.ViewModels
{
    class EditProjectViewModel : BaseViewModel, IDataErrorInfo
    {
        private readonly IProjectWebApiClient _projectWebApiClient;
        private MainViewModel _mainViewModel;
        private int _projectNumber;
        private string _projectName;
        private string _customer;
        private int _group;
        private string _member;
        private int _status;
        private DateTime _startDate;
        private DateTime? _finishDate;
        private EditProjectValidator _projectValidator;

        #region Getter Setter
        public ProjectResource Project { get; set; }
        public int ProjectNumber 
        {
            get { return _projectNumber; }
            set
            {
                _projectNumber = value;
                OnPropertyChanged(nameof(ProjectNumber));
            }
        }
        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                _projectName = value;
                OnPropertyChanged(nameof(ProjectName));
            }
        }
        public string Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                OnPropertyChanged(nameof(Customer));
            }
        }
        public string Member
        {
            get { return _member; }
            set
            {
                _member = value;
                OnPropertyChanged(nameof(Member));
            }
        }
        public int Group
        { 
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged(nameof(Group));
            }
        }
        public int Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public DateTime? FinishDate
        {
            get { return _finishDate; }
            set
            {
                _finishDate = value;
                OnPropertyChanged(nameof(FinishDate));
            }
        }
        #endregion 
        public StatusDictionary StatusDic = new StatusDictionary();
        public ICommand UpdateProjectCommand { get; set; }

        #region Validation setting
        public string this[string columnName]
        {
            get
            {
                var firstOrDefault = _projectValidator.Validate(this).Errors.FirstOrDefault(lol => lol.PropertyName == columnName);
                if (firstOrDefault != null)
                    return _projectValidator != null ? firstOrDefault.ErrorMessage : "";
                return "";
            }
        }
        public string Error
        {
            get
            {
                if (_projectValidator != null)
                {
                    var results = _projectValidator.Validate(this);
                    if (results != null && results.Errors.Any())
                    {
                        var errors = string.Join(Environment.NewLine, results.Errors.Select(x => x.ErrorMessage).ToArray());
                        return errors;
                    }
                }
                return string.Empty;
            }
        }
        #endregion

        public EditProjectViewModel(MainViewModel mainViewModel, ProjectResource project)
        {
            _mainViewModel = mainViewModel;
            _projectValidator = new EditProjectValidator();
            _projectWebApiClient = _mainViewModel.ProjectWebApiClient;
            Project = project;
            LoadProject();
            UpdateProjectCommand = new UpdateProjectCommand(UpdateProject, _mainViewModel);
        }

        //TODO: Update Project to the DB
        private void UpdateProject()
        {
            var project = new SaveProjectResource();
            project.ProjectNumber = (int)ProjectNumber;                                                                                                                                                             
            project.GroupId = Group + 1;
            project.Customer = Customer;
            project.Name = ProjectName;
            project.Member = Member.Trim(' ');
            project.Status = StatusDic.Status[Status].ToString();
            project.StartDate = StartDate;
            project.FinishDate = FinishDate;
            _projectWebApiClient.UpdateProject(project);

            Thread.Sleep(1000);
            _mainViewModel.ResetAllProject();
            _mainViewModel.ProjectListSelected = true;      
            _mainViewModel.SelectedViewModel = new ProjectListViewModel(_mainViewModel);
        }

        //TODO: Load info of selected project to the form
        private void LoadProject()
        {
            ProjectNumber = Project.ProjectNumber;
            ProjectName = Project.Name;
            Customer = Project.Customer;
            Group = Project.GroupId-1;
            Member = Project.Member;
            foreach(var item in StatusDic.Status)
                if(Project.Status.Equals(item.Value))
                {
                    Status = (int)item.Key;
                }
            StartDate = Project.StartDate;
            FinishDate = Project.FinishDate;
        }
    }
}
