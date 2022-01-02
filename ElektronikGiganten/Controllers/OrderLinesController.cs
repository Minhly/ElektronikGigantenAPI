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
    public class OrderLinesController : ControllerBase
    {
        private IOrderLineRepository _orderLineRepository;
        public OrderLinesController(IOrderLineRepository orderLineRepository)
        {
            _orderLineRepository = orderLineRepository;
        }

        // GET: api/OrderLines/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderLine(int id)
        {
            if (!await _orderLineRepository.entityExists(id))
                return NotFound();
            var orderLine = await _orderLineRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderLine);
        }

        //api/OrderLines
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOrderLines()
        {
            var orderLines = await _orderLineRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderLineDto = new List<OrderLineDto>();

            foreach (var o in orderLines)
            {
                orderLineDto.Add(new OrderLineDto
                {
                    Id = o.Id,
                    ProductId = o.ProductId,
                    Quantity = o.Quantity,
                    Price = o.Price,
                    OrderId = o.OrderId
                });
            }
            return Ok(orderLineDto);
        }

        //api/OrderLines
        [HttpPost]
        public async Task<IActionResult> CreateOrderLine([FromBody] OrderLine orderLineToCreate)
        {
            if (orderLineToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _orderLineRepository.Insert(orderLineToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetOrderLine", new { id = orderLineToCreate.Id }, orderLineToCreate);
        }


        //api/OrderLines/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderLine(int id, [FromBody] OrderLine updateOrderLine)
        {
            if (updateOrderLine == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateOrderLine.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _orderLineRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _orderLineRepository.Update(updateOrderLine);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/OrderLines/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderLine(int id)
        {
            if (!await _orderLineRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _orderLineRepository.Delete(id);
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
