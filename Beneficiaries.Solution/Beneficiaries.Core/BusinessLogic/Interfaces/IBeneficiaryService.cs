using Beneficiaries.Core.Models;
using Beneficiaries.Core.ObjectRepository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beneficiaries.Core.BusinessLogic.Interfaces
{
    public interface IBeneficiaryService
    {
        Task<int> Add(BeneficiaryDTO beneficiary);

        Task<string> Update(BeneficiaryDTO beneficiary);

        Task<string> Delete(double id);

        Task<List<BeneficiaryDTO>> GetAllBeneficiaries();

        Task<BeneficiaryDTO> ObtXId(double id);
    }
}
