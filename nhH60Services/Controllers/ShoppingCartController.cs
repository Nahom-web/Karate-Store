using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using nhH60Services.Models;

namespace nhH60Services.Controllers {
    [Route("api/ShoppingCarts")]
    [ApiController]
    public class ShoppingCartController : ControllerBase {


        // GET: api/ShoppingCart
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCart>>> ShoppingCarts(int? CustomerId) {
            ShoppingCart ShoppingCart = new ShoppingCart();

            try {
                if (CustomerId != null) {
                    var Cart = await ShoppingCart.GetCartWithCustomerId((int)CustomerId);
                }
                var ShoppingCarts = await ShoppingCart.GetAllCarts();
                return ShoppingCarts;
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }


        // GET: api/ShoppingCart/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCart>> ShoppingCarts(int id) {
            ShoppingCart ShoppingCart = new ShoppingCart();

            try {
                var ShoppingCartFound = await ShoppingCart.FindCartById(id);
                return ShoppingCartFound;
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
