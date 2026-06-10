using Microsoft.AspNetCore.Mvc;
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

        // Seed data (runs once)
        if (!_context.Employees.Any())
        {
            _context.Employees.AddRange(
                new Employee { Id = 1, Name = "John Doe", Role = "Developer", Department = "IT" },
                new Employee { Id = 2, Name = "Sara Smith", Role = "Manager", Department = "HR" }
            );
            _context.SaveChanges();
        }
    }

    // GET: api/employee
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Employees.ToList());
    }

    // GET: api/employee/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var emp = _context.Employees.Find(id);
        if (emp == null) return NotFound();
        return Ok(emp);
    }

    // POST: api/employee
    [HttpPost]
    public IActionResult Create(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
        return Ok(employee);
    }
}