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
    public class StoresController : ControllerBase
    {
        private IStoreRepository _storeRepository;
        public StoresController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        // GET: api/Stores/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStore(int id)
        {
            if (!await _storeRepository.entityExists(id))
                return NotFound();

            var store = await _storeRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(store);
        }

        //api/Stores
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStores()
        {
            var stores = await _storeRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var storeDto = new List<StoreDto>();

            foreach (var store in stores)
            {
                storeDto.Add(new StoreDto
                {
                    Id = store.Id,
                    Name = store.Name,
                    Address = store.Address,
                    Postal = store.Postal
                });
            }
            return Ok(storeDto);
        }

        //api/Stores
        [HttpPost]
        public async Task<IActionResult> CreateStore([FromBody] Store storeToCreate)
        {
            if (storeToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _storeRepository.Insert(storeToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetStore", new { id = storeToCreate.Id }, storeToCreate);
        }


        //api/Stores/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStore(int id, [FromBody] Store updateStore)
        {
            if (updateStore == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateStore.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _storeRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _storeRepository.Update(updateStore);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/Stores/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            if (!await _storeRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _storeRepository.Delete(id);
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
