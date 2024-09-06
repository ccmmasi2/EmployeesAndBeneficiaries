using Beneficiaries.Core.Models;

namespace Beneficiaries.Core.ObjectRepository.Interface
{
    public interface IBeneficiaryRepository
    {
        Task<int> Add(BeneficiaryDTO beneficiary);

        Task<string> Update(BeneficiaryDTO beneficiary);

        Task<string> Delete(Int64 id);

        Task<List<BeneficiaryDTO>> ObtAll(int page = 1, int sizePage = 10, string sorting = "");

        Task<BeneficiaryDTO> ObtXId(Int64 id);

        Task<List<BeneficiaryDTO>> ObtAllXEmployeeId(Int64 employeeId, int page = 1, int sizePage = 10, string sorting = "");
    }
}
