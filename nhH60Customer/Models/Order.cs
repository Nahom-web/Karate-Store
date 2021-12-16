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
using nhH60Customer.Dtos;

#nullable disable

namespace nhH60Customer.Models {

    [DataContract(Name = "Order")]
    public partial class Order {

        [NotMapped]
        private const string API_URL = "http://localhost:63164/api/Orders";

        public Order() {
            OrderItems = new HashSet<OrderItem>();
        }

        [DataMember(Name = "orderId")]
        public int OrderId { get; set; }

        [DataMember(Name = "customerId")]
        public int CustomerId { get; set; }

        [DataMember(Name = "dateCreated")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        [DataMember(Name = "dateFulfilled")]
        [DataType(DataType.Date)]
        public DateTime? DateFulfilled { get; set; }

        [DataMember(Name = "total")]
        public decimal? Total { get; set; }

        [DataMember(Name = "taxes")]
        public decimal? Taxes { get; set; }

        [DataMember(Name = "customer")]
        public virtual Customer Customer { get; set; }

        [DataMember(Name = "orderItems")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public async Task<OrderDTO> GetOrder() {

            HttpClient Client = new();

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );

            var StreamTask = Client.GetStreamAsync(API_URL + "/" + this.OrderId.ToString());

            var Serializer = new DataContractJsonSerializer(typeof(OrderDTO));

            OrderDTO orderDTO = Serializer.ReadObject(await StreamTask) as OrderDTO;

            return orderDTO;

        }

        public async Task<List<OrderDTO>> AllOrders() {
            HttpClient Client = new();

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );

            var StreamTask = Client.GetStreamAsync(API_URL);

            var Serializer = new DataContractJsonSerializer(typeof(List<OrderDTO>));

            List<OrderDTO> orderDTO = Serializer.ReadObject(await StreamTask) as List<OrderDTO>;

            return orderDTO;
        }

        public async Task<Order> Create(int id) {

            this.CustomerId = id;

            this.DateCreated = DateTime.Now;

            string JsonString = JsonSerializer.Serialize<Order>(this);

            var HttpContext = new StringContent(JsonString, Encoding.UTF8, "application/json");

            HttpClient Client = new();

            HttpResponseMessage Response = await Client.PostAsync(API_URL, HttpContext);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Order>(await Response.Content.ReadAsStringAsync());

        }

    }
}