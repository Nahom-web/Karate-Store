using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace nhH60Store.Models {
    public partial class Product {

        [NotMapped]
        private readonly H60AssignmentDB_nhContext _context;

        public Product() {
            _context = new H60AssignmentDB_nhContext();
        }

        public int ProductId { get; set; }
        public int ProdCatId { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public int? Stock { get; set; }

        [Display(Name = "Buy Price")]
        public decimal? BuyPrice { get; set; }

        [Display(Name = "Sell Price")]
        public decimal? SellPrice { get; set; }
        public virtual ProductCategory ProdCat { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }


        private async void CreateProductDB() {
            try {
                _context.Product.Add(this);
                await _context.SaveChangesAsync();
            } catch {
                throw new Exception("Something went wrong with creating the product");
            }
        }

        public void CreateProduct() {
            CreateProductDB();
        }

        private async Task<List<Product>> GetAllProductsDB() {
            return await _context.Product.Include(x => x.ProdCat).Include(pc => pc.ProdCat).OrderBy(x => x.Description).ToListAsync();
        }

        public async Task<List<Product>> GetAllProducts() {
            return await GetAllProductsDB();
        }

        private async Task<Product> FindProductDB(int id) {
            var result = await _context.Product.Where(x => x.ProductId == id).Include(pc => pc.ProdCat).FirstAsync();

            if (result == null) {
                throw new Exception("Cannot find product.");
            }

            return result;

        }

        public async Task<Product> FindProduct(int id) {
            return await FindProductDB(id);
        }

        private async void UpdatePricesDB() {

            try {

                var product = await FindProductDB(this.ProductId);

                product.UpdateBuyPrice(this.BuyPrice);

                product.UpdateSellPrice(this.SellPrice);

                product.ValidateSellAndBuyPrice(this.SellPrice, this.BuyPrice);

                _context.Update(product);

                await _context.SaveChangesAsync();

            } catch {

                throw new Exception("Something went wrong when updating the prices.");

            }
        }

        public void UpdatePrices() {
            this.UpdatePricesDB();
        }

        private async void UpdateStockDB() {

            try {
                var product = await FindProductDB(this.ProductId);

                product.UpdateStock(this.Stock);

                _context.Update(product);

                await _context.SaveChangesAsync();
            } catch {

                throw new Exception("Something went wrong when updating the stock.");

            }


        }

        public void UpdateStock() {
            this.UpdateStockDB();
        }

        private void ValidateStock(int stockNumber) {
            if (this.Stock == 0 && stockNumber < 0) {
                throw new Exception("Cannot reduce the stock to a negative value");
            }
        }

        private void UpdateStock(int? stock) {

            if (stock != null) {

                ValidateStock((int)stock);

                if (stock < 0) {
                    this.Stock = this.Stock + stock;
                }

                if (stock > 0) {
                    this.Stock = this.Stock + stock;
                }

            } else {

                if (stock < 0) {
                    throw new Exception("Cannot have the stock to be a negative value");
                }                    

                else {
                    this.Stock = stock;
                }             
            }

        }

        private void ValidatePrice(decimal? price) {
            if(price == null) {
                throw new ArithmeticException("The price is not a number");
            } else if((decimal)price < 0) {
                throw new ArgumentException("The price must be greater than 0");
            }
        }

        private void ValidateSellAndBuyPrice(decimal? sellPrice, decimal? buyPrice) {
            if(sellPrice != null && buyPrice != null) {
                if ((decimal)sellPrice < (decimal)buyPrice) {
                    throw new Exception("Sell price can't be less than the buy price.");
                }
            }
        }

        private void UpdateBuyPrice(decimal? buyPrice) {
            ValidatePrice(buyPrice);
            buyPrice = Decimal.Round((decimal)buyPrice, 2);
            this.BuyPrice = buyPrice;
        }

        private void UpdateSellPrice(decimal? sellPrice) {
            ValidatePrice(sellPrice);
            sellPrice = Decimal.Round((decimal)sellPrice, 2);
            this.SellPrice = sellPrice;
        }

        public async Task<Product> ProductDetail(int prodId) {
            try {
                return await FindProductDB(prodId);
            } catch {
                throw new Exception("Something went wrong when getting the product details");
            }           
        }

        private async void DeleteProductDB(int id) {
            try {
                var result = Task.Run(() => FindProductDB(id)).Result;
                _context.Product.Remove(result);
                await _context.SaveChangesAsync();
            } catch {
                throw new Exception("Something went wrong when deleting the product.");
            }

        }

        public void DeleteProduct(int id) {
            DeleteProductDB(id);
        }

        private async Task<List<Product>> GetAllProductsWithCategoriesDB() {
            try {
                var products = await _context.Product.Include(c => c.ProdCat).OrderBy(pc => pc.ProdCat.ProdCat).ThenBy(p => p.Description).ToListAsync();
                return products;
            } catch {
                throw new Exception("Something went wrong when getting all the products with their categories.");
            }

        }

        public async Task<List<Product>> GetAllProductsWithCategories() {
            return await GetAllProductsWithCategoriesDB();
        }
    }
}