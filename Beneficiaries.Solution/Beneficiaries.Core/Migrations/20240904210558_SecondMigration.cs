using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beneficiaries.Core.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BENEFICIARIE_COUNTRIES_CountryId",
                table: "BENEFICIARIE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BENEFICIARIE",
                table: "BENEFICIARIE");

            migrationBuilder.RenameTable(
                name: "BENEFICIARIE",
                newName: "BENEFICIARIES");

            migrationBuilder.RenameIndex(
                name: "IX_BENEFICIARIE_CountryId",
                table: "BENEFICIARIES",
                newName: "IX_BENEFICIARIES_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BENEFICIARIES",
                table: "BENEFICIARIES",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BENEFICIARIES_COUNTRIES_CountryId",
                table: "BENEFICIARIES",
                column: "CountryId",
                principalTable: "COUNTRIES",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BENEFICIARIES_COUNTRIES_CountryId",
                table: "BENEFICIARIES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BENEFICIARIES",
                table: "BENEFICIARIES");

            migrationBuilder.RenameTable(
                name: "BENEFICIARIES",
                newName: "BENEFICIARIE");

            migrationBuilder.RenameIndex(
                name: "IX_BENEFICIARIES_CountryId",
                table: "BENEFICIARIE",
                newName: "IX_BENEFICIARIE_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BENEFICIARIE",
                table: "BENEFICIARIE",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BENEFICIARIE_COUNTRIES_CountryId",
                table: "BENEFICIARIE",
                column: "CountryId",
                principalTable: "COUNTRIES",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
