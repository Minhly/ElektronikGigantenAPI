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
    public class OrderStatusController : ControllerBase
    {
        private IOrderStatusRepository _orderStatusRepository;
        public OrderStatusController(IOrderStatusRepository orderStatusRepository)
        {
            _orderStatusRepository = orderStatusRepository;
        }

        // GET: api/OrderStatus/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderStatus(int id)
        {
            if (!await _orderStatusRepository.entityExists(id))
                return NotFound();

            var orderStatus = await _orderStatusRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderStatus);
        }

        //api/OrderStatus
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOrderStatuses()
        {
            var orderStatuses = await _orderStatusRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderStatusDto = new List<OrderStatusDto>();

            foreach (var orderStatus in orderStatuses)
            {
                orderStatusDto.Add(new OrderStatusDto
                {
                    Id = orderStatus.Id,
                    OrderStatus = orderStatus.Orderstatus1
                });
            }
            return Ok(orderStatusDto);
        }

        //api/OrderStatus
        [HttpPost]
        public async Task<IActionResult> CreateOrderStatus([FromBody] OrderStatus orderStatusToCreate)
        {
            if (orderStatusToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _orderStatusRepository.Insert(orderStatusToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetOrderStatus", new { id = orderStatusToCreate.Id }, orderStatusToCreate);
        }


        //api/OrderStatus/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] OrderStatus updateOrderStatus)
        {
            if (updateOrderStatus == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateOrderStatus.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _orderStatusRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _orderStatusRepository.Update(updateOrderStatus);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/OrderStatus/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderStatus(int id)
        {
            if (!await _orderStatusRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _orderStatusRepository.Delete(id);
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
