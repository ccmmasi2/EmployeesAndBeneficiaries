using Beneficiaries.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Beneficiaries.Core.ObjectRepository.Interface
{
    public interface IEmployeeRepository
    {
        Task<int> Add(EmployeeDTO employee);

        Task Update(EmployeeDTO employee);

        Task<string> Delete(int id);

        Task<List<EmployeeDTO>> GetAllEmployees();

        Task<EmployeeDTO> ObtXId(double Id);
    }
}
