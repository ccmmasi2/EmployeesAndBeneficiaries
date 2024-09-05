using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beneficiaries.Core.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COUNTRIES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COUNTRIES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EMPLOYEES",
                columns: table => new
                {
                    ID = table.Column<double>(type: "float", nullable: false),
                    EMPLOYEENUMBER = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LASTNAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BIRTHDAY = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CURP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SSN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PHONENUMBER = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EMPLOYEES_COUNTRIES_CountryId",
                        column: x => x.CountryId,
                        principalTable: "COUNTRIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BENEFICIARIES",
                columns: table => new
                {
                    ID = table.Column<double>(type: "float", nullable: false),
                    PARTICIPATIONPERCENTAJE = table.Column<float>(type: "real", nullable: false),
                    EmployeeId = table.Column<double>(type: "float", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LASTNAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BIRTHDAY = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CURP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SSN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PHONENUMBER = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BENEFICIARIES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BENEFICIARIES_COUNTRIES_CountryId",
                        column: x => x.CountryId,
                        principalTable: "COUNTRIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BENEFICIARIES_EMPLOYEES_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EMPLOYEES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BENEFICIARIES_CountryId",
                table: "BENEFICIARIES",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_BENEFICIARIES_EmployeeId",
                table: "BENEFICIARIES",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEES_CountryId",
                table: "EMPLOYEES",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BENEFICIARIES");

            migrationBuilder.DropTable(
                name: "EMPLOYEES");

            migrationBuilder.DropTable(
                name: "COUNTRIES");
        }
    }
}
