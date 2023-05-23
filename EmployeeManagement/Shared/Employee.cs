using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Shared
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
		public string? LastName { get; set; }
        [Required]
        [EmailAddress]
		public string? Email { get; set; }
        [Required]
		public DateTime DateOfBirth { get; set; }
        public Department? Department { get; set; }
        [Required]
        public int? DepartmentId { get; set; }
    }
}