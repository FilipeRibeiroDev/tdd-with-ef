using Logic;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Metadata;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeeController(EmployeeContext context) => _context = context;

        [HttpGet]
        public ActionResult<Employee> GetEmployee(string name)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.name == name);
            return employee is null ? NotFound() : employee;
        }

        [HttpPost]
        public ActionResult AddEmployee(string name,int age, string photo)
        {
            _context.Employees.Add(new Employee(name, age, photo));
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public ActionResult UpdateEmployeeAge(string name, int age)
        {
            using var transaction = _context.Database.BeginTransaction();

            var employee = _context.Employees.FirstOrDefault(b => b.name == name);
            if (employee is null)
            {
                return NotFound();
            }

            employee.SetAge(age);
            _context.SaveChanges();

            transaction.Commit();
            return Ok();
        }
    }
}
