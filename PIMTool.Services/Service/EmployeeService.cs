using PIMTool.Services.Service.Entities;
using PIMTool.Services.Service.Pattern;
using PIMTool.Services.Service.Repository;
using System.Collections.Generic;

namespace PIMTool.Services.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWorkProvider _unitOfWorkProvider;
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IUnitOfWorkProvider unitOfWorkProvider,
            IEmployeeRepository employeeRepository)
        {
            _unitOfWorkProvider = unitOfWorkProvider;
            _employeeRepository = employeeRepository;
        }
        public IList<EmployeeEntity> GetAll()
        {
            IList<EmployeeEntity> result;
            using (var scope = _unitOfWorkProvider.Provide())
            {
                result = _employeeRepository.GetAll();
                scope.Complete();
            }
            return result;
        }
    }
}