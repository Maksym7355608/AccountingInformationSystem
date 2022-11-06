using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingInformationSystem.Data.Migrations
{
    public partial class UpdateEmployees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_WorkShedules_WorkSheduleId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_WorkSheduleId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "WorkSheduleId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "WorkShedules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkShedules_EmployeeId",
                table: "WorkShedules",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkShedules_Employees_EmployeeId",
                table: "WorkShedules",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkShedules_Employees_EmployeeId",
                table: "WorkShedules");

            migrationBuilder.DropIndex(
                name: "IX_WorkShedules_EmployeeId",
                table: "WorkShedules");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "WorkShedules");

            migrationBuilder.AddColumn<int>(
                name: "WorkSheduleId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_WorkSheduleId",
                table: "Employees",
                column: "WorkSheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_WorkShedules_WorkSheduleId",
                table: "Employees",
                column: "WorkSheduleId",
                principalTable: "WorkShedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
