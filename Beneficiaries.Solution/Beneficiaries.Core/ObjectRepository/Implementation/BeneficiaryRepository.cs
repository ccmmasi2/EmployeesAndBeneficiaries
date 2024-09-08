using Beneficiaries.Core.Data;
using Beneficiaries.Core.Models;
using Beneficiaries.Core.ObjectRepository.Interface;
using Beneficiaries.Core.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Beneficiaries.Core.ObjectRepository.Implementation
{
    public class BeneficiaryRepository: IBeneficiaryRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfigurationService _configurationService;

        public BeneficiaryRepository(AppDbContext context, IConfigurationService configurationService)
        {
            _context = context;
            _configurationService = configurationService;
        }

        public async Task<int> Add(BeneficiaryDTO beneficiary)
        {
            var newBeneficiaryId = await _context.Database.ExecuteSqlRawAsync(@"
                EXEC InsertBeneficiary 
                @Name = {0}, @LastName = {1}, @BirthDay = {2}, @CURP = {3}, 
                @SSN = {4}, @PhoneNumber = {5}, @CountryId = {6}, @EmployeeId = {7}, @ParticipationPercentaje = {8}",
                beneficiary.Name, beneficiary.LastName, beneficiary.BirthDay, beneficiary.CURP,
                beneficiary.SSN, beneficiary.PhoneNumber, beneficiary.CountryId, beneficiary.EmployeeId, beneficiary.ParticipationPercentaje);

            return newBeneficiaryId;
        }

        public async Task<string> Update(BeneficiaryDTO beneficiary)
        {
            await _context.Database.ExecuteSqlRawAsync(@"
                EXEC UpdateBeneficiary 
                @Id = {0}, @Name = {1}, @LastName = {2}, @BirthDay = {3}, 
                @CURP = {4}, @SSN = {5}, @PhoneNumber = {6}, @CountryId = {7}, 
                @ParticipationPercentaje = {8}",
                beneficiary.ID, beneficiary.Name, beneficiary.LastName, beneficiary.BirthDay,
                beneficiary.CURP, beneficiary.SSN, beneficiary.PhoneNumber,
                beneficiary.CountryId, beneficiary.ParticipationPercentaje);

            return "Beneficiario actualizado!";
        }

        public async Task<string> Delete(Int64 id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteBeneficiary @Id = {0}", id);
            return "Beneficiario eliminado";
        }

        public async Task<PagedList<BeneficiaryDTO>> ObtAll(int page = 1, int sizePage = 10, string sorting = "Id")
        {
            var beneficiaries = new List<BeneficiaryDTO>();
            var query = $"EXEC GetAllBeneficiaries @page={page}, @sizePage={sizePage}, @sorting='{sorting}'";

            var result = _context.Beneficiaries.FromSqlRaw(query).AsNoTracking();

            beneficiaries = await result.ToListAsync();

            var totalRecords = await _context.Beneficiaries.CountAsync();

            return new PagedList<BeneficiaryDTO>(beneficiaries, totalRecords, page, sizePage);
        }

        public async Task<PagedList<BeneficiaryReport>> ObtAllDAO(int page = 1, int sizePage = 10, string sorting = "Id")
        {
            var connectionString = _configurationService.GetConnectionString();
            var beneficiaries = new List<BeneficiaryReport>();
            int totalCount = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetAllBeneficiaries", connection))
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
                            var beneficiary = new BeneficiaryReport
                            {
                                ID = reader.GetInt64(reader.GetOrdinal("ID")),
                                Name = reader.GetString(reader.GetOrdinal("NAME")),
                                LastName = reader.GetString(reader.GetOrdinal("LASTNAME")),
                                BirthDay = reader.GetDateTime(reader.GetOrdinal("BIRTHDAY")),
                                CURP = reader.IsDBNull(reader.GetOrdinal("CURP")) ? null : reader.GetString(reader.GetOrdinal("CURP")),
                                SSN = reader.IsDBNull(reader.GetOrdinal("SSN")) ? null : reader.GetString(reader.GetOrdinal("SSN")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("PHONENUMBER")),
                                ParticipationPercentaje = reader.GetInt32(reader.GetOrdinal("PARTICIPATIONPERCENTAJE")),
                                EmployeeName = reader.IsDBNull(reader.GetOrdinal("EmployeeName")) ? null : reader.GetString(reader.GetOrdinal("EmployeeName")),
                                CountryId = reader.GetInt32(reader.GetOrdinal("CountryId")),
                                CountryName = reader.IsDBNull(reader.GetOrdinal("CountryName")) ? null : reader.GetString(reader.GetOrdinal("CountryName"))
                            };
                            beneficiaries.Add(beneficiary);
                        }
                    }

                    totalCount = (int)command.Parameters["@TotalCount"].Value;
                }
            }
            return new PagedList<BeneficiaryReport>(beneficiaries, totalCount, page, sizePage); ;
        }

        public async Task<BeneficiaryDTO> ObtXId(Int64 id)
        {
            var beneficiary = _context.Beneficiaries
            .FromSqlRaw("EXEC GetBeneficiaryById @Id = {0}", id)
            .AsEnumerable()
            .FirstOrDefault();

            return beneficiary;
        }

        public async Task<PagedList<BeneficiaryDTO>> ObtAllXEmployeeId(Int64 employeeId, int page = 1, int sizePage = 10, string sorting = "Id")
        {
            var beneficiaries = new List<BeneficiaryDTO>();
            var query = $"EXEC GetBeneficiaryByEmployeeId @EmployeeId={employeeId}, @page={page}, @sizePage={sizePage}, @sorting='{sorting}'";

            var result = _context.Beneficiaries.FromSqlRaw(query).AsNoTracking();

            beneficiaries = await result.ToListAsync();

            var totalRecords = await _context.Beneficiaries.CountAsync();

            return new PagedList<BeneficiaryDTO>(beneficiaries, totalRecords, page, sizePage);
        }

        public async Task<PagedList<BeneficiaryReport>> ObtAllXEmployeeIdDAO(Int64 employeeId, int page = 1, int sizePage = 10, string sorting = "Id")
        {
            var connectionString = _configurationService.GetConnectionString();
            var beneficiaries = new List<BeneficiaryReport>();
            int totalCount = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetBeneficiaryByEmployeeId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@EmployeeId", employeeId));
                    command.Parameters.Add(new SqlParameter("@Page", page));
                    command.Parameters.Add(new SqlParameter("@SizePage", sizePage));
                    command.Parameters.Add(new SqlParameter("@Sorting", sorting));
                    command.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                    connection.Open();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var beneficiary = new BeneficiaryReport
                            {
                                ID = reader.GetInt64(reader.GetOrdinal("ID")),
                                Name = reader.GetString(reader.GetOrdinal("NAME")),
                                LastName = reader.GetString(reader.GetOrdinal("LASTNAME")),
                                BirthDay = reader.GetDateTime(reader.GetOrdinal("BIRTHDAY")),
                                CURP = reader.IsDBNull(reader.GetOrdinal("CURP")) ? null : reader.GetString(reader.GetOrdinal("CURP")),
                                SSN = reader.IsDBNull(reader.GetOrdinal("SSN")) ? null : reader.GetString(reader.GetOrdinal("SSN")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("PHONENUMBER")),
                                ParticipationPercentaje = reader.GetInt32(reader.GetOrdinal("PARTICIPATIONPERCENTAJE")),
                                EmployeeId = reader.GetInt64(reader.GetOrdinal("EmployeeId")),
                                EmployeeName = reader.IsDBNull(reader.GetOrdinal("EmployeeName")) ? null : reader.GetString(reader.GetOrdinal("EmployeeName")),
                                CountryId = reader.GetInt32(reader.GetOrdinal("CountryId")),
                                CountryName = reader.IsDBNull(reader.GetOrdinal("CountryName")) ? null : reader.GetString(reader.GetOrdinal("CountryName"))
                            };
                            beneficiaries.Add(beneficiary);
                        }
                    }

                    totalCount = (int)command.Parameters["@TotalCount"].Value;
                }
            }
            return new PagedList<BeneficiaryReport>(beneficiaries, totalCount, page, sizePage); ;
        }
    }
}
