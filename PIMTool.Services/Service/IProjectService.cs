using PIMTool.Services.Resource;
using PIMTool.Services.Service.Communication;
using System.Collections.Generic;

namespace PIMTool.Services.Service
{
    public interface IProjectService
    {
        IList<Entities.ProjectEntity> GetAll();
        Entities.ProjectEntity GetById(int projectId);
        ProjectResponse DeleteById(int projectId);
        ProjectResponse Save(SaveProjectDto project);
        ProjectResponse Update(SaveProjectDto project);
        ProjectResponse DeleteByProjectNumber(int value);
    }
}