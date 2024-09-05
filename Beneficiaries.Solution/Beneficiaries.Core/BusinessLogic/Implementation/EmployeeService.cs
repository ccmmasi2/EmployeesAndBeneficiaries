using Beneficiaries.Core.BusinessLogic.Interfaces;
using Beneficiaries.Core.Models;
using Beneficiaries.Core.ObjectRepository.Interface;

namespace Beneficiaries.Core.BusinessLogic.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        public IEmployeeRepository EmployeeRepository { get; set; } 

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            EmployeeRepository = employeeRepository;
        }

        public async Task<int> Add(EmployeeDTO employee)
        {
            return await EmployeeRepository.Add(employee);
        }

        public async Task<string> Update(EmployeeDTO employee)
        {
            return await EmployeeRepository.Update(employee);
        }

        public async Task<string> Delete(double id)
        {
            return await EmployeeRepository.Delete(id);
        }

        public async Task<List<EmployeeDTO>> GetAllEmployees()
        {
            return await EmployeeRepository.GetAllEmployees();
        }

        public async Task<EmployeeDTO> ObtXId(double id)
        {
            return await EmployeeRepository.ObtXId(id);
        }
    }
}
