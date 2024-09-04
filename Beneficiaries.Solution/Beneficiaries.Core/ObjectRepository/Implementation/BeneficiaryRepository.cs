using Beneficiaries.Core.Data;
using Beneficiaries.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Beneficiaries.Core.ObjectRepository.Implementation
{
    public class BeneficiaryRepository
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
                beneficiary.SSN, beneficiary.PhoneNUmber, beneficiary.CountryId, beneficiary.ParticipationPercentaje);

            return newBeneficiaryId;
        }

        public async Task Update(BeneficiaryDTO beneficiary)
        {
            await _context.Database.ExecuteSqlRawAsync(@"
                EXEC UpdateBeneficiary 
                @Id = {0}, @Name = {1}, @LastName = {2}, @BirthDay = {3}, 
                @CURP = {4}, @SSN = {5}, @PhoneNumber = {6}, @CountryId = {7}, 
                @ParticipationPercentaje = {8}",
                beneficiary.ID, beneficiary.Name, beneficiary.LastName, beneficiary.BirthDay,
                beneficiary.CURP, beneficiary.SSN, beneficiary.PhoneNUmber,
                beneficiary.CountryId, beneficiary.ParticipationPercentaje);
        }

        public async Task<string> Delete(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteBeneficiary @Id = {0}", id);
            return "Beneficiary deleted successfully";
        }

        public async Task<List<BeneficiaryDTO>> GetAllBeneficiaries()
        {
            return await _context.Beneficiaries
                .FromSqlRaw("EXEC GetAllBeneficiaries")
                .ToListAsync();
        }

        public async Task<BeneficiaryDTO> ObtXId(double Id)
        {
            return null;
        }

        public async Task<BeneficiaryDTO> ObtXFilter(string Filter)
        {
            return null;
        }
    }
}
