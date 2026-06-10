using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureApi.Data;
using SecureApi.Models;

namespace SecureApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeeController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/employee
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _context.Employees.ToListAsync();
        return Ok(employees);
    }

    // GET: api/employee/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var employee = await _context.Employees.FindAsync(id);

        if (employee == null)
            return NotFound(new { message = "Employee not found" });

        return Ok(employee);
    }

    // POST: api/employee
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Employee employee)
    {
        if (employee == null)
            return BadRequest(new { message = "Invalid employee data" });

        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
    }

    // PUT: api/employee/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Employee updatedEmployee)
    {
        var employee = await _context.Employees.FindAsync(id);

        if (employee == null)
            return NotFound(new { message = "Employee not found" });

        employee.Name = updatedEmployee.Name;
        employee.Role = updatedEmployee.Role;
        employee.Department = updatedEmployee.Department;

        await _context.SaveChangesAsync();

        return Ok(employee);
    }

    // DELETE: api/employee/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _context.Employees.FindAsync(id);

        if (employee == null)
            return NotFound(new { message = "Employee not found" });

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Employee deleted successfully" });
    }
}