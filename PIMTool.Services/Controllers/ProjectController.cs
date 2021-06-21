using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using PIMTool.Common;
using PIMTool.Services.Resource;
using PIMTool.Services.Service;
using PIMTool.Services.Service.Entities;

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
        public IEnumerable<ProjectResource> GetProjects()
        {
            //[TODO] call real service
            var projects = _projectService.GetAll();
            var resources = _mapper.Map<IEnumerable<ProjectEntity>, IEnumerable<ProjectResource>>(projects);
            return resources;
        }

        [Route(RouteConstants.GetProjectServer)]
        [HttpGet]
        public ProjectEntity GetProject(int projectId)
        {
            //[TODO] call real service
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

            var projectResource = _mapper.Map<ProjectEntity, ProjectResource>(result.Resource);
            return Ok(projectResource);
        }

        [Route(RouteConstants.AddProject)]
        [HttpPost]
        public IHttpActionResult AddProject([FromBody] SaveProjectResource resource)
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
        public IHttpActionResult UpdateProject([FromBody] SaveProjectResource resource)
        {
            var result = _projectService.Update(resource);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Resource);
        }
    }
}
