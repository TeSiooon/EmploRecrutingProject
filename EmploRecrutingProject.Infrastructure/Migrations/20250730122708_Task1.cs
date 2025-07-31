using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmploRecrutingProject.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Task1 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "EmployeeHierarchies",
            columns: table => new
            {
                EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                SuperiorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RelationLevel = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EmployeeHierarchies", x => new { x.EmployeeId, x.SuperiorId });
                table.ForeignKey(
                    name: "FK_EmployeeHierarchies_Employees_EmployeeId",
                    column: x => x.EmployeeId,
                    principalTable: "Employees",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_EmployeeHierarchies_Employees_SuperiorId",
                    column: x => x.SuperiorId,
                    principalTable: "Employees",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_EmployeeHierarchies_SuperiorId",
            table: "EmployeeHierarchies",
            column: "SuperiorId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "EmployeeHierarchies");
    }
}
