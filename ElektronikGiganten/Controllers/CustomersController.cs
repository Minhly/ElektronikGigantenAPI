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
    public class CustomersController : ControllerBase
    {
        private ICustomerRepository _customerRepos;
        public CustomersController(ICustomerRepository customerRepos)
        {
            _customerRepos = customerRepos;
        }

        // GET: api/Customers/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            if (!await _customerRepos.entityExists(id))
                return NotFound();

            var customer = await _customerRepos.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(customer);
        }

        //api/customers
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerRepos.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customersDto = new List<CustomerDto>();

            foreach (var customer in customers)
            {
                customersDto.Add(new CustomerDto
                {
                    Id = customer.Id,
                    FirstName = customer.Firstname,
                    LastName = customer.Lastname,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Postal = customer.Postal,
                    Password = customer.Password,
                    AddressLine = customer.Addressline
                });
            }
            return Ok(customersDto);
        }



        //api/customers
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customerToCreate)
        {
            if (customerToCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                await _customerRepos.Insert(customerToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetCustomer", new { id = customerToCreate.Id }, customerToCreate);
        }

        //api/customers
        [HttpPost("Login")]
        public async Task<IActionResult> CustomerLogin([FromBody] LoginInfo customerInfo)
        {

            if (customerInfo == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var customer = await _customerRepos.verifyLogin(customerInfo);
                if (customer != null)
                {
                    return Ok(customer);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

        }


        //api/customers/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer updateCustomer)
        {
            if (updateCustomer == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateCustomer.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _customerRepos.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _customerRepos.Update(updateCustomer);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/Customers/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (!await _customerRepos.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _customerRepos.Delete(id);
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
