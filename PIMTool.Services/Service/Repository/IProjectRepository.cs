using PIMTool.Services.Resource;
using PIMTool.Services.Service.Entities;
using PIMTool.Services.Service.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIMTool.Services.Service.Repository
{
    public interface IProjectRepository : IBaseRepository<Entities.ProjectEntity>
    {
        ProjectEntity NewProject(SaveProjectResource project);
        ProjectEntity UpdateProject(int projectNumber);
    }
}