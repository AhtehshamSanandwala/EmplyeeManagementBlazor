using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Server.DataAccess.Tables
{
	public class Department
	{	
		[Key]
		public int DepartmentId { get; set; }
		public string? DepartmentName { get; set; }
	}
}
