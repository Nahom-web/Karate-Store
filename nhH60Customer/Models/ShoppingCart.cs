using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using nhH60Customer.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;

#nullable disable

namespace nhH60Customer.Models {

    [DataContract(Name = "ShoppingCart")]

    public partial class ShoppingCart {

        [NotMapped]
        private const string API_URL = "http://localhost:63164/api/ShoppingCarts";

        public ShoppingCart() {
            CartItems = new HashSet<CartItem>();
        }

        [DataMember(Name = "cartId")]
        public int CartId { get; set; }

        [DataMember(Name = "customerId")]
        public int CustomerId { get; set; }

        [DataMember(Name = "dateCreated")]
        public DateTime DateCreated { get; set; }

        [DataMember(Name = "customer")]
        public virtual Customer Customer { get; set; }

        [DataMember(Name = "cartItems")]
        public virtual ICollection<CartItem> CartItems { get; set; }


        public async Task<ShoppingCart> GetShoppingCart(int CustomerId) {

            HttpClient Client = new();

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );

            //var StreamTask = Client.GetStreamAsync(API_URL + "?CustomerId=" + CustomerId.ToString());

            //var Serializer = new DataContractJsonSerializer(typeof(List<ShoppingCart>));

            //List<ShoppingCart> Cart = Serializer.ReadObject(await StreamTask) as List<ShoppingCart>;

            var StreamTask = await Client.GetAsync(API_URL + "?CustomerId=" + CustomerId.ToString());

            List<ShoppingCart> Cart = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ShoppingCart>>(await StreamTask.Content.ReadAsStringAsync());

            return Cart[0];

        }

        public async Task<HttpResponseMessage> Create(Customer customer) {

            var cart = await GetShoppingCart(customer.CustomerId);

            if (cart == null) {

                this.CustomerId = customer.CustomerId;

                this.DateCreated = DateTime.Now;

                string JsonString = JsonSerializer.Serialize<ShoppingCart>(this);

                var HttpContext = new StringContent(JsonString, Encoding.UTF8, "application/json");

                HttpClient Client = new();

                HttpResponseMessage Response = await Client.PostAsync(API_URL, HttpContext);

                return Response;
            }

            return null;

        }
    }
}
