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

    [DataContract(Name = "ProductCategory")]

    public partial class ProductCategory {

        [NotMapped]
        private const string PRODUCT_CATEGORY_URL = "http://localhost:63164/api/ProductCategory";


        [DataMember(Name = "categoryId")]
        public int CategoryId { get; set; }

        [DataMember(Name = "prodCat")]
        [Display(Name = "Category Name")]
        public string ProdCat { get; set; }

        [DataMember(Name = "products")]
        public virtual ICollection<Product> Product { get; set; }

        public async Task<List<ProductCategory>> GetAllCategories() {
            HttpClient Client = new();

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );

            var StreamTask = Client.GetStreamAsync(PRODUCT_CATEGORY_URL);

            var Serializer = new DataContractJsonSerializer(typeof(List<ProductCategory>));

            List<ProductCategory> Products = Serializer.ReadObject(await StreamTask) as List<ProductCategory>;

            return Products;
        }


        public async Task<List<Product>> GetProductsForCategory(int id) {
            HttpClient Client = new();

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );

            var StreamTask = Client.GetStreamAsync(PRODUCT_CATEGORY_URL + "/Products?id=" + id.ToString());

            var Serializer = new DataContractJsonSerializer(typeof(List<Product>));

            List<Product> Products = Serializer.ReadObject(await StreamTask) as List<Product>;

            return Products;
        }

        public async Task<ProductCategory> FindCategory(int id) {
            HttpClient Client = new();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            Client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository");

            string TaskString = PRODUCT_CATEGORY_URL + "/" + id.ToString();

            var StreamTask = Client.GetStreamAsync(TaskString);

            var Serializer = new DataContractJsonSerializer(typeof(ProductCategory));

            ProductCategory product = Serializer.ReadObject(await StreamTask) as ProductCategory;

            return product;

        }


        public async Task<HttpResponseMessage> Create() {
            string JsonString = JsonSerializer.Serialize<ProductCategory>(this);
            var HttpContext = new StringContent(JsonString, Encoding.UTF8, "application/json");

            HttpClient Client = new();

            HttpResponseMessage Response = await Client.PostAsync(PRODUCT_CATEGORY_URL, HttpContext);

            return Response;
        }


        public async Task<HttpResponseMessage> Update() {
            string JsonString = JsonSerializer.Serialize<ProductCategory>(this);

            var HttpContext = new StringContent(JsonString, Encoding.UTF8, "application/json");

            HttpClient Client = new();

            HttpResponseMessage Response = await Client.PutAsync(PRODUCT_CATEGORY_URL + "/" + this.CategoryId.ToString(), HttpContext);

            return Response;
        }


        public async Task<HttpResponseMessage> Delete(int id) {
            HttpClient Client = new();

            HttpResponseMessage Response = await Client.DeleteAsync(PRODUCT_CATEGORY_URL + "/" + id.ToString());

            return Response;
        }
    }
}