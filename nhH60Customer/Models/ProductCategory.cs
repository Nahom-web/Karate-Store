using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

#nullable disable

namespace nhH60Customer.Models {

    [DataContract(Name = "ProductCategory")]

    public partial class ProductCategory {

        [NotMapped]
        private const string PRODUCT_CATEGORY_URL = "http://localhost:63164/api/ProductCategories";


        [DataMember(Name = "categoryId")]
        public int CategoryId { get; set; }

        [DataMember(Name = "prodCat")]
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
    }
}
