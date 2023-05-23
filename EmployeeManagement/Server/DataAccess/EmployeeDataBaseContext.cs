using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Server.DataAccess
{
	public class EmployeeDataBaseContext : DbContext
	{
		public EmployeeDataBaseContext(DbContextOptions<EmployeeDataBaseContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			_ = modelBuilder.Entity<Tables.Department>().HasData(
				new Department() { DepartmentId = 1, DepartmentName = "HR" },
				new Department() { DepartmentId = 2, DepartmentName = "Finance" },
				new Department() { DepartmentId = 3, DepartmentName = "IT" },
				new Department() { DepartmentId = 4, DepartmentName = "Account" },
				new Department() { DepartmentId = 5, DepartmentName = "Payroll" }
			);
			_ = modelBuilder.Entity<Tables.Employee>().HasData(
				new Employee
				{
					EmployeeId = 1,
					FirstName = "A",
					LastName = "B",
					Email = "ab@email.com",
					DateOfBirth = new DateTime(1993, 03, 03),
					DepartmentId = 1
				},
				new Employee
				{
					EmployeeId = 2,
					FirstName = "X",
					LastName = "Y",
					Email = "xy@email.com",
					DateOfBirth = new DateTime(1993, 02, 04),
					DepartmentId = 2
				},
				new Employee
				{
					EmployeeId = 3,
					FirstName = "A",
					LastName = "S",
					Email = "as@email.com",
					DateOfBirth = new DateTime(1997, 10, 11),
					DepartmentId = 3
				}
			);
		}

		public DbSet<Tables.Department>? Department { get; set; }
		public DbSet<Tables.Employee>? Employee { get; set; }
	}
}
