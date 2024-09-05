using Beneficiaries.Core.Models;

namespace Beneficiaries.Core.BusinessLogic.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> Add(EmployeeDTO employee);

        Task<string> Update(EmployeeDTO employee);

        Task<string> Delete(double id);

        Task<List<EmployeeDTO>> ObtAll();

        Task<EmployeeDTO> ObtXId(double id);
    }
}
