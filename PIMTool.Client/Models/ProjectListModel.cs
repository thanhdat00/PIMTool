using PIMTool.Client.Extension;
using PIMTool.Services.Resource;
using System;
using System.Windows.Input;

namespace PIMTool.Client.Models
{
    class ProjectListModel
    {
        public ProjectDto Project { get; set; }
        public int ProjectNumber { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Customer { get; set; }
        public DateTime StartDate { get; set; }
        public ICommand DeleteProjectCommand { get; set; }
        public ProjectListModel(ProjectDto project, ICommand deleteCommand)
        {
            Project = project;
            DeleteProjectCommand = deleteCommand;
            ProjectNumber = Project.ProjectNumber;
            Name = Project.Name;
            Status = EnumHelper.Description(Project.Status);
            Customer = Project.Customer;
            StartDate = Project.StartDate;
        }
    }
}
