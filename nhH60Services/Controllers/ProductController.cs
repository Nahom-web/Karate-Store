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
    public class ProductController : ControllerBase {

        // GET: api/Products
        // Get All Products
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts() {
            Product product = new Product();

            try {
                var AllProducts = await product.GetAllProducts();
                return AllProducts;
            } catch (InvalidOperationException) {
                return NotFound();
            }

        }

        [HttpGet("ByCategory")]
        public async Task<ActionResult<List<Product>>> ByCategory() {
            Product product = new Product();

            try {
                var Products = await product.GetAllProductsWithCategories();
                return Products;
            } catch (InvalidOperationException) {
                return NotFound();
            }

        }

        // GET: api/Products/5
        // Find Product
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) {
            Product product = new Product();

            try {
                var ProductFound = await product.FindProduct(id);
                return ProductFound;
            } catch (InvalidOperationException) {
                return NotFound();
            }

        }


        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // New product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product) {
            
            try {
                await product.CreateProduct();
                return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
            } catch (DbUpdateException) {
                return BadRequest();
            }            
        }

        // PUT: api/Products/5
        // Update product
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProduct(int id, Product product) {
            if (id != product.ProductId) {
                return BadRequest();
            }

            try {
                await product.UpdateProduct();
            } catch (DbUpdateException) {
                if (await product.FindProduct(id) == null) {
                    return NotFound();
                } else {
                    return BadRequest();
                }
            }

            return NoContent();
        }


        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id) {

            Product product = await new Product().FindProduct(id);

            if (product == null) {
                return NotFound();
            }

            try {
                await product.DeleteProduct();
            } catch (DbUpdateException) {
                if (await product.FindProduct(id) == null) {
                    return NotFound();
                } else {
                    return BadRequest();
                }
            }

            return NoContent();
        }

    }
}
