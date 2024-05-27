using EmployeeWebAPl.Models;
using EmployeeWebAPl.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebAPl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                return Ok(await _employeeRepository.GetEmployees());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)

        {
            try
            {
                var result = await _employeeRepository.GetEmployeeById(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }



        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }
                else
                {
                    var emp = await _employeeRepository.GetEmployeeByEmail(employee.Email);
                    if (emp != null)
                    {
                        ModelState.AddModelError("email", "email already in use !!");
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        var createdEmployee = await _employeeRepository.AddEmployee(employee);
                        return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.EmployeeId }, createdEmployee);
                    }
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new employee record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee emp)
        {
            try
            {
                if (id != emp.EmployeeId)
                {
                    return BadRequest("error in id !!!");
                }
                var updatedemp = await _employeeRepository.GetEmployeeById(id);
                if (updatedemp == null)
                {
                    return BadRequest($" employee with id: {id} not found !");
                }
                return await _employeeRepository.UpdateEmployee(emp);
            } catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error updating data !");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>>DeleteEmployee (int id )
        {
            try
            {
                var empdelete = await _employeeRepository.GetEmployeeById(id);
                if (empdelete == null)
                {
                    return BadRequest($"not found employee with id : {id}");
                }
                return await _employeeRepository.DeleteEmployee(empdelete.EmployeeId);
            }catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(String name ,Gender? gender)
        {
            try
            {
                var result = await _employeeRepository.Search(name, gender);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();

            }catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from the database");
            }
        }
    }
}
