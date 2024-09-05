using Beneficiaries.Core.BusinessLogic.Interfaces;
using Beneficiaries.Core.Models;
using Beneficiaries.Core.ObjectRepository.Interface;

namespace Beneficiaries.Core.BusinessLogic.Implementation
{
    public class BeneficiaryService : IBeneficiaryService
    {
        public IBeneficiaryRepository BeneficiaryRepository { get; set; }

        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository)
        {
            BeneficiaryRepository = beneficiaryRepository;
        }

        public async Task<int> Add(BeneficiaryDTO beneficiary)
        {
            return await BeneficiaryRepository.Add(beneficiary);
        }

        public async Task<string> Update(BeneficiaryDTO beneficiary)
        {
            return await BeneficiaryRepository.Update(beneficiary);
        }

        public async Task<string> Delete(double id)
        {
            return await BeneficiaryRepository.Delete(id);
        }

        public async Task<List<BeneficiaryDTO>> GetAllBeneficiaries()
        {
            return await BeneficiaryRepository.GetAllBeneficiaries();
        }

        public async Task<BeneficiaryDTO> ObtXId(double id)
        {
            return await BeneficiaryRepository.ObtXId(id);
        }
    }
}
