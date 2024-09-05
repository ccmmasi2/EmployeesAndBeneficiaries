using Beneficiaries.Core.BusinessLogic.Interfaces;
using Beneficiaries.Core.Models;
using Beneficiaries.Core.ObjectRepository.Interface;

namespace Beneficiaries.Core.BusinessLogic.Implementation
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private IBeneficiaryRepository _beneficiaryRepository { get; set; }

        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository)
        {
            _beneficiaryRepository = beneficiaryRepository;
        }

        public async Task<int> Add(BeneficiaryDTO beneficiary)
        {
            return await _beneficiaryRepository.Add(beneficiary);
        }

        public async Task<string> Update(BeneficiaryDTO beneficiary)
        {
            return await _beneficiaryRepository.Update(beneficiary);
        }

        public async Task<string> Delete(double id)
        {
            return await _beneficiaryRepository.Delete(id);
        }

        public async Task<List<BeneficiaryDTO>> ObtAll()
        {
            return await _beneficiaryRepository.ObtAll();
        }

        public async Task<BeneficiaryDTO> ObtXId(double id)
        {
            return await _beneficiaryRepository.ObtXId(id);
        }

        public async Task<List<BeneficiaryDTO>> ObtAllXEmployeeId(double employeeId)
        {
            return await _beneficiaryRepository.ObtAllXEmployeeId(employeeId);
        }
    }
}
