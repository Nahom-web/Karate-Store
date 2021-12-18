using nhH60Customer.Dtos;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace nhH60Customer.Models {

    [DataContract(Name = "OrderItem")]
    public partial class OrderItem {

        [NotMapped]
        private const string API_URL = "http://localhost:63164/api/OrderItems";

        [DataMember(Name = "orderItemId")]
        public int OrderItemId { get; set; }

        [DataMember(Name = "orderId")]
        public int OrderId { get; set; }

        [DataMember(Name = "productId")]
        public int ProductId { get; set; }

        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }

        [DataMember(Name = "price")]
        public decimal? Price { get; set; }

        [DataMember(Name = "order")]
        public virtual Order Order { get; set; }

        [DataMember(Name = "product")]
        public virtual Product Product { get; set; }


        public async Task<HttpResponseMessage> Create(int orderId, CartItemDTO cartItem) {

            this.OrderId = orderId;

            this.ProductId = cartItem.ProductId;

            this.Quantity = cartItem.Quantity;

            this.Price = cartItem.Price;

            string JsonString = JsonSerializer.Serialize<OrderItem>(this);

            var HttpContext = new StringContent(JsonString, Encoding.UTF8, "application/json");

            HttpClient Client = new();

            HttpResponseMessage Response = await Client.PostAsync(API_URL, HttpContext);

            return Response;

        }
    }
}
