using PIMTool.Services.Resource;
using PIMTool.Services.Service.Entities;
using PIMTool.Services.Service.Generic;
using PIMTool.Services.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIMTool.Services.Service.Repository
{
    public interface IProjectRepository : IBaseRepository<Entities.ProjectEntity>
    {
        ProjectEntity NewProject(SaveProjectDto project);
        ProjectEntity UpdateProject(int projectNumber);
        SearchProjectQueryResult GetSearchProject(SearchProjectQuery query);
    }
}