using Beneficiaries.Core.BusinessLogic.Interfaces;
using Beneficiaries.Core.Models;
using Beneficiaries.Core.ObjectRepository.Interface;

namespace Beneficiaries.Core.BusinessLogic.Implementation
{
    public class CountryService : ICountryService
    {
        private ICountryRepository _countryRepository { get; set; }

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<List<CountryDTO>> ObtAll()
        {
            return await _countryRepository.ObtAll();
        }
    }
}
