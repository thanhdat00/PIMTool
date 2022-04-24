using PIMTool.Services.Resource;
using System.Collections.Generic;

namespace PIMTool.Services.Service.Models
{
    public class SearchProjectQueryResult
    {
        public List<ProjectDto> ProjectItems { get; set; } = new List<ProjectDto>();
        public int TotalItems { get; set; } = 0;
        public SearchProjectQueryResult(List<ProjectDto> projectsResult, int totalItems)
        {
            ProjectItems = projectsResult;
            TotalItems = totalItems;
        }
    }
}