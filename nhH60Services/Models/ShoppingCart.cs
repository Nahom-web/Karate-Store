using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace nhH60Services.Models {
    public partial class ShoppingCart {


        [NotMapped]
        private readonly H60Assignment2DB_nhContext _context;

        public ShoppingCart() {
            CartItems = new HashSet<CartItem>();
            _context = new H60Assignment2DB_nhContext();
        }

        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }


        public async Task<List<ShoppingCart>> GetAllCarts() {
            return await _context.ShoppingCarts.ToListAsync();
        }

        public async Task<ShoppingCart> FindCartById(int id) {
            return await _context.ShoppingCarts.Where(x => x.CartId == id).FirstAsync();
        }

        public async Task Create() {
            _context.ShoppingCarts.Add(this);
            await _context.SaveChangesAsync();
        }


        public async Task Update() {
            H60Assignment2DB_nhContext _db = new H60Assignment2DB_nhContext();
            _db.Entry(this).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }


        public async Task Delete() {
            _context.ShoppingCarts.Remove(this);
            await _context.SaveChangesAsync();
        }
    }
}
