using Beneficiaries.Core.BusinessLogic.Interfaces;
using Beneficiaries.Core.Models;
using Beneficiaries.Core.ObjectRepository.Interface;
using Microsoft.Extensions.Logging;

namespace Beneficiaries.Core.BusinessLogic.Implementation
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private IBeneficiaryRepository _beneficiaryRepository { get; set; }
        private readonly ILogger<BeneficiaryService> _logger;

        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository, ILogger<BeneficiaryService> logger)
        {
            _beneficiaryRepository = beneficiaryRepository;
            _logger = logger;
        }

        public async Task<int> Add(BeneficiaryDTO beneficiary)
        {
            try
            {
                return await _beneficiaryRepository.Add(beneficiary);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding beneficiary: {ex.Message}");
                throw;
            }
        }

        public async Task<string> Update(BeneficiaryDTO beneficiary)
        {
            try
            {
                return await _beneficiaryRepository.Update(beneficiary);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating beneficiary with ID {beneficiary.ID}: {ex.Message}");
                throw;
            }
        }

        public async Task<string> Delete(Int64 id)
        {
            try
            {
                return await _beneficiaryRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting beneficiary with ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<List<BeneficiaryDTO>> ObtAll()
        {
            try
            {
                return await _beneficiaryRepository.ObtAll();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving all beneficiaries: {ex.Message}");
                throw;
            }
        }

        public async Task<BeneficiaryDTO> ObtXId(Int64 id)
        {
            try
            {
                return await _beneficiaryRepository.ObtXId(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving beneficiary with ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<List<BeneficiaryDTO>> ObtAllXEmployeeId(Int64 employeeId)
        {
            try
            {
                return await _beneficiaryRepository.ObtAllXEmployeeId(employeeId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving all beneficiaries by Employee id {employeeId}: {ex.Message}");
                throw;
            }
        }
    }
}
