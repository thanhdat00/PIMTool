using PIMTool.Services.Resource;
using PIMTool.Services.Service.Entities;
using PIMTool.Services.Service.Generic;

namespace PIMTool.Services.Service.Repository
{
    public class ProjectRepository : BaseRepository<ProjectEntity>, IProjectRepository
    {
        // Find the project which will be updated
        public ProjectEntity UpdateProject(int projectNumber)
        {
            ProjectEntity res = Session.QueryOver<ProjectEntity>()
                                .Where(p => p.ProjectNumber == projectNumber).SingleOrDefault();
            return res;
        }

        // Create a ProjectEntity from a SaveProjectResource
        public ProjectEntity NewProject(SaveProjectResource project)
        {
            var result = new ProjectEntity();
            result.Name = project.Name;
            result.Customer = project.Customer;
            result.ProjectNumber = project.ProjectNumber;
            result.StartDate = project.StartDate;
            result.GroupId = project.GroupId;           
            result.FinishDate = project.FinishDate;
            result.Status = project.Status;
            string[] members = project.Member.Split(',');
            foreach(var item in members)
            {
                EmployeeEntity employee = Session.QueryOver<EmployeeEntity>()
                                      .Where(p => p.Visa == item).SingleOrDefault();
                result.Employees.Add(employee);
            }           
            return result;
        }
    }
}