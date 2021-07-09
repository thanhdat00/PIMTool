using AutoMapper;
using PIMTool.Common;
using PIMTool.Services.Resource;
using PIMTool.Services.Service;
using PIMTool.Services.Service.Entities;
using System.Collections.Generic;
using System.Web.Http;

namespace PIMTool.Services.Controllers
{
    [RoutePrefix(RouteConstants.EmployeeApi)]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [Route(RouteConstants.GetAllEmployees)]
        [HttpGet]
        public IEnumerable<EmployeeDto> GetProjects()
        {
            var employees = _employeeService.GetAll();
            var resources = _mapper.Map<IEnumerable<EmployeeEntity>, IEnumerable<EmployeeDto>>(employees);
            return resources;
        }

    }
}