using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

#nullable disable

namespace nhH60Services.Models {
    public partial class Product {

        [NotMapped]
        private readonly H60Assignment2DB_nhContext _context;

        public Product() {
            CartItems = new HashSet<CartItem>();
            OrderItems = new HashSet<OrderItem>();
            _context = new H60Assignment2DB_nhContext();
        }

        public int ProductId { get; set; }
        public int ProdCatId { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public int? Stock { get; set; }
        public decimal? BuyPrice { get; set; }
        public decimal? SellPrice { get; set; }

        public virtual ProductCategory ProdCat { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public async Task<List<Product>> GetAllProductsDB() {
            try {
                return await _context.Products.OrderBy(x => x.Description).ToListAsync();
            } catch {
                throw new Exception("Something went wrong with getting all the products");
            }
        }


        public async Task<Product> FindProductDB(int id) {
            var result = await _context.Products.Where(x => x.ProductId == id).FirstAsync();

            if (result == null) {
                throw new Exception("Cannot find product.");
            }

            return result;
        }


        public async void UpdatePricesDB(Product product) {
            try {
                _context.Update(product);
                await _context.SaveChangesAsync();
            } catch {
                throw new Exception("Something went wrong when updating the prices.");
            }
        }


        public async void UpdateStockDB(Product product) {
            try {
                _context.Update(product);
                await _context.SaveChangesAsync();
            } catch {
                throw new Exception("Something went wrong when updating the stock.");
            }
        }

        public async void DeleteProductDB(Product product) {
            try {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            } catch {
                throw new Exception("Something went wrong when deleting the product.");
            }
        }

        public async Task<List<Product>> GetAllProductsWithCategoriesDB() {
            try {
                return await _context.Products.OrderBy(pc => pc.ProdCat.ProdCat).ThenBy(p => p.Description).Include(c => c.ProdCat).ToListAsync();
            } catch {
                throw new ArgumentNullException("Something went wrong when getting all the products with their categories.");
            }
        }
    }
}