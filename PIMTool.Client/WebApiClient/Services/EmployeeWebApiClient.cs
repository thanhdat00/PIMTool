using PIMTool.Common;
using PIMTool.Services.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Client.WebApiClient.Services
{
    public class EmployeeWebApiClient : WebApiClientBase, IEmployeeWebApiClient
    {
        public EmployeeWebApiClient(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
    {
    }

    public override string RoutePrefix => RouteConstants.EmployeeApi;

        public List<EmployeeDto> GetAllEmployees()
        {
            return Task.Run(() => Get<List<EmployeeDto>>(RouteConstants.GetAllEmployees)).Result;
        }
    }
}
