using Beneficiaries.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Beneficiaries.Core.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<CountryDTO> Countries { get; set; }
        public DbSet<EmployeeDTO> Employees { get; set; }
        public DbSet<BeneficiarieDTO> Beneficiaries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

