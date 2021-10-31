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
            ProductCategory prodCategory = new ProductCategory();

            try {
                var Categories = await prodCategory.GetAllCategories();
                return Categories;
            } catch (Exception e) {
                return NotFound(e.Message);
            }

        }

        // GET: api/ProductCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(int id) {
            ProductCategory prodCategory = new ProductCategory();

            try {
                var CategoryFound = await prodCategory.FindCategory(id);
                return CategoryFound;
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

        }

        // GET: api/ProductCategories/Products
        [HttpGet("Products")]
        public async Task<ActionResult<IEnumerable<Product>>> Products(int id) {
            ProductCategory prodCategory = new ProductCategory();

            try {
                var Products = await prodCategory.GetProductsForCategory(id);
                return Products;
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

        }


        // POST: api/ProductCategories
        [HttpPost]
        public async Task<ActionResult<ProductCategory>> PostProductCategory(ProductCategory productCategory) {

            try {
                await productCategory.Create();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

            return NoContent();

        }


        // PUT: api/ProductCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCategory(int id, ProductCategory productCategory) {
            if (productCategory.FindCategory(id) == null) {
                return NotFound();
            }

            try {
                await productCategory.Update();
            } catch (Exception e) {
                return BadRequest(e.Message);
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
            } catch (Exception e) {
                return BadRequest(e.Message);
            } 

            return NoContent();
        }

    }
}
