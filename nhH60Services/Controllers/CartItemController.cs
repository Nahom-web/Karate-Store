﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using nhH60Services.Models;

namespace nhH60Services.Controllers {
    [Route("api/CartItems")]
    [ApiController]
    public class CartItemController : ControllerBase {


        // GET: api/CartItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItem>>> CartItems() {
            CartItem CartItem = new CartItem();

            try {
                var CartItems = await CartItem.GetAllCartItems();
                return CartItems;
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }


        // GET: api/CartItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartItem>> CartItem(int id) {
            CartItem CartItem = new CartItem();

            try {
                var CartItemFound = await CartItem.FindItemById(id);
                return CartItemFound;
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }

        // POST: api/CartItem
        [HttpPost]
        public async Task<ActionResult<CartItem>> PostCartItem(CartItem CartItem) {
            try {
                await CartItem.Create();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        // PUT: api/CartItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartItem(int id, CartItem CartItem) {
            if (await CartItem.FindItemById(id) == null) {
                return NotFound();
            }

            try {
                await CartItem.Update();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        // DELETE: api/CartItem/RemoveCartItem/5
        [HttpDelete("RemoveCartItem/{id}")]
        public async Task<IActionResult> RemoveCartItem(int id) {
            var CartItem = await new CartItem().FindItemById(id);

            if (CartItem == null) {
                return NotFound();
            }

            try {
                await CartItem.Remove();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        // DELETE: api/CartItem/DeleteCartItem/5
        [HttpDelete("DeleteCartItem/{id}")]
        public async Task<IActionResult> DeleteCartItem(int id) {
            var CartItem = await new CartItem().FindItemById(id);

            if (CartItem == null) {
                return NotFound();
            }

            try {
                await CartItem.Delete();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

    }
}
