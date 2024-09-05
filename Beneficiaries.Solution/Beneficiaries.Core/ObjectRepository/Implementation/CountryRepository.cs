using Beneficiaries.Core.Data;
using Beneficiaries.Core.Models;
using Beneficiaries.Core.ObjectRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Beneficiaries.Core.ObjectRepository.Implementation
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext _context;

        public CountryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CountryDTO>> ObtAll()
        {
            return await _context.Countries
                .FromSqlRaw("EXEC GetAllCountries")
                .ToListAsync();
        }
    }
}
