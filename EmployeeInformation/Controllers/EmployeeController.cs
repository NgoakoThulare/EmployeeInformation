using EmployeeInformation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.User;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeInformation.Controllers
{
    /// <summary>
    /// Employee controller manages employee information
    /// </summary>
    [Authorize]
    [Route("Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        /// <summary>
        /// Employee service contains employee management operations.
        /// </summary>
        /// <param name="employeeService"></param>
        public EmployeeController(IEmployeeService employeeService) => _employeeService = employeeService;

        /// <summary>
        /// Returns a list of employees.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("List")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<Employee>))]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeService.GetAll();

            if (!employees.Any())
                return NotFound();

            return Ok(employees);
        }

        /// <summary>
        /// Gets individual employee.
        /// </summary>
        /// <param name="id">Employee id.</param>
        /// <returns></returns>
        [HttpGet("View/{id:int}", Name = "GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _employeeService.Get(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        /// <summary>
        /// Creates employee.
        /// </summary>
        /// <param name="employee">Employee object.</param>
        /// <returns></returns>
        [Authorize(Roles = Constants.Manager)]
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Employee))]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest(ModelState);

            if (!await _employeeService.Create(employee))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the employee {employee.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return CreatedAtRoute("GetEmployee", new { id = employee.Id }, employee);
        }

        /// <summary>
        /// Updates employee.
        /// </summary>
        /// <param name="id">Employee id.</param>
        /// <param name="employee">Employee object.</param>
        /// <returns></returns>
        [Authorize(Roles = Constants.Manager)]
        [HttpPatch("Edit/{id:int}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (employee == null || id != employee.Id)
                return BadRequest(ModelState);
            if (!await _employeeService.Update(employee))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the employee {employee.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Removes employee. 
        /// </summary>
        /// <param name="id">Employee id.</param>
        /// <returns></returns>
        [Authorize(Roles = Constants.Manager)]
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (!await _employeeService.Exists(id))
                return NotFound();

            if (!await _employeeService.Delete(id))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the employee Id {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return NoContent();
        }
    }
}
