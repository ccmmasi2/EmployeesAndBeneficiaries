using Beneficiaries.Core.Data;
using Beneficiaries.Core.Models;
using Beneficiaries.Core.ObjectRepository.Interface;
using Beneficiaries.Core.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Beneficiaries.Core.ObjectRepository.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfigurationService _configurationService;


        public EmployeeRepository(AppDbContext context, IConfigurationService configurationService)
        {
            _context = context;
            _configurationService = configurationService;
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

            return "Empleado actualizado";
        }

        public async Task<string> Delete(Int64 id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteEmployee @Id = {0}", id);
            return "Empleado eliminado!";
        } 

        public async Task<PagedList<EmployeeDTO>> ObtAll(int page = 1, int sizePage = 10, string sorting = "Id")
        {
            var employees = new List<EmployeeDTO>();
            var query = $"EXEC GetAllEmployees @page={page}, @sizePage={sizePage}, @sorting='{sorting}'";

            var result = _context.Employees.FromSqlRaw(query).AsNoTracking();

            employees = await result.ToListAsync();

            var totalRecords = await _context.Employees.CountAsync();

            return new PagedList<EmployeeDTO>(employees, totalRecords, page, sizePage);
        }

        public async Task<PagedList<EmployeeReport>> ObtAllDAO(int page = 1, int sizePage = 10, string sorting = "Id")
        {
            var connectionString = _configurationService.GetConnectionString();
            var employees = new List<EmployeeReport>();
            int totalCount = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetAllEmployees", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Page", page));
                    command.Parameters.Add(new SqlParameter("@SizePage", sizePage));
                    command.Parameters.Add(new SqlParameter("@Sorting", sorting));
                    command.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                    connection.Open();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var employee = new EmployeeReport
                            {
                                ID = reader.GetInt64(reader.GetOrdinal("ID")),
                                Name = reader.GetString(reader.GetOrdinal("NAME")),
                                LastName = reader.GetString(reader.GetOrdinal("LASTNAME")),
                                BirthDay = reader.GetDateTime(reader.GetOrdinal("BIRTHDAY")),
                                CURP = reader.IsDBNull(reader.GetOrdinal("CURP")) ? null : reader.GetString(reader.GetOrdinal("CURP")),
                                SSN = reader.IsDBNull(reader.GetOrdinal("SSN")) ? null : reader.GetString(reader.GetOrdinal("SSN")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("PHONENUMBER")),
                                EmployeeNumber = reader.GetInt64(reader.GetOrdinal("EMPLOYEENUMBER")),
                                CountryName = reader.IsDBNull(reader.GetOrdinal("CountryName")) ? null : reader.GetString(reader.GetOrdinal("CountryName"))
                            };
                            employees.Add(employee);
                        }
                    }

                    totalCount = (int)command.Parameters["@TotalCount"].Value;
                }
            }
            return new PagedList<EmployeeReport>(employees, totalCount, page, sizePage); ;
        }

        public async Task<EmployeeDTO> ObtXId(Int64 id)
        {
            var employee = _context.Employees
            .FromSqlRaw("EXEC GetEmployeeById @Id = {0}", id)
            .AsEnumerable()
            .FirstOrDefault();

            return employee;
        } 
    }
}
