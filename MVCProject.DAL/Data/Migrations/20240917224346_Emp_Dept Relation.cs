using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCProject.DAL.Data.Migrations
{
    public partial class Emp_DeptRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "dept_Id",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_dept_Id",
                table: "Employees",
                column: "dept_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_dept_Id",
                table: "Employees",
                column: "dept_Id",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_dept_Id",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_dept_Id",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "dept_Id",
                table: "Employees");
        }
    }
}
