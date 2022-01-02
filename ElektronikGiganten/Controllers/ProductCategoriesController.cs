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
    public class ProductCategoriesController : ControllerBase
    {
        private IProductCategoryRepository _productCategoryRepository;
        public ProductCategoriesController(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        // GET: api/ProductCategories/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductCategory(int id)
        {
            if (!await _productCategoryRepository.entityExists(id))
                return NotFound();

            var customer = await _productCategoryRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(customer);
        }

        //api/ProductCategories
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetProductCategories()
        {
            var productCategories = await _productCategoryRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productCategoryDto = new List<ProductCategoryDto>();

            foreach (var productCategory in productCategories)
            {
                productCategoryDto.Add(new ProductCategoryDto
                {
                    Id = productCategory.Id,
                    Name = productCategory.Name
                });
            }
            return Ok(productCategoryDto);
        }

        //api/ProductCategories
        [HttpPost]
        public async Task<IActionResult> CreateProductCategory([FromBody] ProductCategory produtCategoryToCreate)
        {
            if (produtCategoryToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _productCategoryRepository.Insert(produtCategoryToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetProductCategory", new { id = produtCategoryToCreate.Id }, produtCategoryToCreate);
        }


        //api/ProductCategory/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductCategory(int id, [FromBody] ProductCategory updateProductCategory)
        {
            if (updateProductCategory == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateProductCategory.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _productCategoryRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _productCategoryRepository.Update(updateProductCategory);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/ProductCategory/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            if (!await _productCategoryRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _productCategoryRepository.Delete(id);
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
