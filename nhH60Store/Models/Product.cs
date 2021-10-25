using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace nhH60Store.Models {

    [DataContract(Name = "Product")]

    public class Product {


        [NotMapped]
        private const string PRODUCTS_URL = "http://localhost:63164/api/Products";

        [NotMapped]
        private const string PRODUCTS_BY_CATEGORIES_URL = "http://localhost:63164/api/Products";


        [DataMember(Name = "ProductId")]
        public int ProductId { get; set; }

        [DataMember(Name = "ProdCatId")]
        [Display(Name ="Category")]
        public int ProdCatId { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }

        [DataMember(Name = "Manufacturer")]
        public string Manufacturer { get; set; }

        [DataMember(Name = "Stock")]
        public int? Stock { get; set; }

        [DataMember(Name = "BuyPrice")]
        [Display(Name = "Buy Price")]
        public decimal? BuyPrice { get; set; }

        [DataMember(Name = "SellPrice")]
        [Display(Name = "Sell Price")]
        public decimal? SellPrice { get; set; }

        [DataMember(Name = "ProdCat")]
        public virtual ProductCategory ProdCat { get; set; }

        [DataMember(Name = "CartItems")]
        public virtual ICollection<CartItem> CartItems { get; set; }

        [DataMember(Name = "OrderItems")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public async Task<HttpResponseMessage> CreateProduct() {
            string JsonString = JsonSerializer.Serialize<Product>(this);
            var HttpContext = new StringContent(JsonString, Encoding.UTF8, "application/json");

            HttpClient Client = new();

            HttpResponseMessage Response = await Client.PostAsync(PRODUCTS_URL, HttpContext);

            return Response;
        }

        public async Task<List<Product>> GetAllProducts() {
            HttpClient Client = new();

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );

            var StreamTask = Client.GetStreamAsync(PRODUCTS_URL);

            var Serializer = new DataContractJsonSerializer(typeof(List<Product>));

            List<Product> Products = Serializer.ReadObject(await StreamTask) as List<Product>;

            return Products;
        }

        public async Task<Product> FindProduct(int id) {
            HttpClient Client = new();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            Client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository");

            string TaskString = PRODUCTS_URL + "/" + id.ToString();

            var StreamTask = Client.GetStreamAsync(TaskString);

            var Serializer = new DataContractJsonSerializer(typeof(Product));

            Product product = Serializer.ReadObject(await StreamTask) as Product;

            return product;

        }

        public async Task<HttpResponseMessage> UpdatePrices() {

            var product = await FindProduct(this.ProductId);

            product.UpdateBuyPrice(this.BuyPrice);

            product.UpdateSellPrice(this.SellPrice);

            product.ValidateSellAndBuyPrice(this.SellPrice, this.BuyPrice);

            string JsonString = JsonSerializer.Serialize<Product>(product);

            var HttpContext = new StringContent(JsonString, Encoding.UTF8, "application/json");

            HttpClient Client = new();

            HttpResponseMessage Response = await Client.PutAsync(PRODUCTS_URL + "/" + this.ProductId.ToString(), HttpContext);

            return Response;
        }

        private void ValidatePrice(decimal? price) {
            if (price == null) {
                throw new ArithmeticException("The price is not a number");
            } else if ((decimal)price < 0) {
                throw new ArgumentException("The price must be greater than 0");
            }
        }

        private void ValidateSellAndBuyPrice(decimal? sellPrice, decimal? buyPrice) {
            if (sellPrice != null && buyPrice != null) {
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

        public async Task<HttpResponseMessage> UpdateStock() {
            var product = await FindProduct(this.ProductId);

            product.ValidateStock(this.Stock);

            string JsonString = JsonSerializer.Serialize<Product>(product);

            var HttpContext = new StringContent(JsonString, Encoding.UTF8, "application/json");

            HttpClient Client = new();

            HttpResponseMessage Response = await Client.PutAsync(PRODUCTS_URL + "/" + this.ProductId.ToString(), HttpContext);

            return Response;
        }

        private void ValidateStock(int? stock) {
            if (stock != null) {
                if (stock < 0) {
                    throw new ArgumentException("Cannot reduce the stock to a negative value");
                }
                this.Stock = stock;
            } else {
                throw new Exception("Please enter in a number for the stock.");
            }
        }

        public async Task<Product> ProductDetail(int prodId) {
            try {
                return await FindProduct(prodId);
            } catch {
                throw new Exception("Something went wrong when getting the product details");
            }
        }

        public async Task<HttpResponseMessage> DeleteProduct(int id) {
            HttpClient Client = new();

            HttpResponseMessage Response = await Client.DeleteAsync(PRODUCTS_URL + "/" + id.ToString());

            return Response;
        }

        public async Task<List<Product>> GetAllProductsWithCategories() {
            HttpClient Client = new();

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );

            var StreamTask = Client.GetStreamAsync(PRODUCTS_BY_CATEGORIES_URL);

            var Serializer = new DataContractJsonSerializer(typeof(List<Product>));

            List<Product> Products = Serializer.ReadObject(await StreamTask) as List<Product>;

            return Products;
        }

    }
}