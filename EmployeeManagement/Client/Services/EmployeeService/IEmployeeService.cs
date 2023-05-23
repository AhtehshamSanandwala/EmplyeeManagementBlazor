namespace EmployeeManagement.Client.Services.EmployeeService
{
	public interface IEmployeeService
	{
		List<Employee> Employees { get; set; }
		List<Department> Departments { get; set; }
		Task GetEmployees();
		Task GetDepartments();
		Task<Employee> GetEmployeeById(int id);
		Task CreateEmployee(Employee employee);
		Task UpdateEmployee(Employee employee);
		Task DeleteEmployee(int id);
		Task SearchEmployee(string searchText);
	}
}
