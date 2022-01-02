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
    public class CreditCardsController : ControllerBase
    {
        private ICreditCardRepository _creditCardRepository;
        public CreditCardsController(ICreditCardRepository creditCardRepository)
        {
            _creditCardRepository = creditCardRepository;
        }

        // GET: api/CreditCards/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCreditCard(int id)
        {
            if (!await _creditCardRepository.entityExists(id))
                return NotFound();
            var product = await _creditCardRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        //api/CreditCards
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCreditCards()
        {
            var creditCards = await _creditCardRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var creditCardDto = new List<CreditCardDto>();

            foreach (var creditCard in creditCards)
            {
                creditCardDto.Add(new CreditCardDto
                {
                    Id = creditCard.Id,
                    CardNumer = creditCard.CardNumber,
                    CustomerId = creditCard.CustomerId
                });
            }
            return Ok(creditCardDto);
        }

        //api/CreditCards
        [HttpPost]
        public async Task<IActionResult> CreateCreditCard([FromBody] CreditCard creditCardToCreate)
        {
            if (creditCardToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _creditCardRepository.Insert(creditCardToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetCreditCard", new { id = creditCardToCreate.Id }, creditCardToCreate);
        }


        //api/CreditCards/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCreditCard(int id, [FromBody] CreditCard updateCreditCard)
        {
            if (updateCreditCard == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateCreditCard.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _creditCardRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _creditCardRepository.Update(updateCreditCard);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/CreditCards/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreditCard(int id)
        {
            if (!await _creditCardRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _creditCardRepository.Delete(id);
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
