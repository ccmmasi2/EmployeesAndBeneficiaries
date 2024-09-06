using Beneficiaries.Core.Models;

namespace Beneficiaries.Core.ObjectRepository.Interface
{
    public interface IEmployeeRepository
    {
        Task<int> Add(EmployeeDTO employee);

        Task<string> Update(EmployeeDTO employee);

        Task<string> Delete(Int64 id);

        Task<List<EmployeeDTO>> ObtAll();

        Task<EmployeeDTO> ObtXId(Int64 id);
    }
}
