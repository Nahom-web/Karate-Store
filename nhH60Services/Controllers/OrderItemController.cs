using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nhH60Services.Dtos;
using nhH60Services.Models;

namespace nhH60Services.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase {

        // GET: api/OrderItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> OrderItems() {
            OrderItem OrderItem = new OrderItem();

            try {
                var OrderItems = OrderItem.ToDTO(await OrderItem.GetAllOrdersItems());
                return OrderItems;
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }


        // GET: api/OrderItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemDTO>> OrderItem(int id) {
            OrderItem OrderItem = new OrderItem();

            try {
                var OrderItemFound = OrderItem.ToSingleDTO(await OrderItem.FindOrderItemById(id));
                return OrderItemFound;
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }

        // PUT: api/OrderItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderItem(int id, OrderItem OrderItem) {
            if (OrderItem.FindOrderItemById(id) == null) {
                return NotFound();
            }

            try {
                await OrderItem.Update();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        // POST: api/OrderItem
        [HttpPost]
        public async Task<ActionResult<OrderItem>> PostOrderItem(OrderItem OrderItem) {
            try {
                await OrderItem.Create();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        // DELETE: api/OrderItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id) {
            var OrderItem = await new OrderItem().FindOrderItemById(id);

            if (OrderItem == null) {
                return NotFound();
            }

            try {
                await OrderItem.Delete();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

            return NoContent();
        }
    }
}
