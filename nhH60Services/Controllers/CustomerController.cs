using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nhH60Services.Models;

namespace nhH60Services.Controllers {
    [Route("api/Customers")]
    [ApiController]
    public class CustomerController : ControllerBase {

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Customers(string? Name) {
            Customer customer = new Customer();

            try {
                if (Name != null) {
                    return await customer.FindCustomerByName(Name);
                }
                return await customer.GetAllCustomers(); ;
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }


        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Customer(int id) {
            Customer customer = new Customer();

            try {
                var CustomerFound = await customer.FindCustomer(id);
                return CustomerFound;
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer) {
            if (customer.FindCustomer(id) == null) {
                return NotFound();
            }

            try {
                await customer.Update();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer) {
            try {
                await customer.Create();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id) {
            var customer = await new Customer().FindCustomer(id);
            if (customer == null) {
                return NotFound();
            }

            try {
                await customer.Delete();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

            return NoContent();
        }


    }
}
