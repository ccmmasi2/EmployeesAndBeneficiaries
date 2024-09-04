using Beneficiaries.Core.Data;
using Beneficiaries.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Beneficiaries.Core.Init
{
    public class DBInitializer : IDBInitializer
    {
        private readonly AppDbContext _db;

        public DBInitializer(AppDbContext db)
        {
            _db = db;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (!_db.Countries.Any())
            {
                var LDataJson = File.ReadAllText("../Beneficiaries.Core/Data/SeedData/Countries.json");
                var LData = JsonSerializer.Deserialize<List<CountryDTO>>(LDataJson);
                _db.Countries.AddRange(LData);
            }

            if (_db.ChangeTracker.HasChanges())
                _db.SaveChanges();
        }
    }
}
