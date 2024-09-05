using Beneficiaries.Core.Models;

namespace Beneficiaries.Core.ObjectRepository.Interface
{
    public interface IBeneficiaryRepository
    {
        Task<int> Add(BeneficiaryDTO beneficiary);

        Task<string> Update(BeneficiaryDTO beneficiary);

        Task<string> Delete(double id);

        Task<List<BeneficiaryDTO>> GetAll();

        Task<BeneficiaryDTO> ObtXId(double id);
    }
}
