using nhH60Customer.Dtos;
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

namespace nhH60Customer.Models {

    [DataContract(Name = "CartItem")]

    public partial class CartItem {

        [NotMapped]
        private const string API_URL = "http://localhost:63164/api/CartItems";

        [DataMember(Name = "cartItemId")]
        public int CartItemId { get; set; }

        [DataMember(Name = "cartId")]
        public int CartId { get; set; }

        [DataMember(Name = "productId")]
        public int ProductId { get; set; }

        [DataMember(Name = "quantity")]
        [Required]
        public int Quantity { get; set; }

        [DataMember(Name = "price")]
        public decimal? Price { get; set; }

        [DataMember(Name = "cart")]

        public virtual ShoppingCart Cart { get; set; }

        [DataMember(Name = "product")]
        public virtual Product Product { get; set; }

        public async Task<HttpResponseMessage> Create() {

            string JsonString = JsonSerializer.Serialize<CartItem>(this);

            var HttpContext = new StringContent(JsonString, Encoding.UTF8, "application/json");

            HttpClient Client = new();

            HttpResponseMessage Response = await Client.PostAsync(API_URL, HttpContext);

            return Response;
        }

        public async Task<CartItem> FindCartItem(int id) {
            HttpClient Client = new();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            Client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository");

            string TaskString = API_URL + "/" + id.ToString();

            var StreamTask = Client.GetStreamAsync(TaskString);

            var Serializer = new DataContractJsonSerializer(typeof(CartItem));

            CartItem item = Serializer.ReadObject(await StreamTask) as CartItem;

            return item;

        }

        public async Task<HttpResponseMessage> Update(CartItemDTO UpdatedItem) {
            var item = await FindCartItem(UpdatedItem.CartItemId);

            item.Quantity = UpdatedItem.Quantity;

            string JsonString = JsonSerializer.Serialize<CartItem>(item);

            var HttpContext = new StringContent(JsonString, Encoding.UTF8, "application/json");

            HttpClient Client = new();

            HttpResponseMessage Response = await Client.PutAsync(API_URL + "/" + item.CartItemId.ToString(), HttpContext);

            return Response;
        }

        public async Task<HttpResponseMessage> Delete(int id) {
            HttpClient Client = new();

            HttpResponseMessage Response = await Client.DeleteAsync(API_URL + "/" + id.ToString());

            return Response;
        }
    }
}
