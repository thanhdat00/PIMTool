using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using PIMTool.Common;
using PIMTool.Common.BusinessObjects;
using PIMTool.Services.Resource;
using PIMTool.Services.Service.Models;

namespace PIMTool.Client.WebApiClient.Services
{
    public class ProjectWebApiClient : WebApiClientBase, IProjectWebApiClient
    {
        public ProjectWebApiClient(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
        {
        }

        public override string RoutePrefix => RouteConstants.ProjectApi;

        public List<ProjectDto> GetAllProjects()
        {
            return Task.Run(() => Get<List<ProjectDto>>(RouteConstants.GetAllProjects)).Result;
        }

        public Project GetProject(int projectId)
        {
            return Task.Run(() => Get<Project>(string.Format(RouteConstants.GetProjectClient, projectId))).Result;
        }

        public void SaveProject<SaveProjectResource>(SaveProjectResource resource)
        {
            Task.Run(() => Post<SaveProjectResource>(RouteConstants.AddProject, resource));
        }

        public void UpdateProject(SaveProjectDto resource)
        {
            Task.Run(() => Put(RouteConstants.UpdateProject, resource));
        }

        public void DeleteProject(int projectNumber)
        {
            string route = "DeleteProject/" + projectNumber.ToString();
            Task.Run(() => Delete(route));
        }

        public SearchProjectQueryResult GetSearchProject(SearchProjectQuery query)
        {
            //var queryParam = new NameValueCollection
            //{
            //    {"SearchCriteria", query.SearchCriteria },
            //    {"SelectedPagee", query.SelectedPage.ToString()},
            //    {"PageSize", query.PageSize.ToString() }
            //};
            string route = "SearchProject/"+ query.SearchCriteria + "/" +query.SelectedPage.ToString()
                            + "/" + query.PageSize.ToString();
            return Task.Run(() => Get<SearchProjectQueryResult>(route)).Result;
        }
    }
}
