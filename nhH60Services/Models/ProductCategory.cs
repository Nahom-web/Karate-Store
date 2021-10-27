using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

#nullable disable

namespace nhH60Services.Models {
    public partial class ProductCategory {

        [NotMapped]
        private readonly H60Assignment2DB_nhContext _context;

        public ProductCategory() {
            Products = new HashSet<Product>();
            _context = new H60Assignment2DB_nhContext();
        }

        public int CategoryId { get; set; }
        public string ProdCat { get; set; }

        public virtual ICollection<Product> Products { get; set; }


        public async Task<List<ProductCategory>> GetAllCategories() {
            return await _context.ProductCategories.Include(p => p.Products).OrderBy(pc => pc.ProdCat).ToListAsync();
        }


        public async Task<List<Product>> GetProductsForCategory(int id) {
            return await _context.Products
                            .Where(x => x.ProdCatId == id)
                            .Include(p => p.ProdCat)
                            .OrderBy(d => d.Description)
                            .ToListAsync();
        }

        public async Task Create() {
            _context.ProductCategories.Add(this);
            await _context.SaveChangesAsync();
        }

        public async Task Update() {
            _context.Entry(this).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<ProductCategory> FindCategory(int id) {
            return await _context.ProductCategories.Include(p => p.Products).Where(x => x.CategoryId == id).FirstOrDefaultAsync();
        }

        public async Task Delete() {
            List<Product> categoryProducts = Task.Run(() => GetProductsForCategory(this.CategoryId)).Result;

            if (categoryProducts.Count != 0) {
                throw new Exception("Cannot delete this category becauce there are products in this category.");
            }

            _context.ProductCategories.Remove(this);

            await _context.SaveChangesAsync();
        }
    }
}