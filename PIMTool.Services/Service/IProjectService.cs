using System.Collections.Generic;

namespace PIMTool.Services.Service
{
    public interface IProjectService
    {
        IList<Entities.ProjectEntity> GetAll();
        Entities.ProjectEntity GetById(int projectId);
    }
}