using Beneficiaries.Core.Models;

namespace Beneficiaries.Core.BusinessLogic.Interfaces
{
    public interface IBeneficiaryService
    {
        Task<int> Add(BeneficiaryDTO beneficiary);

        Task<string> Update(BeneficiaryDTO beneficiary);

        Task<string> Delete(double id);

        Task<List<BeneficiaryDTO>> GetAll();

        Task<BeneficiaryDTO> ObtXId(double id);
    }
}
