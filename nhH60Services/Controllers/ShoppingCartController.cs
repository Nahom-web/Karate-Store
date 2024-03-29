﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using nhH60Services.Models;
using nhH60Services.Dtos;

namespace nhH60Services.Controllers {
    [Route("api/ShoppingCarts")]
    [ApiController]
    public class ShoppingCartController : ControllerBase {


        // GET: api/ShoppingCarts
        [HttpGet]
        public async Task<ActionResult<List<ShoppingCart>>> ShoppingCarts() {
            ShoppingCart ShoppingCart = new ShoppingCart();

            try {
                return await ShoppingCart.AllShoppingCarts();
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }

        // GET: api/ShoppingCart
        [HttpGet("Customers/{CustomerId}")]
        public async Task<ActionResult<ShoppingCartDTO>> Customers(int CustomerId) {
            ShoppingCart ShoppingCart = new ShoppingCart();

            try {
                var cart = await ShoppingCart.GetCartWithCustomerId((int)CustomerId);
                if (cart != null) {
                    return ShoppingCart.ToDTO(cart);
                } else {
                    return NotFound();
                }
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }


        // GET: api/ShoppingCart/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCart>> ShoppingCarts(int id) {
            ShoppingCart ShoppingCart = new ShoppingCart();

            try {
                return await ShoppingCart.FindCartById(id);
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }

        // PUT: api/ShoppingCart/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingCart(int id, ShoppingCart ShoppingCart) {
            if (ShoppingCart.FindCartById(id) == null) {
                return NotFound();
            }

            try {
                await ShoppingCart.Update();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        // POST: api/ShoppingCart
        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> PostShoppingCart(ShoppingCart ShoppingCart) {
            try {
                await ShoppingCart.Create();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        // DELETE: api/ShoppingCart/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingCart(int id) {
            var ShoppingCart = await new ShoppingCart().FindCartById(id);
            if (ShoppingCart == null) {
                return NotFound();
            }

            try {
                await ShoppingCart.Delete();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

    }
}
