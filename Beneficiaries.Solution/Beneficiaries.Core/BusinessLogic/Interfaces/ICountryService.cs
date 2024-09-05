using Beneficiaries.Core.Models;

namespace Beneficiaries.Core.BusinessLogic.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryDTO>> ObtAll();
    }
}
