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
    public class OrdersController : ControllerBase {

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Orders() {
            Order Order = new Order();

            try {
                var Orders = await Order.GetAllOrders();
                return Orders;
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }


        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Order(int id) {
            Order Order = new Order();

            try {
                var OrderFound = await Order.FindOrderById(id);
                return OrderFound;
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }

        // PUT: api/Order/5
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

        // POST: api/Order
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order Order) {
            try {
                await Order.Create();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        // DELETE: api/Order/5
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
