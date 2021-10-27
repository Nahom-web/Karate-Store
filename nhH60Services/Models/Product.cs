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

        public async Task<List<Product>> GetAllProducts() {
            return await _context.Products.Include(x => x.ProdCat).OrderBy(x => x.Description).ToListAsync();
        }


        public async Task<Product> FindProduct(int id) {
            return await _context.Products.Include(p => p.ProdCat).Where(x => x.ProductId == id).FirstAsync(); ;
        }

        public async Task CreateProduct() {
            _context.Products.Add(this);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateProduct() {
            _context.Entry(this).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task DeleteProduct() {
            _context.Products.Remove(this);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProductsWithCategories() {
            return await _context.Products.Include(x => x.ProdCat).OrderBy(pc => pc.ProdCat.ProdCat).ThenBy(p => p.Description).Include(c => c.ProdCat).ToListAsync();
        }
    }
}