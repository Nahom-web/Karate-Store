using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace nhH60Services.Models {
    public partial class CartItem {

        [NotMapped]
        private readonly H60Assignment2DB_nhContext _context;

        public CartItem() {
            _context = new H60Assignment2DB_nhContext();
        }


        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }

        public virtual ShoppingCart Cart { get; set; }
        public virtual Product Product { get; set; }

        public async Task<List<CartItem>> GetAllCartItems() {
            return await _context.CartItems.ToListAsync();
        }

        public async Task<CartItem> FindItemById(int id) {
            return await _context.CartItems.Where(x => x.CartItemId == id).FirstAsync();
        }

        public async Task Create() {
            _context.CartItems.Add(this);
            await _context.SaveChangesAsync();
        }


        public async Task Update() {
            H60Assignment2DB_nhContext _db = new H60Assignment2DB_nhContext();
            _db.Entry(this).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }


        public async Task Delete() {
            _context.CartItems.Remove(this);
            await _context.SaveChangesAsync();
        }
    }
}
