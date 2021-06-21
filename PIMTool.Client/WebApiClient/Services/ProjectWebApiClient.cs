using System.Threading.Tasks;
using PIMTool.Common;
using PIMTool.Common.BusinessObjects;

namespace PIMTool.Client.WebApiClient.Services
{
    public class ProjectWebApiClient : WebApiClientBase, IProjectWebApiClient
    {
        public ProjectWebApiClient(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
        {
        }

        public override string RoutePrefix => RouteConstants.ProjectApi;

        public string GetAllProjects()
        {
            return Task.Run(() => Get<string>(RouteConstants.GetAllProjects)).Result;
        }

        public Project GetProject(int projectId)
        {
            return Task.Run(() => Get<Project>(string.Format(RouteConstants.GetProjectClient, projectId))).Result;
        }
    }
}
