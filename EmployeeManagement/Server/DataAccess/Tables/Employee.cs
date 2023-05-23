using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Server.DataAccess.Tables
{
	public class Employee
	{
		[Key]
		public int EmployeeId { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Email { get; set; }
		public DateTime DateOfBirth { get; set; }
		public int? DepartmentId { get; set; }
	}
}
