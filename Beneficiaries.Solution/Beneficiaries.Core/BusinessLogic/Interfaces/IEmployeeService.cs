using Beneficiaries.Core.Models;

namespace Beneficiaries.Core.BusinessLogic.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> Add(EmployeeDTO employee);

        Task<string> Update(EmployeeDTO employee);

        Task<string> Delete(Int64 id);

        Task<List<EmployeeDTO>> ObtAll();

        Task<EmployeeDTO> ObtXId(Int64 id);
    }
}
