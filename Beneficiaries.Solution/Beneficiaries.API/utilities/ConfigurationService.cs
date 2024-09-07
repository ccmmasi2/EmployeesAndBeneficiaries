using Beneficiaries.Core.Utilities;

namespace Beneficiaries.API.utilities
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("BeneficiariesConectionDB");
        }
    }
}
