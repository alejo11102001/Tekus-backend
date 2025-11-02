using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tekus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCountryFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Services_ServiceId",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_ServiceId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Countries");

            migrationBuilder.AddColumn<string>(
                name: "CustomFieldsJson",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ServiceCountries",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCountries", x => new { x.ServiceId, x.CountryId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceCountries");

            migrationBuilder.DropColumn(
                name: "CustomFieldsJson",
                table: "Providers");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Countries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_ServiceId",
                table: "Countries",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Services_ServiceId",
                table: "Countries",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }
    }
}
