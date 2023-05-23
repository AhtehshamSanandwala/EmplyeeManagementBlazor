using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Server.Migrations
{
	public partial class Initial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Department",
				columns: table => new
				{
					DepartmentId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					DepartmentName = table.Column<string>(type: "nvarchar(10)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Department", x => x.DepartmentId);
				});

			migrationBuilder.CreateTable(
				name: "Employee",
				columns: table => new
				{
					EmployeeId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					FirstName = table.Column<string>(type: "nvarchar(100)", nullable: true),
					LastName = table.Column<string>(type: "nvarchar(100)", nullable: true),
					Email = table.Column<string>(type: "nvarchar(100)", nullable: true),
					DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
					DepartmentId = table.Column<int>(type: "int", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Employee", x => x.EmployeeId);
					table.ForeignKey(
						name: "FK_Employee_Department_DepartmentId",
						column: x => x.DepartmentId,
						principalTable: "Department",
						principalColumn: "DepartmentId");
				});

			migrationBuilder.InsertData(
				table: "Department",
				columns: new[] { "DepartmentId", "DepartmentName" },
				values: new object[,]
				{
					{ 1, "HR" },
					{ 2, "Finance" },
					{ 3, "IT" },
					{ 4, "Account" },
					{ 5, "Payroll" }
				});

			migrationBuilder.InsertData(
				table: "Employee",
				columns: new[] { "EmployeeId", "DateOfBirth", "DepartmentId", "Email", "FirstName", "LastName" },
				values: new object[] { 1, new DateTime(1993, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "ab@email.com", "A", "B" });

			migrationBuilder.InsertData(
				table: "Employee",
				columns: new[] { "EmployeeId", "DateOfBirth", "DepartmentId", "Email", "FirstName", "LastName" },
				values: new object[] { 2, new DateTime(1993, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "xy@email.com", "X", "Y" });

			migrationBuilder.InsertData(
				table: "Employee",
				columns: new[] { "EmployeeId", "DateOfBirth", "DepartmentId", "Email", "FirstName", "LastName" },
				values: new object[] { 3, new DateTime(1997, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "as@email.com", "A", "S" });

			migrationBuilder.CreateIndex(
				name: "IX_Employee_DepartmentId",
				table: "Employee",
				column: "DepartmentId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{

			migrationBuilder.DropTable(
				name: "Employee");

			migrationBuilder.DropTable(
				name: "Department");
		}
	}
}
