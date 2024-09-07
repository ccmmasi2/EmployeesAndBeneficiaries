using Beneficiaries.Core.BusinessLogic.Interfaces;
using Beneficiaries.Core.Models;
using Beneficiaries.Core.ObjectRepository.Interface;
using Beneficiaries.Core.Utilities;
using Microsoft.Extensions.Logging;

namespace Beneficiaries.Core.BusinessLogic.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository { get; set; }
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository employeeRepository, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<int> Add(EmployeeDTO employee)
        {
            try
            {
                return await _employeeRepository.Add(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding employee: {ex.Message}");
                throw;
            }
        }

        public async Task<string> Update(EmployeeDTO employee)
        {
            try
            {
                return await _employeeRepository.Update(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating employee with ID {employee.ID}: {ex.Message}");
                throw;
            }
        }

        public async Task<string> Delete(Int64 id)
        {
            try
            {
                return await _employeeRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting employee with ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<PagedList<EmployeeDTO>> ObtAll(int page = 1, int sizePage = 10, string sorting = "")
        {
            try
            {
                return await _employeeRepository.ObtAll(page, sizePage, sorting);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving all employees: {ex.Message}");
                throw;
            }
        }

        public async Task<EmployeeDTO> ObtXId(Int64 id)
        {
            try
            {
                return await _employeeRepository.ObtXId(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving employee with ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
