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

        private async Task UpdateProductStock(int quantity) {
            var product = await _context.Products.Where(x => x.ProductId == this.ProductId).FirstOrDefaultAsync();

            product.Stock -= quantity;

            H60Assignment2DB_nhContext _db = new H60Assignment2DB_nhContext();

            _context.Entry(product).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task Create() {

            var checkIfExists = await _context.CartItems.Where(x => x.ProductId == this.ProductId).FirstOrDefaultAsync();

            if(checkIfExists != null) {

                checkIfExists.Quantity += this.Quantity;

                H60Assignment2DB_nhContext _db = new H60Assignment2DB_nhContext();

                _context.Entry(checkIfExists).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                await UpdateProductStock(this.Quantity);

            } else {
                _context.CartItems.Add(this);

                await _context.SaveChangesAsync();

                await UpdateProductStock(this.Quantity);
            }


        }


        public async Task Update() {

            var item = await FindItemById(this.CartItemId);

            var initialQuantity = item.Quantity;

            item.Quantity = this.Quantity;      

            H60Assignment2DB_nhContext _db = new H60Assignment2DB_nhContext();

            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            await UpdateProductStock(this.Quantity - initialQuantity);
        }


        public async Task Remove() {
            await UpdateProductStock(-this.Quantity);
            _context.CartItems.Remove(this);
            await _context.SaveChangesAsync();
        }

        public async Task Delete() {
            _context.CartItems.Remove(this);
            await _context.SaveChangesAsync();
        }

    }
}
