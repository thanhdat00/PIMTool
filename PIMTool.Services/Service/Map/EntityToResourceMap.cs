using AutoMapper;
using PIMTool.Services.Resource;
using PIMTool.Services.Service.Entities;

namespace PIMTool.Services.Service.Map
{
    public class EntityToResourceMap : Profile
    {
        public EntityToResourceMap()
        {
            CreateMap<ProjectEntity, ProjectDto>().ForMember(d => d.Member, m => m.MapFrom(s =>
              s.GetMember()));

            CreateMap<EmployeeEntity, EmployeeDto>();
        }
    }
}