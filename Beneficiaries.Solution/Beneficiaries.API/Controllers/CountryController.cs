using Beneficiaries.Core.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Beneficiaries.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly ILogger<CountryController> _logger;

        public CountryController(ICountryService countryService, ILogger<CountryController> logger)
        {
            _countryService = countryService;
            _logger = logger;
        }

        [HttpGet("ObtAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<object>>> ObtAll()
        {
            _logger.LogInformation("Get list");
            var LItems = await _countryService.ObtAll();
            return Ok(LItems);
        }
    }
}
