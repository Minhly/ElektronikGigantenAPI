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
    public class OrderDeliveriesController : ControllerBase
    {
        private IOrderDeliveryRepository _orderDeliveryRepository;
        public OrderDeliveriesController(IOrderDeliveryRepository orderDeliveryRepository)
        {
            _orderDeliveryRepository = orderDeliveryRepository;
        }

        // GET: api/OrderDeliveries/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDelivery(int id)
        {
            if (!await _orderDeliveryRepository.entityExists(id))
                return NotFound();
            var orderDelivery = await _orderDeliveryRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderDelivery);
        }

        //api/OrderDeliveries
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOrderDeliveries()
        {
            var orderDeliveries = await _orderDeliveryRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderDeliveryDto = new List<OrderDeliveryDto>();

            foreach (var o in orderDeliveries)
            {
                orderDeliveryDto.Add(new OrderDeliveryDto
                {
                    Id = o.Id,
                    OrderId = o.OrderId,
                    CustomerId = o.CustomerId,
                    DeliveryStatusId = o.DeliverystatusId,
                    Address = o.Address,
                    DateDelivered = o.Datedelivered,
                    Postal = o.Postal
                });
            }
            return Ok(orderDeliveryDto);
        }

        //api/OrderDeliveries
        [HttpPost]
        public async Task<IActionResult> CreateOrderDelivery([FromBody] OrderDelivery OrderDeliveryToCreate)
        {
            if (OrderDeliveryToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _orderDeliveryRepository.Insert(OrderDeliveryToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetOrderDelivery", new { id = OrderDeliveryToCreate.Id }, OrderDeliveryToCreate);
        }


        //api/OrderDeliveries/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderDelivery(int id, [FromBody] OrderDelivery updateOrderDelivery)
        {
            if (updateOrderDelivery == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateOrderDelivery.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _orderDeliveryRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _orderDeliveryRepository.Update(updateOrderDelivery);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/OrderDeliveries/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDelivery(int id)
        {
            if (!await _orderDeliveryRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _orderDeliveryRepository.Delete(id);
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
