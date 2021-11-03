using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace nhH60Services.Models {
    public partial class Customer {

        [NotMapped]
        private readonly H60Assignment2DB_nhContext _context;

        public Customer() {
            _context = new H60Assignment2DB_nhContext();
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Province { get; set; }
        public string CreditCard { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public async Task<List<Customer>> GetAllCustomers() {
            return await _context.Customers.OrderBy(ln => ln.LastName).ToListAsync();
        }

        public async Task<Customer> FindCustomer(int id) {
            return await _context.Customers.Where(x => x.CustomerId == id).FirstOrDefaultAsync();
        }

        public async Task Create() {
            _context.Customers.Add(this);
            await _context.SaveChangesAsync();
        }

        public async Task Update() {
            H60Assignment2DB_nhContext _db = new H60Assignment2DB_nhContext();
            _db.Entry(this).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task Delete() {
            ShoppingCart cart = await _context.ShoppingCarts.Where(x => x.CustomerId == this.CustomerId).FirstOrDefaultAsync();

            List<Order> orders = await _context.Orders.Where(x => x.CustomerId == this.CustomerId).ToListAsync();

            if (cart != null || orders.Count != 0) {
                throw new Exception("Sorry, cannot delete account because you orders and/or items in your cart.");
            }

            _context.Customers.Remove(this);

            await _context.SaveChangesAsync();
        }
    }
}
