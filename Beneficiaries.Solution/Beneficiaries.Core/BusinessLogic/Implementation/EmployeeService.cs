using Beneficiaries.Core.BusinessLogic.Interfaces;
using Beneficiaries.Core.Models;
using Beneficiaries.Core.ObjectRepository.Interface;

namespace Beneficiaries.Core.BusinessLogic.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository { get; set; } 

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<int> Add(EmployeeDTO employee)
        {
            return await _employeeRepository.Add(employee);
        }

        public async Task<string> Update(EmployeeDTO employee)
        {
            return await _employeeRepository.Update(employee);
        }

        public async Task<string> Delete(double id)
        {
            return await _employeeRepository.Delete(id);
        }

        public async Task<List<EmployeeDTO>> ObtAll()
        {
            return await _employeeRepository.ObtAll();
        }

        public async Task<EmployeeDTO> ObtXId(double id)
        {
            return await _employeeRepository.ObtXId(id);
        }
    }
}
