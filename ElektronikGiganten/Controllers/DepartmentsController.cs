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
    public class DepartmentsController : ControllerBase
    {
        private IDepartmentRepository _departmentRepository;
        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        // GET: api/Departments/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            if (!await _departmentRepository.entityExists(id))
                return NotFound();
            var product = await _departmentRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        //api/Departments
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _departmentRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var departmentDto = new List<DepartmentDto>();

            foreach (var department in departments)
            {
                departmentDto.Add(new DepartmentDto
                {
                    Id = department.Id,
                    Name = department.Name
                });
            }
            return Ok(departmentDto);
        }

        //api/Departments
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] Department departmentmployeeToCreate)
        {
            if (departmentmployeeToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _departmentRepository.Insert(departmentmployeeToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetDepartment", new { id = departmentmployeeToCreate.Id }, departmentmployeeToCreate);
        }


        //api/Departments/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department updateDepartment)
        {
            if (updateDepartment == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateDepartment.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _departmentRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _departmentRepository.Update(updateDepartment);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/Departments/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (!await _departmentRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _departmentRepository.Delete(id);
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
