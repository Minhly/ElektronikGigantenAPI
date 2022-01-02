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
    public class ProductsController : ControllerBase
    {
        private IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/Products/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            if (!await _productRepository.entityExists(id))
                return NotFound();

            var product = await _productRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        //api/Products
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productsDto = new List<ProductDto>();

            foreach (var product in products)
            {
                productsDto.Add(new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    CategoryId = product.CategoryId,
                    Description = product.Description
                });
            }
            return Ok(productsDto);
        }

        //api/Products
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product productToCreate)
        {
            if (productToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _productRepository.Insert(productToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetProduct", new { id = productToCreate.Id }, productToCreate);
        }


        //api/Products/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updateProduct)
        {
            if (updateProduct == null)
            {
                ModelState.AddModelError("", "It is null!");
                return BadRequest(ModelState);
            }
            if (id != updateProduct.Id)
            {
                ModelState.AddModelError("errors", "The id in the model does not match the id in the url!");
                return BadRequest(ModelState);
            }
            if (!await _productRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            try
            {
                await _productRepository.Update(updateProduct);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/Products/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!await _productRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _productRepository.Delete(id);
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
