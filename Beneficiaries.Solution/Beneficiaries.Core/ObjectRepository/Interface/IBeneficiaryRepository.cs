using Beneficiaries.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Beneficiaries.Core.ObjectRepository.Interface
{
    public interface IBeneficiaryRepository
    {
        Task<int> Add(BeneficiaryDTO beneficiary);

        Task Update(BeneficiaryDTO beneficiary);

        Task<string> Delete(int id);

        Task<List<BeneficiaryDTO>> GetAllBeneficiaries();

        Task<BeneficiaryDTO> ObtXId(double Id);
    }
}
