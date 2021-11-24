using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace nhH60Services.Models {
    public partial class OrderItem {

        [NotMapped]
        private readonly H60Assignment2DB_nhContext _context;

        public OrderItem() {
            _context = new H60Assignment2DB_nhContext();
        }

        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        public async Task<List<OrderItem>> GetAllOrdersItems() {
            return await _context.OrderItems.ToListAsync();
        }

        public async Task<OrderItem> FindOrderItemById(int id) {
            return await _context.OrderItems.Where(x => x.OrderItemId == id).FirstAsync();
        }

        public async Task Create() {
            _context.OrderItems.Add(this);
            await _context.SaveChangesAsync();
        }

        public async Task Update() {
            H60Assignment2DB_nhContext _db = new H60Assignment2DB_nhContext();
            _db.Entry(this).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task Delete() {
            _context.OrderItems.Remove(this);
            await _context.SaveChangesAsync();
        }
    }
}
