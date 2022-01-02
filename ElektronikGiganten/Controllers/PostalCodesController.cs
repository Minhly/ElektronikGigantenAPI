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
    public class PostalCodesController : ControllerBase
    {
        private IPostalCodeRepository _postalCodeRepository;
        public PostalCodesController(IPostalCodeRepository postalCodeRepository)
        {
            _postalCodeRepository = postalCodeRepository;
        }

        // GET: api/PostalCodes/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostalCode(int id)
        {
            if (!await _postalCodeRepository.entityExists(id))
                return NotFound();
            var postalCode = await _postalCodeRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(postalCode);
        }

        //api/PostalCodes
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPostalCodes()
        {
            var postalCodes = await _postalCodeRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postalCodeDto = new List<PostalCodeDto>();

            foreach (var postalCode in postalCodes)
            {
                postalCodeDto.Add(new PostalCodeDto
                {
                    PostalCode = postalCode.Postalcodex,
                    City = postalCode.City
                });
            }
            return Ok(postalCodeDto);
        }

        //api/PostalCodes
        [HttpPost]
        public async Task<IActionResult> CreatePostalCode([FromBody] PostalCode postalCodeToCreate)
        {
            if (postalCodeToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _postalCodeRepository.Insert(postalCodeToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetPostalCode", new { id = postalCodeToCreate.Postalcodex }, postalCodeToCreate);
        }


        //api/PostalCodes/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePostalCode(int id, [FromBody] PostalCode updatePostalCode)
        {
            if (updatePostalCode == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updatePostalCode.Postalcodex)
            {
                return BadRequest(ModelState);
            }
            if (!await _postalCodeRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _postalCodeRepository.Update(updatePostalCode);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/PostalCode/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostalCode(int id)
        {
            if (!await _postalCodeRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _postalCodeRepository.Delete(id);
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
