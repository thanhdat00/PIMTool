using PIMTool.Client.Dictionary;
using PIMTool.Client.Presentation.Commands;
using PIMTool.Client.WebApiClient.Services;
using PIMTool.Services.Resource;
using System;
using System.Threading;
using System.Linq;
using System.Windows.Input;
using System.ComponentModel;
using PIMTool.Client.Validation;
using System.Windows;

namespace PIMTool.Client.Presentation.ViewModels
{
    class CreateProjectViewModel : BaseViewModel , IDataErrorInfo
    {       
        private SaveProjectResource _project = new SaveProjectResource();
        private readonly IProjectWebApiClient _projectWebApiClient;
        private MainViewModel _mainViewModel;
        private ProjectValidator _projectValidator;

        public StatusDictionary StatusDic = new StatusDictionary();
        public int ProjectNumber { get; set; }
        public string ProjectName { get; set; }
        public string Customer { get; set; }
        public int Group { get; set; }
        public string Member { get; set; }
        public int Status { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now.Date;
        public DateTime? FinishDate { get; set; } = null;

        public ICommand CreateProjectCommand { get; set; }
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
        public CreateProjectViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _projectValidator = new ProjectValidator(_mainViewModel.Projects);          
            _projectWebApiClient = _mainViewModel.ProjectWebApiClient ;
            CreateProjectCommand = new CreateProjectCommand(CreateProject, _mainViewModel);
        }

        //TODO: Save new Project to the DB
        private void CreateProject(object obj)
        {
            try
            {
                _project.ProjectNumber = (int)ProjectNumber;
                _project.GroupId = Group + 1;
                _project.Customer = Customer;
                _project.Name = ProjectName;
                _project.Status = StatusDic.Status[Status].ToString();
                _project.StartDate = StartDate;
                _project.Member = Member.Trim(' ');
                AddProject(_project);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.ToString());
            }

            Thread.Sleep(1000);
            _mainViewModel.ResetAllProject();
            _mainViewModel.ProjectListSelected = true;
            _mainViewModel.SelectedViewModel = new ProjectListViewModel(_mainViewModel);
        }

        private void AddProject(SaveProjectResource resource)
        {
            _projectWebApiClient.SaveProject(resource);
        }
    }
}
