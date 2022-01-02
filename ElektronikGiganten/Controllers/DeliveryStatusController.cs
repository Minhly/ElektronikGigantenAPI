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
    public class DeliveryStatusController : ControllerBase
    {
        private IDeliveryStatusRepository _deliveryStatusRepository;
        public DeliveryStatusController(IDeliveryStatusRepository deliveryStatusRepository)
        {
            _deliveryStatusRepository = deliveryStatusRepository;
        }

        // GET: api/DeliveryStatus/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeliveryStatus(int id)
        {
            if (!await _deliveryStatusRepository.entityExists(id))
                return NotFound();

            var deliveryStatus = await _deliveryStatusRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(deliveryStatus);
        }

        //api/DeliveryStatus
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetDeliveryStatuses()
        {
            var deliveryStatuses = await _deliveryStatusRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deliveryStatusDto = new List<DeliveryStatusDto>();

            foreach (var deliveryStatus in deliveryStatuses)
            {
                deliveryStatusDto.Add(new DeliveryStatusDto
                {
                    Id = deliveryStatus.Id,
                    DeliveryStatus = deliveryStatus.Deliverystatus1
                });
            }
            return Ok(deliveryStatusDto);
        }

        //api/DeliveryStatus
        [HttpPost]
        public async Task<IActionResult> CreateDeliveryStatus([FromBody] DeliveryStatus deliveryStatusToCreate)
        {
            if (deliveryStatusToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _deliveryStatusRepository.Insert(deliveryStatusToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetDeliveryStatus", new { id = deliveryStatusToCreate.Id }, deliveryStatusToCreate);
        }


        //api/DeliveryStatus/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeliveryStatus(int id, [FromBody] DeliveryStatus updateDeliveryStatus)
        {
            if (updateDeliveryStatus == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateDeliveryStatus.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _deliveryStatusRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _deliveryStatusRepository.Update(updateDeliveryStatus);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/DeliveryStatus/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryStatus(int id)
        {
            if (!await _deliveryStatusRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _deliveryStatusRepository.Delete(id);
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
