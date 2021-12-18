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
    public class OrdersController : ControllerBase {

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> Orders() {
            Order Order = new Order();

            try {
                var Orders = Order.ToDTO(await Order.GetAllOrders());
                return Orders;
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Orders(int id) {
            Order Order = new Order();

            try {
                var OrderFound = await Order.FindOrderById(id);
                return OrderFound;
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }


        // GET: api/Order/OrderDTO/5
        [HttpGet("OrderDTO/{id}")]
        public async Task<ActionResult<OrderDTO>> OrderDTO(int id) {
            Order Order = new Order();

            try {
                var OrderFound = await Order.FindOrderById(id);
                var OrderDTOobj = Order.ToSingleDTO(OrderFound);
                return OrderDTOobj;
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }

        // GET: api/Orders/
        [HttpGet("Date/{date}")]
        public async Task<ActionResult<List<OrderDTO>>> Date(string date) {
            Order Order = new Order();

            try {
                var OrderFound = Order.ToDTO(await Order.FindOrderByDate(date));
                return OrderFound;
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }

        // GET: api/Orders/Customers/
        [HttpGet("Customers/{id}")]
        public async Task<ActionResult<List<OrderDTO>>> Customers(int id) {
            Order Order = new Order();

            try {
                var OrderFound = Order.ToDTO(await Order.FindOrdersForCustomer(id));
                return OrderFound;
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order Order) {
            if (Order.FindOrderById(id) == null) {
                return NotFound();
            }

            try {
                await Order.Update();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        // PUT: api/Orders/5
        [HttpPut("FinalizedOrder/{id}")]
        public async Task<IActionResult> FinalizedOrder(int id, Order Order) {
            if (Order.FindOrderById(id) == null) {
                return NotFound();
            }

            try {
                await Order.UpdateFinalizedOrder(id);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order Order) {
            try {
                await Order.Create();
                return CreatedAtAction("Orders", new { id = Order.OrderId }, Order);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id) {
            var Order = await new Order().FindOrderById(id);

            if (Order == null) {
                return NotFound();
            }

            try {
                await Order.Delete();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

            return NoContent();
        }


    }
}
