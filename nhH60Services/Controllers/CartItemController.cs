using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // PUT: api/CartItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartItem(int id, CartItem CartItem) {
            if (CartItem.FindItemById(id) == null) {
                return NotFound();
            }

            try {
                await CartItem.Update();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

            return NoContent();
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

        // DELETE: api/CartItem/5
        [HttpDelete("{id}")]
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
