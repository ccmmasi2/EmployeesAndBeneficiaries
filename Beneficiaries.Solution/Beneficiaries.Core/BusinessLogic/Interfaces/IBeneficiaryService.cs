using Beneficiaries.Core.Models;

namespace Beneficiaries.Core.BusinessLogic.Interfaces
{
    public interface IBeneficiaryService
    {
        Task<int> Add(BeneficiaryDTO beneficiary);

        Task<string> Update(BeneficiaryDTO beneficiary);

        Task<string> Delete(Int64 id);

        Task<List<BeneficiaryDTO>> ObtAll();

        Task<BeneficiaryDTO> ObtXId(Int64 id);

        Task<List<BeneficiaryDTO>> ObtAllXEmployeeId(Int64 employeeId);
    }
}
