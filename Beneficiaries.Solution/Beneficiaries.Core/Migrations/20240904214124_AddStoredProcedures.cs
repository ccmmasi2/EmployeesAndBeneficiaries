using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beneficiaries.Core.Migrations
{
    public partial class AddStoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetAllBeneficiaries
                    @Page INT,
                    @SizePage INT,
                    @Sorting NVARCHAR(50),
                    @TotalCount INT OUTPUT
                AS
                BEGIN
                    DECLARE @Skip INT = (@Page - 1) * @SizePage;
                    DECLARE @Sql NVARCHAR(MAX);

                    SET @Sql = '
                        SELECT 
                            b.ID, 
                            b.NAME, 
                            b.LASTNAME, 
                            b.BIRTHDAY, 
                            b.CURP, 
                            b.SSN, 
                            b.PHONENUMBER, 
                            b.CountryId, 
                            b.PARTICIPATIONPERCENTAJE 
                        FROM BENEFICIARIES b
                        INNER JOIN COUNTRIES c ON b.CountryId = c.ID
                        ORDER BY ' + CAST(@Sorting AS NVARCHAR) + ' 
                        OFFSET ' + CAST(@Skip AS NVARCHAR) + ' ROWS
                        FETCH NEXT ' + CAST(@SizePage AS NVARCHAR) + ' ROWS ONLY';

                    EXEC sp_executesql @Sql;

                    SELECT @TotalCount = COUNT(e.ID) FROM EMPLOYEES e; 
                END;
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetAllEmployees
                    @Page INT,
                    @SizePage INT,
                    @Sorting NVARCHAR(50),
                    @TotalCount INT OUTPUT
                AS
                BEGIN
                    DECLARE @Skip INT = (@Page - 1) * @SizePage;
                    DECLARE @Sql NVARCHAR(MAX);

                    SET @Sql = '
                        SELECT
                            e.ID, 
                            e.NAME, 
                            e.LASTNAME, 
                            e.BIRTHDAY, 
                            e.CURP, 
                            e.SSN, 
                            e.PHONENUMBER, 
                            e.CountryId, 
                            e.EMPLOYEENUMBER,
                            c.NAME CountryName
                        FROM EMPLOYEES e
                        INNER JOIN COUNTRIES c ON e.CountryId = c.ID
                        ORDER BY ' + CAST(@Sorting AS NVARCHAR) + ' 
                        OFFSET ' + CAST(@Skip AS NVARCHAR) + ' ROWS
                        FETCH NEXT ' + CAST(@SizePage AS NVARCHAR) + ' ROWS ONLY';
        
                    EXEC sp_executesql @Sql;

                    SELECT @TotalCount = COUNT(e.ID) FROM EMPLOYEES e; 
                END;
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE DeleteBeneficiary
                    @Id BIGINT
                AS
                BEGIN
                    DELETE FROM BENEFICIARIES
                    WHERE ID = @Id;
                END;
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE DeleteEmployee
                    @Id BIGINT
                AS
                BEGIN
                    DELETE FROM EMPLOYEES
                    WHERE ID = @Id;
                END;
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE InsertBeneficiary
                    @Name NVARCHAR(50),
                    @LastName NVARCHAR(50),
                    @BirthDay DATE,
                    @CURP NVARCHAR(50),
                    @SSN NVARCHAR(50),
                    @PhoneNumber NVARCHAR(50),
                    @CountryId INT,
                    @ParticipationPercentaje FLOAT
                AS
                BEGIN
                    INSERT INTO BENEFICIARIES (NAME, LASTNAME, BIRTHDAY, CURP, SSN, PHONENUMBER, CountryId, PARTICIPATIONPERCENTAJE)
                    VALUES (@Name, @LastName, @BirthDay, @CURP, @SSN, @PhoneNumber, @CountryId, @ParticipationPercentaje);
                    SELECT SCOPE_IDENTITY() AS NewBeneficiaryId;
                END;
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE InsertEmployee
                    @Name NVARCHAR(50),
                    @LastName NVARCHAR(50),
                    @BirthDay DATE,
                    @CURP NVARCHAR(50),
                    @SSN NVARCHAR(50),
                    @PhoneNumber NVARCHAR(50),
                    @CountryId INT,
                    @EmployeeNumber BIGINT
                AS
                BEGIN
                    INSERT INTO EMPLOYEES (NAME, LASTNAME, BIRTHDAY, CURP, SSN, PHONENUMBER, CountryId, EMPLOYEENUMBER)
                    VALUES (@Name, @LastName, @BirthDay, @CURP, @SSN, @PhoneNumber, @CountryId, @EmployeeNumber);
                    SELECT SCOPE_IDENTITY() AS NewEmployeeId;
                END;
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE UpdateBeneficiary
                    @Id BIGINT,
                    @Name NVARCHAR(50),
                    @LastName NVARCHAR(50),
                    @BirthDay DATE,
                    @CURP NVARCHAR(50),
                    @SSN NVARCHAR(50),
                    @PhoneNumber NVARCHAR(50),
                    @CountryId INT,
                    @ParticipationPercentaje FLOAT
                AS
                BEGIN
                    UPDATE BENEFICIARIES
                    SET 
                        NAME = @Name,
                        LASTNAME = @LastName,
                        BIRTHDAY = @BirthDay,
                        CURP = @CURP,
                        SSN = @SSN,
                        PHONENUMBER = @PhoneNumber,
                        CountryId = @CountryId,
                        PARTICIPATIONPERCENTAJE = @ParticipationPercentaje
                    WHERE ID = @Id;
                END;
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE UpdateEmployee
                    @Id BIGINT,
                    @Name NVARCHAR(50),
                    @LastName NVARCHAR(50),
                    @BirthDay DATE,
                    @CURP NVARCHAR(50),
                    @SSN NVARCHAR(50),
                    @PhoneNumber NVARCHAR(50),
                    @CountryId INT,
                    @EmployeeNumber BIGINT
                AS
                BEGIN
                    UPDATE EMPLOYEES
                    SET 
                        NAME = @Name,
                        LASTNAME = @LastName,
                        BIRTHDAY = @BirthDay,
                        CURP = @CURP,
                        SSN = @SSN,
                        PHONENUMBER = @PhoneNumber,
                        CountryId = @CountryId,
                        EMPLOYEENUMBER = @EmployeeNumber
                    WHERE ID = @Id;
                END;
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetBeneficiaryById
                    @Id BIGINT
                AS
                BEGIN
                    SELECT 
                        b.ID, 
                        b.NAME, 
                        b.LASTNAME, 
                        b.BIRTHDAY, 
                        b.CURP, 
                        b.SSN, 
                        b.PHONENUMBER, 
                        b.CountryId, 
                        b.PARTICIPATIONPERCENTAJE
                    FROM BENEFICIARIES b
                    WHERE b.ID = @Id;
                END; 
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetEmployeeById
                    @Id BIGINT
                AS
                BEGIN
                    SELECT 
                        e.ID, 
                        e.NAME, 
                        e.LASTNAME, 
                        e.BIRTHDAY, 
                        e.CURP, 
                        e.SSN, 
                        e.PHONENUMBER, 
                        e.CountryId, 
                        e.EMPLOYEENUMBER
                    FROM EMPLOYEES e
                    WHERE e.ID = @Id;
                END;
            ");

            migrationBuilder.Sql(@"
	            CREATE PROCEDURE GetBeneficiariesByEmployeeId
		            @EmployeeId BIGINT,
                    @Page INT,
                    @SizePage INT,
                    @Sorting NVARCHAR(50),
                    @TotalCount INT OUTPUT
	            AS
	            BEGIN  
                    DECLARE @Skip INT = (@Page - 1) * @SizePage;
                    DECLARE @Sql NVARCHAR(MAX);

                    SET @Sql = '
		                SELECT 
			                b.ID, 
			                b.EmployeeId,
			                b.NAME, 
			                b.LASTNAME, 
			                b.BIRTHDAY, 
			                b.CURP, 
			                b.SSN, 
			                b.PHONENUMBER, 
			                b.CountryId, 
			                b.PARTICIPATIONPERCENTAJE 
		                FROM BENEFICIARIES b
		                INNER JOIN COUNTRIES c ON b.CountryId = c.ID
		                WHERE b.EmployeeId = @EmployeeId
                        ORDER BY ' + CAST(@Sorting AS NVARCHAR) + ' 
                        OFFSET ' + CAST(@Skip AS NVARCHAR) + ' ROWS
                        FETCH NEXT ' + CAST(@SizePage AS NVARCHAR) + ' ROWS ONLY';

                    EXEC sp_executesql @Sql;

                    SELECT @TotalCount = COUNT(e.ID) FROM EMPLOYEES e; 
	            END; 
            "); 

            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetAllCountries
                AS
                BEGIN
                    SELECT 
                        b.ID, 
                        b.NAME
                    FROM COUNTRIES b
                    ORDER BY NAME;
                END;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllBeneficiaries");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllEmployees");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteBeneficiary");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteEmployee");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS InsertBeneficiary");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS InsertEmployee");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateBeneficiary");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateEmployee");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetBeneficiariesByEmployeeId");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllCountries");
        }

    }
}
