using PIMTool.Services.Resource;
using System.Collections.Generic;

namespace PIMTool.Client.WebApiClient.Services
{
    public interface IEmployeeWebApiClient
    {
        List<EmployeeDto> GetAllEmployees();
    }
}
