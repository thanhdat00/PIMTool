using PIMTool.Client.Dictionary;
using PIMTool.Services.Resource;
using System;
using System.Windows.Input;

namespace PIMTool.Client.Models
{
    class ProjectModel
    {
        public ProjectResource Project { get; set; }
        public int ProjectNumber { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Customer { get; set; }
        public DateTime StartDate { get; set; }
        public ICommand DeleteProjectCommand { get; set; }
        public StatusDictionary StatusDic = new StatusDictionary();
        public ProjectModel(ProjectResource project, ICommand command)
        {
            Project = project;
            DeleteProjectCommand = command;

            ProjectNumber = Project.ProjectNumber;
            Name = Project.Name;
            Status = StatusDic.Status[Project.Status].ToString();
            Customer = Project.Customer;
            StartDate = Project.StartDate;
        }
    }
}
