using Beneficiaries.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;

namespace Beneficiaries.Core.Data
{
    public class AppDbContext : DbContext
    { 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<CountryDTO> Countries { get; set; }
        public DbSet<EmployeeDTO> Employees { get; set; }
        public DbSet<BeneficiaryDTO> Beneficiaries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<EmployeeDTO>()
                .HasIndex(e => e.EmployeeNumber)
            .IsUnique();

            builder.Entity<BeneficiaryDTO>()
                .HasOne(b => b.Employee)
                .WithMany()   
                .HasForeignKey(b => b.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

