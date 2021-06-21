using PIMTool.Common.BusinessObjects;
using PIMTool.Services.Resource;
using System.Collections.Generic;

namespace PIMTool.Client.WebApiClient.Services
{
    public interface IProjectWebApiClient
    {
        List<ProjectResource> GetAllProjects();
        Project GetProject(int projectId);
        void SaveProject<SaveProjectResource>(SaveProjectResource resource);
        void UpdateProject(SaveProjectResource resource);
        void DeleteProject(int projectNumber);
    }
}
