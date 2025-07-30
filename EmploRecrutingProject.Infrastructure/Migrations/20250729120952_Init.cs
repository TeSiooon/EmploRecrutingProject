using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmploRecrutingProject.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Teams",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Teams", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "VacationPackages",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                GrantedDays = table.Column<int>(type: "int", nullable: false),
                Year = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_VacationPackages", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Employees",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                SuperiorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                VacationPackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Employees", x => x.Id);
                table.ForeignKey(
                    name: "FK_Employees_Employees_SuperiorId",
                    column: x => x.SuperiorId,
                    principalTable: "Employees",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Employees_Teams_TeamId",
                    column: x => x.TeamId,
                    principalTable: "Teams",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Employees_VacationPackages_VacationPackageId",
                    column: x => x.VacationPackageId,
                    principalTable: "VacationPackages",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Vacations",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DateSince = table.Column<DateTime>(type: "datetime2", nullable: false),
                DateUntil = table.Column<DateTime>(type: "datetime2", nullable: false),
                NumberOfHours = table.Column<int>(type: "int", nullable: false),
                IsPartialVacation = table.Column<int>(type: "int", nullable: false),
                EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Vacations", x => x.Id);
                table.ForeignKey(
                    name: "FK_Vacations_Employees_EmployeeId",
                    column: x => x.EmployeeId,
                    principalTable: "Employees",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Employees_SuperiorId",
            table: "Employees",
            column: "SuperiorId");

        migrationBuilder.CreateIndex(
            name: "IX_Employees_TeamId",
            table: "Employees",
            column: "TeamId");

        migrationBuilder.CreateIndex(
            name: "IX_Employees_VacationPackageId",
            table: "Employees",
            column: "VacationPackageId");

        migrationBuilder.CreateIndex(
            name: "IX_Vacations_EmployeeId",
            table: "Vacations",
            column: "EmployeeId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Vacations");

        migrationBuilder.DropTable(
            name: "Employees");

        migrationBuilder.DropTable(
            name: "Teams");

        migrationBuilder.DropTable(
            name: "VacationPackages");
    }
}
