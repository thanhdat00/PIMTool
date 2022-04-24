using PIMTool.Common.BusinessObjects;
using PIMTool.Services.Resource;
using PIMTool.Services.Service.Models;
using System.Collections.Generic;

namespace PIMTool.Client.WebApiClient.Services
{
    public interface IProjectWebApiClient
    {
        List<ProjectDto> GetAllProjects();
        Project GetProject(int projectId);
        void SaveProject<SaveProjectResource>(SaveProjectResource resource);
        void UpdateProject(SaveProjectDto resource);
        void DeleteProject(int projectNumber);
        SearchProjectQueryResult GetSearchProject(SearchProjectQuery query);
    }
}
