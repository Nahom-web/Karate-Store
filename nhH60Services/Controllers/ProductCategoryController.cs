using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nhH60Services.Models;

namespace nhH60Services.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase {


        // GET: api/ProductCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategories() {
            ProductCategory prodCategory = new();

            try {
                var Categories = await prodCategory.GetAllCategories();
                return Categories;
            } catch (InvalidOperationException) {
                return NotFound();
            }

        }

        // GET: api/ProductCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(int id) {
            ProductCategory prodCategory = new();

            try {
                var CategoryFound = await prodCategory.FindCategory(id);
                return CategoryFound;
            } catch (InvalidOperationException) {
                return NotFound();
            }
        }


        [HttpGet("Products")]
        public async Task<ActionResult<IEnumerable<Product>>> Products(int id) {
            ProductCategory prodCategory = new();

            try {
                var Products = await prodCategory.GetProductsForCategory(id);
                return Products;
            } catch (InvalidOperationException) {
                return NotFound();
            }

        }

        // PUT: api/ProductCategories/5
        // Update category
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCategory(int id, ProductCategory productCategory) {
            if (id != productCategory.CategoryId) {
                return BadRequest();
            }            

            try {
                await productCategory.Update();
            } catch (DbUpdateConcurrencyException) {
                if (productCategory.FindCategory(id) != null) {
                    return NotFound();
                } else {
                    return BadRequest();
                }
            }

            return NoContent();
        }

        // POST: api/ProductCategories
        // Create Category
        [HttpPost]
        public async Task<ActionResult<ProductCategory>> PostProductCategory(ProductCategory productCategory) {
            
            try {
                await productCategory.Create();
            } catch (DbUpdateException) {
                if (productCategory.FindCategory(productCategory.CategoryId) != null) {
                    return NotFound();
                } else {
                    return BadRequest();
                }
            }

            return NoContent();

        }

        // DELETE: api/ProductCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategory(int id) {
            var productCategory = await new ProductCategory().FindCategory(id);
            if (productCategory == null) {
                return NotFound();
            }

            try {
                await productCategory.Delete();
            } catch (DbUpdateException) {
                return BadRequest();
            }

            return NoContent();
        }

    }
}
