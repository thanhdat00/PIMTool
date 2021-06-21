using PIMTool.Common.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIMTool.Client.WebApiClient.Services;

namespace PIMTool.Client.Presentation
{
    public class ProjectViewModel
    {
        private readonly IProjectWebApiClient _projectWebApiClient;
        public ProjectViewModel(
            IProjectWebApiClient projectWebApiClient)
        {
            _projectWebApiClient = projectWebApiClient;
            LoadProject(10);
        }
        
        private Project _model;

        public int ProjectId 
        {
            get { return _model.Id; }
            set
            { 
                _model.Id = value; 
                //OnpropertyChange(nameof(ProjectName));
            }
        }

        public string ProjectName
        {
            get { return _model.Name; }
            set
            { 
                _model.Name = value;
                //OnpropertyChange(nameof(ProjectName));
            }
        }

        private void LoadProject(int projectId)
        {
            _model = _projectWebApiClient.GetProject(projectId);
        }
    }
}
