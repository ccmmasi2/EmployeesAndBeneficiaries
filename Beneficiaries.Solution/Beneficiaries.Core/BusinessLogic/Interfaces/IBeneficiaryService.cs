﻿using Beneficiaries.Core.Models;
using Beneficiaries.Core.Utilities;

namespace Beneficiaries.Core.BusinessLogic.Interfaces
{
    public interface IBeneficiaryService
    {
        Task<int> Add(BeneficiaryDTO beneficiary);

        Task<string> Update(BeneficiaryDTO beneficiary);

        Task<string> Delete(Int64 id);

        Task<PagedList<BeneficiaryDTO>> ObtAll(int page = 1, int sizePage = 10, string sorting = "Id");

        Task<BeneficiaryDTO> ObtXId(Int64 id);

        Task<PagedList<BeneficiaryDTO>> ObtAllXEmployeeId(Int64 employeeId, int page = 1, int sizePage = 10, string sorting = "Id");
    }
}
