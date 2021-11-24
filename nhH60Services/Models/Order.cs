using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace nhH60Services.Models {
    public partial class Order {

        [NotMapped]
        private readonly H60Assignment2DB_nhContext _context;

        public Order() {
            OrderItems = new HashSet<OrderItem>();
            _context = new H60Assignment2DB_nhContext();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateFulfilled { get; set; }
        public decimal? Total { get; set; }
        public decimal? Taxes { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }


        public async Task<List<Order>> GetAllOrders() {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> FindOrderById(int id) {
            return await _context.Orders.Where(x => x.OrderId == id).FirstAsync();
        }

        public async Task Create() {
            _context.Orders.Add(this);
            await _context.SaveChangesAsync();
        }

        public async Task Update() {
            H60Assignment2DB_nhContext _db = new H60Assignment2DB_nhContext();
            _db.Entry(this).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task Delete() {
            _context.Orders.Remove(this);
            await _context.SaveChangesAsync();
        }
    }
}
