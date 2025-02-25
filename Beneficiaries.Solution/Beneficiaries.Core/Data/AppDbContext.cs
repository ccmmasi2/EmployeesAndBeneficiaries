﻿using Beneficiaries.Core.Configuration;
using Beneficiaries.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new EmployeeConfiguration());
            builder.ApplyConfiguration(new BeneficiaryConfiguration());

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

