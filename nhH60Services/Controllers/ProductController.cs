﻿using System;
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
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts(string? ProductName) {    

            Product product = new Product();

            try {
                if (ProductName != null) {
                    return await product.FindProductByName(ProductName);
                } 
                return await product.GetAllProducts();
            } catch (Exception) {
                return NotFound();
            }

        }

        // GET: api/Products/ByCategory
        [HttpGet("ByCategory")]
        public async Task<ActionResult<List<Product>>> ByCategory() {
            Product product = new Product();

            try {
                var Products = await product.GetAllProductsWithCategories();
                return Products;
            } catch (Exception) {
                return NotFound();
            }

        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) {
            Product product = new Product();

            try {
                var ProductFound = await product.FindProductById(id);
                return ProductFound;
            } catch (Exception) {
                return NotFound();
            }

        }


        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product) {

            try {
                await product.Create();
            } catch (Exception) {
                return BadRequest();
            }

            return NoContent();
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product) {
            if (product.FindProductById(id) == null) {
                return NotFound();
            }

            try {
                await product.Update();
            } catch (Exception) {
                return BadRequest();
            }

            return NoContent();
        }


        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id) {
            Product product = await new Product().FindProductById(id);

            if (product == null) {
                return NotFound();
            }

            try {
                await product.Delete();
            } catch (Exception) {
                return BadRequest();
            }

            return NoContent();
        }

    }
}
