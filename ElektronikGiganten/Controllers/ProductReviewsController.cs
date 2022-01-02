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
    public class ProductReviewsController : ControllerBase
    {
        private IProductReviewRepository _productReviewRepository;
        public ProductReviewsController(IProductReviewRepository productReviewRepository)
        {
            _productReviewRepository = productReviewRepository;
        }

        // GET: api/ProductReviews/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductReview(int id)
        {
            if (!await _productReviewRepository.entityExists(id))
                return NotFound();

            var productReview = await _productReviewRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(productReview);
        }

        //api/ProductReviews
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetProductReviews()
        {
            var productReviews = await _productReviewRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productReviewsDto = new List<ProductReviewDto>();

            foreach (var productReview in productReviews)
            {
                productReviewsDto.Add(new ProductReviewDto
                {
                    Id = productReview.Id,
                    Review = productReview.Review,
                    Rating = productReview.Rating,
                    ProductId = productReview.ProductId
                });
            }
            return Ok(productReviewsDto);
        }

        //api/ProductReviews
        [HttpPost]
        public async Task<IActionResult> CreateProductReview([FromBody] ProductReview productReviewToCreate)
        {
            if (productReviewToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _productReviewRepository.Insert(productReviewToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetProductReview", new { id = productReviewToCreate.Id }, productReviewToCreate);
        }


        //api/ProductReviews/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductReview(int id, [FromBody] ProductReview updateProductReview)
        {
            if (updateProductReview == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateProductReview.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _productReviewRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _productReviewRepository.Update(updateProductReview);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/ProductReviews/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductReview(int id)
        {
            if (!await _productReviewRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _productReviewRepository.Delete(id);
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
