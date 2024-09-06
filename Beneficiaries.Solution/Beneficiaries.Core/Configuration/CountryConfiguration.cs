using Beneficiaries.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beneficiaries.Core.Configuration
{
    public class CountryConfiguration : IEntityTypeConfiguration<CountryDTO>
    {
        public void Configure(EntityTypeBuilder<CountryDTO> builder)
        {
            builder.ToTable("COUNTRIES");

            builder.Property(c => c.ID).IsRequired();
            builder.Property(c => c.Name).IsRequired();
        }
    }
}