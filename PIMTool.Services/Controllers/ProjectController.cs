using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using PIMTool.Common;
using PIMTool.Services.Resource;
using PIMTool.Services.Service;
using PIMTool.Services.Service.Entities;
using PIMTool.Services.Service.Models;

namespace PIMTool.Services.Controllers
{
    [RoutePrefix(RouteConstants.ProjectApi)]
    public class ProjectController : ApiController
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        public ProjectController(
            IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        [Route(RouteConstants.GetAllProjects)]
        [HttpGet]
        public IEnumerable<ProjectDto> GetProjects()
        {
            var projects = _projectService.GetAll();
            var resources = _mapper.Map<IEnumerable<ProjectEntity>, IEnumerable<ProjectDto>>(projects);
            return resources;
        }

        [Route(RouteConstants.GetProjectServer)]
        [HttpGet]
        public ProjectEntity GetProject(int projectId)
        {
            return _projectService.GetById(projectId);
        }

        [Route(RouteConstants.DeleteProject)]
        [HttpDelete]
        public IHttpActionResult DeleteProject(int projectNumber)
        {
            var result = _projectService.DeleteByProjectNumber(projectNumber);

            if (!result.Success)
            {
                return BadRequest(result.Message);  
            }

            var projectResponse = _mapper.Map<ProjectEntity, ProjectDto>(result.Resource);
            return Ok(projectResponse);
        }

        [Route(RouteConstants.AddProject)]
        [HttpPost]
        public IHttpActionResult AddProject([FromBody] SaveProjectDto resource)
        {            
            var result = _projectService.Save(resource);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Resource);
        }

        [Route(RouteConstants.UpdateProject)]
        [HttpPut]
        public IHttpActionResult UpdateProject([FromBody] SaveProjectDto resource)
        {
            var result = _projectService.Update(resource);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Resource);
        }

        [Route(RouteConstants.SearchProject)]
        [HttpGet]
        public SearchProjectQueryResult GetSearchProjects(int selectedPage, int pageSize, string searchCriteria)
        {
            var query = new SearchProjectQuery();
            query.SearchCriteria = searchCriteria;
            query.SelectedPage = selectedPage;
            query.PageSize = pageSize;
            var result = _projectService.GetSearchProject(query);
            return result;
        }
    }
}
