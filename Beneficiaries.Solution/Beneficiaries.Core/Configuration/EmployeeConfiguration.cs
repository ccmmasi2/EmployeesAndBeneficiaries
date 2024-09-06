using Beneficiaries.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beneficiaries.Core.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeDTO>
    {
        public void Configure(EntityTypeBuilder<EmployeeDTO> builder)
        {
            builder.ToTable("EMPLOYEES");

            builder.Property(c => c.ID).IsRequired();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
            builder.Property(c => c.LastName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.BirthDay).IsRequired();
            builder.Property(c => c.CURP).IsRequired().HasMaxLength(10);
            builder.Property(c => c.SSN).IsRequired().HasMaxLength(10);
            builder.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(10); 
            builder.Property(c => c.CountryId).IsRequired();
            builder.Property(c => c.EmployeeNumber).IsRequired();

            builder.HasOne(e => e.Country)
                   .WithMany()
                   .HasForeignKey(e => e.CountryId)
                   .OnDelete(DeleteBehavior.Restrict); 

            builder.HasIndex(e => e.EmployeeNumber)
                   .IsUnique();
        }
    }
}
