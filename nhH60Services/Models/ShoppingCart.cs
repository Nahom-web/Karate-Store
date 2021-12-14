using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using nhH60Services.Dtos;


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


        public async Task<List<ShoppingCart>> AllShoppingCarts() {
            return await _context.ShoppingCarts.ToListAsync();
        }

        public ShoppingCartDTO ToDTO(ShoppingCart Cart) {
            return new ShoppingCartDTO(Cart);
        }

        public async Task<ShoppingCart> FindCartById(int id) {
            return await _context.ShoppingCarts.Where(x => x.CartId == id).Include(t => t.CartItems).ThenInclude(p => p.Product).FirstOrDefaultAsync();
        }

        public async Task<ShoppingCart> GetCartWithCustomerId(int id) {
            var customerdto = await _context.ShoppingCarts.Where(x => x.CustomerId == id).Include(t => t.CartItems).ThenInclude(p => p.Product).FirstOrDefaultAsync();
            return customerdto;
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

            List<CartItem> Items = await _context.CartItems.Where(c => c.CartId == this.CartId).ToListAsync();

            if (Items.Count != 0) {
                throw new Exception("Cannot delete your shopping cart because you have products in it.");
            }

            _context.ShoppingCarts.Remove(this);

            await _context.SaveChangesAsync();
        }
    }
}
