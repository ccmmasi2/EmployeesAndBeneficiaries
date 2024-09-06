using Beneficiaries.Core.Data;
using Beneficiaries.Core.Models;
using Beneficiaries.Core.ObjectRepository.Interface;
using Beneficiaries.Core.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Beneficiaries.Core.ObjectRepository.Implementation
{
    public class BeneficiaryRepository: IBeneficiaryRepository
    {
        private readonly AppDbContext _context;

        public BeneficiaryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(BeneficiaryDTO beneficiary)
        {
            var newBeneficiaryId = await _context.Database.ExecuteSqlRawAsync(@"
                EXEC InsertBeneficiary 
                @Name = {0}, @LastName = {1}, @BirthDay = {2}, @CURP = {3}, 
                @SSN = {4}, @PhoneNumber = {5}, @CountryId = {6}, @ParticipationPercentaje = {7}",
                beneficiary.Name, beneficiary.LastName, beneficiary.BirthDay, beneficiary.CURP,
                beneficiary.SSN, beneficiary.PhoneNumber, beneficiary.CountryId, beneficiary.ParticipationPercentaje);

            return newBeneficiaryId;
        }

        public async Task<string> Update(BeneficiaryDTO beneficiary)
        {
            await _context.Database.ExecuteSqlRawAsync(@"
                EXEC UpdateBeneficiary 
                @Id = {0}, @Name = {1}, @LastName = {2}, @BirthDay = {3}, 
                @CURP = {4}, @SSN = {5}, @PhoneNumber = {6}, @CountryId = {7}, 
                @ParticipationPercentaje = {8}",
                beneficiary.ID, beneficiary.Name, beneficiary.LastName, beneficiary.BirthDay,
                beneficiary.CURP, beneficiary.SSN, beneficiary.PhoneNumber,
                beneficiary.CountryId, beneficiary.ParticipationPercentaje);

            return "Beneficiary updated successfully";
        }

        public async Task<string> Delete(Int64 id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteBeneficiary @Id = {0}", id);
            return "Beneficiary deleted successfully";
        }

        public async Task<PagedList<BeneficiaryDTO>> ObtAll(int page = 1, int sizePage = 10, string sorting = "Id")
        {
            var beneficiaries = new List<BeneficiaryDTO>();
            var query = $"EXEC GetAllEmployees @page={page}, @sizePage={sizePage}, @sorting='{sorting}'";

            var result = _context.Beneficiaries.FromSqlRaw(query).AsNoTracking();

            beneficiaries = await result.ToListAsync();

            var totalRecords = await _context.Beneficiaries.CountAsync();

            return new PagedList<BeneficiaryDTO>(beneficiaries, totalRecords, page, sizePage);
        } 

        public async Task<BeneficiaryDTO> ObtXId(Int64 id)
        {
            var beneficiary = await _context.Beneficiaries
            .FromSqlRaw("EXEC GetBeneficiaryById @Id = {0}", id)
            .FirstOrDefaultAsync();

            return beneficiary;
        }

        public async Task<PagedList<BeneficiaryDTO>> ObtAllXEmployeeId(Int64 employeeId, int page = 1, int sizePage = 10, string sorting = "Id")
        {
            var beneficiaries = new List<BeneficiaryDTO>();
            var query = $"EXEC GetBeneficiaryByEmployeeId @EmployeeId={employeeId}, @page={page}, @sizePage={sizePage}, @sorting='{sorting}'";

            var result = _context.Beneficiaries.FromSqlRaw(query).AsNoTracking();

            beneficiaries = await result.ToListAsync();

            var totalRecords = await _context.Beneficiaries.CountAsync();

            return new PagedList<BeneficiaryDTO>(beneficiaries, totalRecords, page, sizePage);
        }
    }
}
