using Beneficiaries.Core.Models;
using Beneficiaries.Core.ObjectRepository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beneficiaries.Core.BusinessLogic.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> Add(EmployeeDTO employee);

        Task<string> Update(EmployeeDTO employee);

        Task<string> Delete(double id);

        Task<List<EmployeeDTO>> GetAllEmployees();

        Task<EmployeeDTO> ObtXId(double id);
    }
}
