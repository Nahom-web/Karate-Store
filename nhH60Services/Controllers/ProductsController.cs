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
    public class ProductsController : ControllerBase {
        private readonly H60Assignment2DB_nhContext _context;

        public ProductsController(H60Assignment2DB_nhContext context) {
            _context = context;
        }

        // GET: api/Products
        // Get All Products
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts() {
            Product product = new Product();
            return await product.GetAllProductsDB();
        }

        // GET: api/Products/5
        // Find Product
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) {
            Product product = new Product();
            return await product.FindProductDB(id);
        }

        // PUT: api/Products/5
        // Update product
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProduct(int id, Product product) {
            if (id != product.ProductId) {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!ProductExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();

        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // New product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product) {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id) {
            var product = await _context.Products.FindAsync(id);
            if (product == null) {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("ByCategory")]
        public async Task<ActionResult<List<Product>>> ByCategory() {
            Product product = new Product();
            return await product.GetAllProductsWithCategoriesDB();
        }

        private bool ProductExists(int id) {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
