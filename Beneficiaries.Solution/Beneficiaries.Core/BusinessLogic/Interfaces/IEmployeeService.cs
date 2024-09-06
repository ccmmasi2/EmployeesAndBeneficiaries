using Beneficiaries.Core.Models;
using Beneficiaries.Core.Utilities;

namespace Beneficiaries.Core.BusinessLogic.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> Add(EmployeeDTO employee);

        Task<string> Update(EmployeeDTO employee);

        Task<string> Delete(Int64 id);

        Task<PagedList<EmployeeDTO>> ObtAll(int page = 1, int sizePage = 10, string sorting = "Id");

        Task<EmployeeDTO> ObtXId(Int64 id);
    }
}
