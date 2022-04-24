using PIMTool.Services.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIMTool.Services.Service.Communication
{
    public class ProjectResponse : BaseResponse<ProjectEntity>
    {
        public ProjectResponse(ProjectEntity project) : base(project)
        { }
        public ProjectResponse(string message) : base(message)
        { }
    }
}