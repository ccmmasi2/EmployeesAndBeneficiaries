using Beneficiaries.Core.Data;
using Beneficiaries.Core.Models;
using Beneficiaries.Core.ObjectRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Beneficiaries.Core.ObjectRepository.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(EmployeeDTO employee)
        {
            var newEmployeeId = await _context.Database.ExecuteSqlRawAsync(@"
                EXEC InsertEmployee 
                @Name = {0}, @LastName = {1}, @BirthDay = {2}, @CURP = {3}, 
                @SSN = {4}, @PhoneNumber = {5}, @CountryId = {6}, @EmployeeNumber = {7}",
                employee.Name, employee.LastName, employee.BirthDay, employee.CURP,
                employee.SSN, employee.PhoneNumber, employee.CountryId, employee.EmployeeNumber);

            return newEmployeeId;
        }

        public async Task<string> Update(EmployeeDTO employee)
        {
            await _context.Database.ExecuteSqlRawAsync(@"
                EXEC UpdateEmployee 
                @Id = {0}, @Name = {1}, @LastName = {2}, @BirthDay = {3}, 
                @CURP = {4}, @SSN = {5}, @PhoneNumber = {6}, @CountryId = {7}, 
                @EmployeeNumber = {8}",
                employee.ID, employee.Name, employee.LastName, employee.BirthDay,
                employee.CURP, employee.SSN, employee.PhoneNumber,
                employee.CountryId, employee.EmployeeNumber);

            return "Employee updated successfully";
        }

        public async Task<string> Delete(Int64 id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteEmployee @Id = {0}", id);
            return "Employee deleted successfully";
        }

        public async Task<List<EmployeeDTO>> ObtAll()
        {
            return await _context.Employees
                .FromSqlRaw("EXEC GetAllEmployees")
                .ToListAsync();
        }

        public async Task<EmployeeDTO> ObtXId(Int64 id)
        {
            var employee = await _context.Employees
            .FromSqlRaw("EXEC GetEmployeeById @Id = {0}", id)
            .FirstOrDefaultAsync();

            return employee;
        } 
    }
}
