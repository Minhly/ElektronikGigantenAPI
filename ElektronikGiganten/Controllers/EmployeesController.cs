using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElektronikGigantenLibrary.Models;
using ElektronikGiganten.Services;
using ElektronikGiganten.Dtos;

namespace ElektronikGiganten.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeRepository _employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // GET: api/Employees/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            if (!await _employeeRepository.entityExists(id))
                return NotFound();
            var product = await _employeeRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        //api/Employees
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeDto = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                employeeDto.Add(new EmployeeDto
                {
                    Id = employee.Id,
                    FirstName = employee.Firstname,
                    LastName = employee.Lastname,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    Postal = employee.Postal,
                    Address = employee.Address,
                    Password = employee.Password,
                    DepartmentId = employee.DepartmentId
                });
            }
            return Ok(employeeDto);
        }

        //api/Employees
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employeeToCreate)
        {
            if (employeeToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _employeeRepository.Insert(employeeToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetEmployee", new { id = employeeToCreate.Id }, employeeToCreate);
        }


        //api/Employees/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee updateEmployee)
        {
            if (updateEmployee == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateEmployee.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _employeeRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _employeeRepository.Update(updateEmployee);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/Employee/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (!await _employeeRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _employeeRepository.Delete(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
