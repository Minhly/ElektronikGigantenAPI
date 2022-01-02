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
    public class OrderSalesController : ControllerBase
    {
        private IOrderSaleRepository _orderSaleRepository;
        public OrderSalesController(IOrderSaleRepository orderSaleRepository)
        {
            _orderSaleRepository = orderSaleRepository;
        }

        // GET: api/OrderSales/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderSale(int id)
        {
            if (!await _orderSaleRepository.entityExists(id))
                return NotFound();
            var orderSale = await _orderSaleRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderSale);
        }

        //api/OrderSales
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOrderSales()
        {
            var orderSales = await _orderSaleRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderSaleDto = new List<OrderSaleDto>();

            foreach (var o in orderSales)
            {
                orderSaleDto.Add(new OrderSaleDto
                {
                    Id = o.Id,
                    OrderDate = o.Orderdate,
                    CustomerId = o.CustomerId,
                    OrderStatusId = o.OrderstatusId,
                    StoreId = o.StoreId
                });
            }
            return Ok(orderSaleDto);
        }

        //api/OrderSales
        [HttpGet("xxx")]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOrderSalesV2()
        {
            var orderSales = await _orderSaleRepository.GetAllSales();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(orderSales);
        }

        //api/OrderSales
        [HttpPost]
        public async Task<IActionResult> CreateOrderSale([FromBody] OrderSale orderSaleToCreate)
        {
            if (orderSaleToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _orderSaleRepository.Insert(orderSaleToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetOrderSale", new { id = orderSaleToCreate.Id }, orderSaleToCreate);
        }


        //api/OrderSales/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderSale(int id, [FromBody] OrderSale updateOrderSale)
        {
            if (updateOrderSale == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateOrderSale.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _orderSaleRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _orderSaleRepository.Update(updateOrderSale);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/OrderSales/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderSale(int id)
        {
            if (!await _orderSaleRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _orderSaleRepository.Delete(id);
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
