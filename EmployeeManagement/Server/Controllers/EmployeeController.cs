using EmployeeManagement.Server.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Immutable;
using System.Linq;

namespace EmployeeManagement.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly EmployeeDataBaseContext _context;

		public EmployeeController(EmployeeDataBaseContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<List<Employee>>> GetEmployees()
		{
			try
			{
				var getEmployeesFromDb = await _context.Employee!.ToListAsync();
				var getDepartmentsFromDb = await _context.Department!.ToListAsync();
				var departments = getDepartmentsFromDb.Select(item => new Department() { DepartmentId = item.DepartmentId, DepartmentName = item.DepartmentName }).ToList();
				var employees = getEmployeesFromDb.Select(
					item => new Employee()
					{
						EmployeeId = item.EmployeeId,
						FirstName = item.FirstName,
						LastName = item.LastName,
						Email = item.Email,
						DateOfBirth = item.DateOfBirth,
						Department = departments.FirstOrDefault(d => d.DepartmentId == item.DepartmentId),
						DepartmentId = item.DepartmentId,
					}
				).ToList();

				return Ok(employees);
			}
			catch (Exception)
			{
				return BadRequest("Internal Server Error.!");
			}
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Employee>> GetEmployeeById(int id)
		{
			try
			{
				var getEmployeeFromDb = await _context.Employee!.FirstOrDefaultAsync(e => e.EmployeeId == id);
				var employee = new Employee();

				switch (getEmployeeFromDb)
				{
					case null:
						return NotFound("Sorry! Did not Found the employee record.");
					default:
						{
							var getEmployeeDepartMent = await _context.Department!.FirstOrDefaultAsync(d => d.DepartmentId == getEmployeeFromDb!.DepartmentId);
							var department = new Department()
							{
								DepartmentId = getEmployeeDepartMent!.DepartmentId,
								DepartmentName = getEmployeeDepartMent!.DepartmentName,
							};
							employee.EmployeeId = getEmployeeFromDb.EmployeeId;
							employee.FirstName = getEmployeeFromDb.FirstName;
							employee.LastName = getEmployeeFromDb.LastName;
							employee.Email = getEmployeeFromDb.Email;
							employee.DateOfBirth = getEmployeeFromDb.DateOfBirth;
							employee.Department = department;
							employee.DepartmentId = getEmployeeFromDb!.DepartmentId;
							return Ok(employee);
						}
				}
			}
			catch (Exception)
			{
				return BadRequest("Internal Server Error.!");
			}
		}

		[HttpGet("GetDepartments")]
		public async Task<ActionResult<List<Department>>> GetDepartments()
		{
			try
			{
				var getDepartmentsFromDb = await _context.Department!.ToListAsync();
				var departments = getDepartmentsFromDb.Select(item => new Department() { DepartmentId = item.DepartmentId, DepartmentName = item.DepartmentName }).ToList();
				return Ok(departments);
			}
			catch (Exception)
			{
				return BadRequest("Internal Server Error.!");
			}
		}

		[HttpPost]
		public async Task<ActionResult<List<Employee>>> CreateEmployee(Employee employee)
		{
			try
			{
				var employeeDb = new DataAccess.Tables.Employee
				{
					FirstName = employee.FirstName,
					LastName = employee.LastName,
					Email = employee.Email,
					DateOfBirth = employee.DateOfBirth,
					DepartmentId = employee.DepartmentId
				};
				_ = _context.Employee!.Add(employeeDb);
				_ = await _context.SaveChangesAsync();
				return RedirectToAction("GetEmployees");
			}
			catch (Exception)
			{
				return BadRequest("Internal Server Error.!");
			}
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee employee, int id)
		{
			try
			{
				var getEmployeeFromDb = await _context.Employee!.FirstOrDefaultAsync(e => e.EmployeeId == id);
				switch (getEmployeeFromDb)
				{
					case null:
						return NotFound("Sorry! Did not Found the employee record.");
					default:
						getEmployeeFromDb.FirstName = employee.FirstName;
						getEmployeeFromDb.LastName = employee.LastName;
						getEmployeeFromDb.Email = employee.Email;
						getEmployeeFromDb.DateOfBirth = employee.DateOfBirth;
						getEmployeeFromDb.DepartmentId = employee.DepartmentId;
						_ = await _context.SaveChangesAsync();
						return RedirectToAction("GetEmployees");
				}
			}
			catch (Exception)
			{
				return BadRequest("Internal Server Error.!");
			}
		}
		[HttpDelete("{id}")]
		public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
		{
			try
			{
				var getEmployeeFromDb = await _context.Employee!.FirstOrDefaultAsync(e => e.EmployeeId == id);
				switch (getEmployeeFromDb)
				{
					case null:
						return NotFound("Sorry! Did not Found the employee record.");
					default:
						_ = _context.Employee!.Remove(getEmployeeFromDb);
						_ = await _context.SaveChangesAsync();
						return RedirectToAction("GetEmployees");
				}
			}
			catch (Exception)
			{
				return BadRequest("Internal Server Error.!");
			}
		}

		[HttpGet("SearchEmployee/{searchText}")]
		public async Task<ActionResult<List<Employee>>> SearchEmployee(string searchText)
		{
			try
			{
				DataAccess.Tables.Department? getSearchDepartmentsFromDb;
				List<DataAccess.Tables.Employee> getEmployeesFromDb;
				if (!string.IsNullOrEmpty(searchText))
				{
					getSearchDepartmentsFromDb = await _context.Department!.Where(s => s.DepartmentName!.Contains(searchText)).FirstOrDefaultAsync();
					if (getSearchDepartmentsFromDb is not null)
					{
						getEmployeesFromDb = await _context.Employee!
					   .Where(s => s.DepartmentId == getSearchDepartmentsFromDb!.DepartmentId)
					   .ToListAsync();
					}
					else
					{
						getEmployeesFromDb = await _context.Employee!
								.Where(s => s.FirstName!.Contains(searchText) || s.LastName!.Contains(searchText) || s.Email!.Contains(searchText))
								.ToListAsync();
					}
				}
				else
				{
					getEmployeesFromDb = await _context.Employee!.ToListAsync();
				}
				var getDepartmentsFromDb = await _context.Department!.ToListAsync();
				var departments = getDepartmentsFromDb.Select(item => new Department() { DepartmentId = item.DepartmentId, DepartmentName = item.DepartmentName }).ToList();
				var employees = getEmployeesFromDb.Select(
					item => new Employee()
					{
						EmployeeId = item.EmployeeId,
						FirstName = item.FirstName,
						LastName = item.LastName,
						Email = item.Email,
						DateOfBirth = item.DateOfBirth,
						Department = departments.FirstOrDefault(d => d.DepartmentId == item.DepartmentId),
						DepartmentId = item.DepartmentId,
					}
				).ToList();

				return Ok(employees);
			}
			catch (Exception)
			{
				return BadRequest("Internal Server Error.!");
			}
		}

	}
}
