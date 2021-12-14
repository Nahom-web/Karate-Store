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
using nhH60Customer.Dtos;

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


        public async Task<List<ShoppingCartDTO>> GetShoppingCarts() {

            HttpClient Client = new();

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );

            var StreamTask = Client.GetStreamAsync(API_URL);

            var Serializer = new DataContractJsonSerializer(typeof(List<ShoppingCartDTO>));

            List<ShoppingCartDTO> Carts = Serializer.ReadObject(await StreamTask) as List<ShoppingCartDTO>;

            return Carts;

        }

        public async Task<ShoppingCartDTO> GetShoppingCart(int CustomerId) {

            HttpClient Client = new();

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );

            var StreamTask = Client.GetStreamAsync(API_URL + "/Customers/" + CustomerId.ToString());

            var Serializer = new DataContractJsonSerializer(typeof(ShoppingCartDTO));

            var Cart = Serializer.ReadObject(await StreamTask);

            return (ShoppingCartDTO) Cart;

        }

        public async Task<HttpResponseMessage> Create(Customer customer) {

            var Carts = await GetShoppingCarts();

            if (!CheckIfCustomerHasCart(Carts, customer.CustomerId)) {

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


        private bool CheckIfCustomerHasCart(List<ShoppingCartDTO> Carts, int CustomerId) {
            foreach(var c in Carts) {
                if(c.CustomerId == CustomerId) {
                    return true;
                }
            }
            return false;
        }
    }
}
