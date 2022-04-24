
using System.Collections.Generic;

namespace PIMTool.Services.Service
{
    public interface IEmployeeService
    {
        IList<Entities.EmployeeEntity> GetAll();
    }
}
