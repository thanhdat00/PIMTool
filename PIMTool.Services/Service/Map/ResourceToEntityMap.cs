using AutoMapper;
using PIMTool.Services.Resource;
using PIMTool.Services.Service.Entities;

namespace PIMTool.Services.Service.Map
{
    public class ResourceToEntityMap : Profile
    {
        public ResourceToEntityMap()
        {
            CreateMap<SaveProjectResource, ProjectEntity>();
        }
    }
}