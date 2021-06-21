using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;
using Ninject;
using PIMTool.Common;
using PIMTool.Common.BusinessObjects;
using PIMTool.Services.Service;

namespace PIMTool.Services.Controllers
{
    [RoutePrefix(RouteConstants.ProjectApi)]
    public class ProjectController : ApiController
    {
        private readonly IProjectService _projectService;

        public ProjectController(
            IProjectService projectService)
        {
            _projectService = projectService;
        }

        [Route(RouteConstants.GetAllProjects)]
        [HttpGet]
        public IHttpActionResult GetProjects()
        {
            //[TODO] call real service
            return Ok(new Project {Name = "Test", Id = 1});
        }

        [Route(RouteConstants.GetProjectServer)]
        [HttpGet]
        public IHttpActionResult GetProject(int projectId)
        {
            //[TODO] call real service
            return Ok(new Project { Name = "Test2", Id = 2 });
        }
    }
}
