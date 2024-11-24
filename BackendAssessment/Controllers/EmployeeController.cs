using BackendAssessment.Data;
using BackendAssessment.DTOs;
using BackendAssessment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EmployeeController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EmployeeController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
    {
        // Validate that the department exists
        var department = await _context.Departments
                                        .FirstOrDefaultAsync(d => d.Name == employeeDto.Department);
        if (department == null)
        {
            //We could create a new department, based on the new employee Department
            return BadRequest("Department not found");
        }

        var employee = new Employee
        {
            FirstName = employeeDto.FirstName,
            LastName = employeeDto.LastName,
            HireDate = employeeDto.HireDate,
            Department = employeeDto.Department,
            Phone = employeeDto.Phone,
            Address = employeeDto.Address,
            DepartmentDetailsId = department.Id
        };

        _context.Employees.Add(employee);

        await _context.SaveChangesAsync();

        return StatusCode(200);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
    {
        return await _context.Employees.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployeeById(int id)
    {
        var employee = await _context.Employees.FindAsync(id);

        if (employee == null)
            return NotFound();

        return employee;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDto employeeDto)
    {
        var employee = await _context.Employees.FindAsync(id);

        if (employee == null)
            return NotFound();

        var department = await _context.Departments.FirstOrDefaultAsync(d => d.Name == employeeDto.Department);

        if (department == null)
            return BadRequest("Department not found");

        employee.FirstName = employeeDto.FirstName;
        employee.LastName = employeeDto.LastName;
        employee.HireDate = employeeDto.HireDate;
        employee.Department = employeeDto.Department;
        employee.Phone = employeeDto.Phone;
        employee.Address = employeeDto.Address;
        employee.DepartmentDetailsId = department.Id;

        _context.Entry(employee).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var employee = await _context.Employees.FindAsync(id);

        if (employee == null)
            return NotFound();

        _context.Employees.Remove(employee);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}