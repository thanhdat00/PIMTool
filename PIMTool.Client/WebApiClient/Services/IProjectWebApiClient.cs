using PIMTool.Common.BusinessObjects;

namespace PIMTool.Client.WebApiClient.Services
{
    public interface IProjectWebApiClient
    {
        string GetAllProjects();
        Project GetProject(int projectId);
    }
}
