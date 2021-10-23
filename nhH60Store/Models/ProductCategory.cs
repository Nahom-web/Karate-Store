using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace nhH60Store.Models {
    public partial class ProductCategory {

        [NotMapped]
        private readonly H60AssignmentDB_nhContext _context;

        public ProductCategory() {
            _context = new H60AssignmentDB_nhContext();
        }


        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        public string ProdCat { get; set; }

        public virtual ICollection<Product> Product { get; set; }


        private async Task<List<ProductCategory>> GetAllCategoriesDB() {
            return await _context.ProductCategory.Include(p => p.Product).OrderBy(pc => pc.ProdCat).ToListAsync();
        }

        public async Task<List<ProductCategory>> GetAllCategories() {
            return await GetAllCategoriesDB();
        }

        private async Task<List<Product>> GetProductsForCategoryDB(int id) {
            return await _context.Product
                            .Where(x => x.ProdCatId == id)
                            .Include(p => p.ProdCat)
                            .OrderBy(d => d.Description)
                            .ToListAsync();
        }

        public async Task<List<Product>> GetProductsForCategory(int id) {
            return await GetProductsForCategoryDB(id);
        }

        private async void CreateDB() {
            _context.ProductCategory.Add(this);
            await _context.SaveChangesAsync();
        }

        public void Create() {
            CreateDB();
        }

        private async void UpdateDB() {
            try {
                _context.Update(this);
                await _context.SaveChangesAsync();
            } catch {
                throw new Exception("Something went wrong when updating the category");
            }
        }

        public async Task<ProductCategory> FindCategory(int id) {
            return await _context.ProductCategory.Where(x => x.CategoryId == id).FirstOrDefaultAsync();
        }

        public void Update() {
            UpdateDB();
        }

        private async void DeleteDB(int id) {
            try {
                List<Product> categoryProducts = Task.Run(() => GetProductsForCategoryDB(id)).Result;

                if (categoryProducts != null) {
                    foreach (var x in categoryProducts) {
                        _context.Product.Remove(x);
                    }
                }

                var prodCat = await _context.ProductCategory
                                .Where(x => x.CategoryId == id)
                                .FirstOrDefaultAsync();

                _context.ProductCategory.Remove(prodCat);

                await _context.SaveChangesAsync();
            } catch {

                throw new Exception("Something went wrong when deleting the category.");

            }           
        }

        public void Delete(int id) {
            DeleteDB(id);
        }
    }
}