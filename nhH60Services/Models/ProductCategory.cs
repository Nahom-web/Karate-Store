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


        public async Task<List<ProductCategory>> GetAllCategoriesDB() {
            return await _context.ProductCategories.Include(p => p.Products).OrderBy(pc => pc.ProdCat).ToListAsync();
        }


        public async Task<List<Product>> GetProductsForCategoryDB(int id) {
            return await _context.Products
                            .Where(x => x.ProdCatId == id)
                            .Include(p => p.ProdCat)
                            .OrderBy(d => d.Description)
                            .ToListAsync();
        }

        public async void CreateDB() {
            _context.ProductCategories.Add(this);
            await _context.SaveChangesAsync();
        }

        public async void UpdateDB() {
            try {
                _context.Update(this);
                await _context.SaveChangesAsync();
            } catch {
                throw new Exception("Something went wrong when updating the category");
            }
        }

        public async Task<ProductCategory> FindCategory(int id) {
            return await _context.ProductCategories.Where(x => x.CategoryId == id).FirstOrDefaultAsync();
        }

        public async void DeleteDB(int id) {
            try {
                List<Product> categoryProducts = Task.Run(() => GetProductsForCategoryDB(id)).Result;

                if (categoryProducts != null) {
                    foreach (var x in categoryProducts) {
                        _context.Products.Remove(x);
                    }
                }

                var prodCat = await _context.ProductCategories
                                .Where(x => x.CategoryId == id)
                                .FirstOrDefaultAsync();

                _context.ProductCategories.Remove(prodCat);

                await _context.SaveChangesAsync();
            } catch {

                throw new Exception("Something went wrong when deleting the category.");

            }
        }
    }
}