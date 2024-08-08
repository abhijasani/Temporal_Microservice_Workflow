using Microsoft.AspNetCore.Mvc;
using Company.Services;
using Company.DTOs;

namespace Company.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController(EmployeeService employeeService) : ControllerBase
    {
        private readonly EmployeeService _employeeService = employeeService;

        // Create
        [HttpPost]
        public IActionResult AddEmployee([FromBody] EmployeeDTO employeeDTO, [FromQuery] Guid governmentDirectoryId)
        {
            if (employeeDTO == null)
            {
                return BadRequest("Employee data is null.");
            }

            var employeeID = _employeeService.AddEmployee(employeeDTO, governmentDirectoryId);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeID }, employeeDTO);
        }

        // Read
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employees = _employeeService.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // Update
        [HttpPut("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, [FromBody] EmployeeDTO updatedEmployee)
        {
            if (updatedEmployee == null)
            {
                return BadRequest("Updated employee data is null.");
            }

            var result = _employeeService.UpdateEmployee(id, updatedEmployee);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Delete
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var result = _employeeService.DeleteEmployee(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
