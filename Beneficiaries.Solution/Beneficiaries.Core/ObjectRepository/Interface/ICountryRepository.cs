using Beneficiaries.Core.Models;

namespace Beneficiaries.Core.ObjectRepository.Interface
{
    public interface ICountryRepository
    {
        Task<List<CountryDTO>> ObtAll();
    }
}
