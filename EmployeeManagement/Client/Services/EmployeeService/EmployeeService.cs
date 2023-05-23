using EmployeeManagement.Client.Pages;
using EmployeeManagement.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EmployeeManagement.Client.Services.EmployeeService
{
	public class EmployeeService : IEmployeeService
	{
		private readonly HttpClient _httpClient;
		private readonly NavigationManager _navigationManager;

		public EmployeeService(HttpClient httpClient, NavigationManager navigationManager)
		{
			_httpClient = httpClient;
			_navigationManager = navigationManager;
		}

		public List<EmployeeManagement.Shared.Employee> Employees { get; set; } = new List<EmployeeManagement.Shared.Employee>();
		public List<Department> Departments { get; set; } = new List<Department>();

		public async Task GetDepartments()
		{
			var departments = await _httpClient.GetFromJsonAsync<List<Department>>("api/employee/GetDepartments");
			if (departments is not null)
			{
				Departments = departments;
			}
		}

		public async Task<EmployeeManagement.Shared.Employee> GetEmployeeById(int id)
		{
			var employee = await _httpClient.GetFromJsonAsync<EmployeeManagement.Shared.Employee>($"api/employee/{id}");
			return employee is not null ? employee : throw new Exception("Sorry! Did not Found the employee record.");
		}

		public async Task GetEmployees()
		{
			var employees = await _httpClient.GetFromJsonAsync<List<EmployeeManagement.Shared.Employee>>("api/employee");
			if (employees is not null)
			{
				Employees = employees;
			}
		}

		public async Task CreateEmployee(EmployeeManagement.Shared.Employee employee)
		{
			var request = await _httpClient.PostAsJsonAsync("api/employee", employee);
			var response = await request.Content.ReadFromJsonAsync<List<EmployeeManagement.Shared.Employee>>();
			if (response is not null)
			{
				Employees = response;
			}
			NavitgateToEmployees();
		}

		public async Task DeleteEmployee(int id)
		{
			var request = await _httpClient.DeleteAsync($"api/employee/{id}");
			NavitgateToEmployees();
		}

		public async Task UpdateEmployee(EmployeeManagement.Shared.Employee employee)
		{
			var request = await _httpClient.PutAsJsonAsync($"api/employee/{employee.EmployeeId}", employee);
			NavitgateToEmployees();
		}

		public async Task SearchEmployee(string searchText)
		{
			var employees = await _httpClient.GetFromJsonAsync<List<EmployeeManagement.Shared.Employee>>($"api/employee/SearchEmployee/{searchText}");
			if (employees is not null)
			{
				Employees = employees;
			}
		}

		private void NavitgateToEmployees()
		{

			_navigationManager.NavigateTo("/");
		}
	}
}
